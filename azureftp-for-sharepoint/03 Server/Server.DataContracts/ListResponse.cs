namespace AzureFtpForSharePoint.Server.DataContracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    [KnownType(typeof(ServiceResponse))]
    public class ListResponse : ServiceResponse
    {
    }
}
