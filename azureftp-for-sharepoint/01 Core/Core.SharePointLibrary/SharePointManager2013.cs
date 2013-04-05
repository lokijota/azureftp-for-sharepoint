﻿namespace AzureFtpForSharePoint.Core.SharePointLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using Microsoft.SharePoint.Client;

    /// <summary>
    /// This class holds the responsibility of talking to SharePoint and performing several different operations on it in an abstracted way.
    /// It used ClientOM to talk to SharePoint Server 2013
    /// notajota: define an interface...
    /// </summary>
    public class SharePointManager2013
    {
        /// <summary>
        /// Checks if the specified credentials have access to the specified SharePoint url
        /// </summary>
        /// <param name="url">The URL to she SharePoint site</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>true if has access, false otherwise</returns>
        public bool Open(string url, string username, string password)
        {
            try
            {
                System.IO.File.AppendAllText(@"c:\azureftpforsharepoint\log.server.log", string.Format("{0} Attempting open URL {1}", DateTime.Now.ToShortTimeString(), url));

                // Starting with ClientContext, the constructor requires a URL to the 
                // server running SharePoint. 
                ClientContext context = new ClientContext(url);
                context.Credentials = new NetworkCredential(username, password);

                Web web = context.Web; // The SharePoint web at the URL.
                context.Load(web); // We want to retrieve the web's properties.
                context.ExecuteQuery(); // Execute the query to the server.

                System.IO.File.AppendAllText(@"c:\azureftpforsharepoint\log.server.log", string.Format("{0} Open URL {1} with title {2}", DateTime.Now.ToShortTimeString(), url, web.Title));
                return true;
            }
            catch (Exception e)
            {
                System.IO.File.AppendAllText(@"c:\azureftpforsharepoint\log.server.log", string.Format("{0} Open URL {1} with error: {2}", DateTime.Now.ToShortTimeString(), url, e.Message));
                return false;
            }
        }
    }
}
