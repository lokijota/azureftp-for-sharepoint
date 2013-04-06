namespace AzureFtpForSharePoint.Server.DataContracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    [KnownType(typeof(ServiceResponse))]
    public class OpenResponse: ServiceResponse
    {
        public enum StatusOption
        {
            Success = 0,

            InvalidUrl = 1,

            AuthenticationFailed = 2,

            UnknownError = 99
        }

        [DataMember]
        public string SessionId { get; set; }
    }
}
