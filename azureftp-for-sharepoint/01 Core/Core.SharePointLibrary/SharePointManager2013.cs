namespace AzureFtpForSharePoint.Core.SharePointLibrary
{
    using AzureFtpForSharePoint.Core.SharePointLibrary.DataObjects;
    using AzureFtpForSharePoint.Core.SharePointLibrary.Exceptions;
    using AzureFtpForSharePoint.Core.SharePointLibrary.Interfaces;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using SP = Microsoft.SharePoint.Client;

    /// <summary>
    /// This class holds the responsibility of talking to SharePoint and performing several different operations on it in an abstracted way.
    /// It used ClientOM to talk to SharePoint Server 2013
    /// notajota: define an interface...
    /// </summary>
    public class SharePointManager2013: ISharePointManager
    {
        /// <summary>
        /// Create a logger for use in this class 
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Caracter used for a root folder URL.
        /// </summary>
        private const string ROOT_FOLDER_URL = "/";

        #region Private Methods

        /// <summary>
        /// Checks if a URL belongs to a SharePoint website.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">Username for SharePoint authentication.</param>
        /// <param name="password">Password for SharePoint authentication.</param>
        /// <returns>True if the URL is a SharePoint website. False, otherwise.</returns>
        private bool IsWeb(string url, string username, string password)
        {
            string methodName = "SharePointManager2013.IsWeb";

            try
            {
                Logger.DebugFormat("{0}: Checking if {1} is a website.", methodName, url);

                SP.ClientContext context = new SP.ClientContext(url);
                context.Credentials = new NetworkCredential(username, password);

                SP.Web web = context.Web; // The SharePoint web at the URL.
                context.Load(web, w => w.Title); // We want to retrieve the web's properties.
                context.ExecuteQuery(); // Execute the query to the server.

                Logger.DebugFormat("{0}: URL {1} is a website with title {2}.", methodName, url, web.Title);
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("{0}: Error checking URL {1}.", methodName, url), e);
                return false;
            }
        }

        /// <summary>
        /// Retrieves the URL of the web that contains the specified URL.
        /// </summary>
        /// <param name="url">URL to use to search for the container web.</param>
        /// <param name="username">Username for SharePoint authentication.</param>
        /// <param name="password">Password for SharePoint authentication.</param>
        /// <returns>The website URL if found. Null otherwise.</returns>
        private string GetWebUrl(string url, string username, string password)
        {
            // try to use the direct method to get the web URL from a folder URL.
            // if that fails (because the URL is not a folder) try the recursive approach.
            string webUrl = GetWebUrlFromFolderUrl(url, username, password);
            if (string.IsNullOrEmpty(webUrl))
            {
                webUrl = GetWebUrlRecursive(url, username, password);
            }

            return webUrl;
        }

        /// <summary>
        /// Retrieves the URL for the closest website, recursively.
        /// </summary>
        /// <param name="url">Original URL to start searching for the website.</param>
        /// <param name="username">Username for SharePoint authentication.</param>
        /// <param name="password">Password for SharePoint authentication.</param>
        /// <returns>The URL of the closest website, or Null if not found.</returns>
        private string GetWebUrlRecursive(string url, string username, string password)
        {
            string methodName = "SharePointManager2013.GetWebUrlRecursive";

            try
            {
                // if this URL belogns to a website, the search is over
                if (IsWeb(url, username, password))
                {
                    return url;
                }

                // built a new URL with all the segments except the last one
                Uri uri = new Uri(url);
                string[] uriSegments = uri.Segments;
                if (uriSegments.Length == 1)
                {
                    return null;
                }

                string newUrl = url.Substring(0, url.Length - uriSegments[uriSegments.Length - 1].Length);
                return GetWebUrlRecursive(newUrl, username, password);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("{0}: Error getting Web for URL {1}.", methodName, url), e);
                return null;
            }
        }

        /// <summary>
        /// Uses the direct method to get the web URL from a folder URL.
        /// </summary>
        /// <param name="url">Folder URL.</param>
        /// <param name="username">Username for SharePoint authentication.</param>
        /// <param name="password">Password for SharePoint authentication.</param>
        /// <returns>The web URL if found. Null otherwise (or if the specified URL is not a folder).</returns>
        private string GetWebUrlFromFolderUrl(string url, string username, string password)
        {
            string methodName = "SharePointManager2013.GetWebUrlFromFolderUrl";

            try
            {
                SP.ClientContext context = new SP.ClientContext(url);
                context.Credentials = new NetworkCredential(username, password);

                return SP.Web.WebUrlFromFolderUrlDirect(context, new Uri(url)).OriginalString;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("{0}: Could not find folder with URL {1}.", methodName, url), e);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a folder's server-relative URL from its absolute URL. If not found, it tries again removing the last URL segment.
        /// </summary>
        /// <param name="webUrl">Web site URL.</param>
        /// <param name="folderUrl">Folder absolute URL.</param>
        /// <param name="username">Username for SharePoint authentication.</param>
        /// <param name="password">Password for SharePoint authentication.</param>
        /// <returns>The server-relative URL for the folder, if found. Null otherwise.</returns>
        private string GetFolderUrl(string webUrl, string folderUrl, string username, string password)
        {
            string methodName = "SharePointManager2013.GetFolderUrl";

            try
            {
                Uri folderUri = new Uri(folderUrl);
                string[] uriSegments = folderUri.Segments;

                // compare the folder URL with the web URL. If they're the same, then there is no folder with that URL
                if (Uri.Compare(folderUri, new Uri(webUrl), UriComponents.AbsoluteUri, UriFormat.SafeUnescaped, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    return ROOT_FOLDER_URL;
                }

                // if there are no more segments to remove, the folder URL was not found
                if (uriSegments.Length == 1)
                {
                    return null;
                }

                // check if the URL is a valid folder
                string serverRelativeUrl = GetFolderUrlDirect(webUrl, folderUrl, username, password);
                if (serverRelativeUrl != null)
                {
                    return serverRelativeUrl;
                }

                // if not found, remove last segment and try again
                string newUrl = folderUrl.Substring(0, folderUrl.Length - uriSegments[uriSegments.Length - 1].Length);
                return GetFolderUrl(webUrl, newUrl, username, password);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("{0}: Error getting folder for URL {1}.", methodName, folderUrl), e);
                return null;
            }
        }

        /// <summary>
        /// Retrieves the server-relative URL of a folder from its absolute URL.
        /// </summary>
        /// <param name="webUrl">Web URL to establish context.</param>
        /// <param name="folderUrl">folder URL</param>
        /// <param name="username">Username for SharePoint authentication.</param>
        /// <param name="password">Password for SharePoint authentication.</param>
        /// <returns>The server-relative folder URL if found. Null otherwise.</returns>
        private string GetFolderUrlDirect(string webUrl, string folderUrl, string username, string password)
        {
            string methodName = "SharePointManager2003.GetFolderUrlDirect";

            try
            {
                SP.ClientContext context = new SP.ClientContext(webUrl);
                context.Credentials = new NetworkCredential(username, password);

                // load the web data                
                SP.Web web = context.Web;
                context.Load(web);

                // load the folder data
                Uri uri = new Uri(folderUrl);
                SP.Folder folder = web.GetFolderByServerRelativeUrl(uri.AbsolutePath);
                context.Load(folder);

                context.ExecuteQuery();

                // check if the URL belongs to a folder
                if (folder != null)
                {
                    return folder.ServerRelativeUrl;
                }

                return null;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("{0}: Could not find folder with URL {1}.", methodName, folderUrl), e);
                return null;
            }
        }

        #endregion

        #region ISharePointManager Interface Implementation Methods

        /// <summary>
        /// Opens a connection and sets the context for a specific website and folder in SharePoint.
        /// </summary>
        /// <param name="url">Absolute URL of the folder.</param>
        /// <param name="username">Username for SharePoint authentication.</param>
        /// <param name="password">Password for SharePoint authentication.</param>
        /// <returns>Context object with the calculated URLs of the website and folder.</returns>
        /// <exception cref="InvalidUrlException">Thrown if the URL is not a valid SharePoint website or folder.</exception>
        public CurrentContext Open(string url, string username, string password)
        {
            string webUrl = this.GetWebUrl(url, username, password);
            if (!string.IsNullOrEmpty(webUrl))
            {
                string folderUrl = this.GetFolderUrl(webUrl, url, username, password);

                return new CurrentContext()
                {
                    WebUrl = webUrl,
                    FolderUrl = folderUrl
                };
            }
            else
            {
                throw new InvalidUrlException(url, "Could not find a SharePoint website for the requested URL.");
            }
        }

        #endregion
    }
}
