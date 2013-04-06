namespace AzureFtpForSharePoint.Core.SharePointLibrary
{
    using log4net;
    using System;
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

        /// <summary>
        /// Checks if a URL belongs to a SharePoint website.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>True if the URL is a SharePoint website. False, otherwise.</returns>
        public bool IsWeb(string url)
        {
            string methodName = "SharePointManager2003.IsWeb";

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
        /// Retrieves the URL for the closest website.
        /// </summary>
        /// <param name="url">Original URL to start searching for the website.</param>
        /// <returns>The URL of the closest website, or Null if not found.</returns>
        public string GetWebUrl(string url)
        {
            string methodName = "SharePointManager2003.GetWebUrl";

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
                return GetWebUrl(newUrl);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("{0}: Error getting Web for URL {1}.", methodName, url), e);
                return null;
            }
        }
    }
}
