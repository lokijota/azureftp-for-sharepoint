namespace AzureFtpForSharePoint.Server.ServiceContracts
{
    using AzureFtpForSharePoint.Server.DataContracts;
    using AzureFtpForSharePoint.Server.DataContracts.Faults;
    using AzureFtpForSharePoint.Server.DataContracts.Parameters;
    using System.ServiceModel;
    
    /// <summary>
    /// [Draft] Contract definition for the SharePoint FTP Operations
    /// These are not final contracts!! Comment them as implementation is stabilized
    /// </summary>
    [ServiceContract(Namespace = "http://www.4guysfrompalmira.net/contracts/2013/04/04")]
    public interface ISharePointFtpService
    {
        [OperationContract]
        [FaultContract(typeof(InvalidUrlFault))]
        OpenResponse Open(OpenRequest request);

        [OperationContract]
        ListResponse List(ListRequest request);

        [OperationContract]
        ChangeDirectoryResponse ChangeDirectory(ChangeDirectoryRequest request);

        [OperationContract]
        GetResponse Get(GetRequest request);
    }
}
