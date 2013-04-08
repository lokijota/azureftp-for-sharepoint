namespace AzureFtpForSharePoint.Server.DataContracts.Parameters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    public class OpenRequest
    {
        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
