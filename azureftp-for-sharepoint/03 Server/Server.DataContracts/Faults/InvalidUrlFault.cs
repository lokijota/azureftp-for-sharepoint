namespace AzureFtpForSharePoint.Server.DataContracts.Faults
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    public class InvalidUrlFault
    {
        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
