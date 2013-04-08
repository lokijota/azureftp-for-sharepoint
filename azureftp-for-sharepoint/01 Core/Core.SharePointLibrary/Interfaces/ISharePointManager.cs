namespace AzureFtpForSharePoint.Core.SharePointLibrary.Interfaces
{
    using AzureFtpForSharePoint.Core.SharePointLibrary.DataObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface used by all SharePoint Managers.
    /// </summary>
    public interface ISharePointManager
    {
        /// <summary>
        /// Opens a connection and sets the context for a specific website and folder in SharePoint.
        /// </summary>
        /// <param name="url">Absolute URL of the folder.</param>
        /// <param name="username">Username for SharePoint access.</param>
        /// <param name="password">Password for SharePoint access.</param>
        /// <returns>Context object with the calculated URLs of the website and folder.</returns>
        /// <exception cref="InvalidUrlException">Thrown if the URL is not a valid SharePoint website or folder.</exception>
        CurrentContext Open(string url, string username, string password);
    }
}
