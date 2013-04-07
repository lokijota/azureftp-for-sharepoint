namespace AzureFtpForSharePoint.Core.SharePointLibrary
{
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
    public class SharePointManager2013
    {
        /// <summary>
        /// Create a logger for use in this class 
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Username used for SharePoint access.
        /// </summary>
        private string Username { get; set; }

        /// <summary>
        /// Password used for SharePoint access.
        /// </summary>
        private string Password { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="username">Username to be used for all SharePoint calls.</param>
        /// <param name="password">Password to be used for all SharePoint calls.</param>
        public SharePointManager2013(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        #region Public Methods

        /// <summary>
        /// Checks if a URL belongs to a SharePoint website.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>True if the URL is a SharePoint website. False, otherwise.</returns>
        public bool IsWeb(string url)
        {
            string methodName = "SharePointManager2013.IsWeb";

            try
            {
                Logger.DebugFormat("{0}: Checking if {1} is a website.", methodName, url);

                SP.ClientContext context = new SP.ClientContext(url);
                context.Credentials = new NetworkCredential(this.Username, this.Password);

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
        /// <returns>The website URL if found. Null otherwise.</returns>
        public string GetWebUrl(string url)
        {
            // try to use the direct method to get the web URL from a folder URL.
            // if that fails (because the URL is not a folder) try the recursive approach.
            string webUrl = GetWebUrlFromFolderUrl(url);
            if (string.IsNullOrEmpty(webUrl))
            {
                webUrl = GetWebUrlRecursive(url);
            }

            return webUrl;
        }

        /// <summary>
        /// Retrieves a folder's server-relative URL from its absolute URL. If not found, it tries again removing the last URL segment.
        /// </summary>
        /// <param name="webUrl">Web site URL.</param>
        /// <param name="folderUrl">Folder absolute URL.</param>
        /// <returns>The server-relative URL for the folder, if found. Null otherwise.</returns>
        public string GetFolderUrl(string webUrl, string folderUrl)
        {
            string methodName = "SharePointManager2013.GetFolderUrl";

            try
            {
                Uri folderUri = new Uri(folderUrl);
                string[] uriSegments = folderUri.Segments;

                // compare the folder URL with the web URL. If they're the same, then there is no folder with that URL
                if (Uri.Compare(folderUri, new Uri(webUrl), UriComponents.AbsoluteUri, UriFormat.SafeUnescaped, StringComparison.InvariantCultureIgnoreCase) == 0 ||
                    uriSegments.Length == 1)
                {
                    return null;
                }

                // check if the URL is a valid folder
                string serverRelativeUrl = GetFolderUrlDirect(webUrl, folderUrl);
                if (serverRelativeUrl != null)
                {
                    return serverRelativeUrl;
                }

                // if not found, remove last segment and try again
                string newUrl = folderUrl.Substring(0, folderUrl.Length - uriSegments[uriSegments.Length - 1].Length);
                return GetFolderUrl(webUrl, newUrl);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("{0}: Error getting folder for URL {1}.", methodName, folderUrl), e);
                return null;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves the URL for the closest website, recursively.
        /// </summary>
        /// <param name="url">Original URL to start searching for the website.</param>
        /// <returns>The URL of the closest website, or Null if not found.</returns>
        private string GetWebUrlRecursive(string url)
        {
            string methodName = "SharePointManager2013.GetWebUrlRecursive";

            try
            {
                // if this URL belogns to a website, the search is over
                if (IsWeb(url))
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
                return GetWebUrlRecursive(newUrl);
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
        /// <returns>The web URL if found. Null otherwise (or if the specified URL is not a folder).</returns>
        private string GetWebUrlFromFolderUrl(string url)
        {
            string methodName = "SharePointManager2013.GetWebUrlFromFolderUrl";

            try
            {
                SP.ClientContext context = new SP.ClientContext(url);
                context.Credentials = new NetworkCredential(this.Username, this.Password);

                return SP.Web.WebUrlFromFolderUrlDirect(context, new Uri(url)).OriginalString;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("{0}: Could not find folder with URL {1}.", methodName, url), e);
                return null;
            }
        }

        /// <summary>
        /// Retrieves the server-relative URL of a folder from its absolute URL.
        /// </summary>
        /// <param name="webUrl">Web URL to establish context.</param>
        /// <param name="folderUrl">folder URL</param>
        /// <returns>The server-relative folder URL if found. Null otherwise.</returns>
        private string GetFolderUrlDirect(string webUrl, string folderUrl)
        {
            string methodName = "SharePointManager2003.GetFolderUrlDirect";

            try
            {
                SP.ClientContext context = new SP.ClientContext(webUrl);
                context.Credentials = new NetworkCredential(this.Username, this.Password);

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
    }
}
