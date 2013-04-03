namespace AzureFtpForSharePoint.Server.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AzureFtpForSharePoint.Server.ServiceContracts;

    /// <summary>
    /// Implementation of the SharePoint FTP Service
    /// [DRAFT]
    /// </summary>
    public class SharePointFtpService : ISharePointFtpService
    {
        public bool Open(string url, string username, string password)
        {
            throw new NotImplementedException();
        }

        public string[] List(string currentFolder)
        {
            throw new NotImplementedException();
        }

        public bool ChangeDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] Get(string file)
        {
            throw new NotImplementedException();
        }
    }
}
