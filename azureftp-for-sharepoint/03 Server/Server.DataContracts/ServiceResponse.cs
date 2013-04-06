namespace AzureFtpForSharePoint.Server.DataContracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    public class ServiceResponse
    {
        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public string StatusMessage { get; set; }
    }
}
