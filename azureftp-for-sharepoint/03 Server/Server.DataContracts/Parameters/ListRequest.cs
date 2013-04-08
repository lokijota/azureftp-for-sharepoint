namespace AzureFtpForSharePoint.Server.DataContracts.Parameters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    public class ListRequest
    {
        [DataMember]
        public string WebUrl { get; set; }

        [DataMember]
        public string FolderUrl { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
