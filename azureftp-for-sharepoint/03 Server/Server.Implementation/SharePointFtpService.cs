namespace AzureFtpForSharePoint.Server.Implementation
{
    using AzureFtpForSharePoint.Core.SharePointLibrary;
    using AzureFtpForSharePoint.Server.ServiceContracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Implementation of the SharePoint FTP Service
    /// [DRAFT]
    /// </summary>
    public class SharePointFtpService : ISharePointFtpService
    {
        public bool ChangeDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] Get(string file)
        {
            throw new NotImplementedException();
        }

        public string[] List(string currentFolder)
        {
            throw new NotImplementedException();
        }

        public bool Open(string url, string username, string password)
        {
            SharePointManager manager = new SharePointManager();
            return manager.Open(url, username, password);
        }
    }
}