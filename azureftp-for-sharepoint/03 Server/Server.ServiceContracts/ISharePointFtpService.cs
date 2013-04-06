namespace AzureFtpForSharePoint.Server.ServiceContracts
{
    using AzureFtpForSharePoint.Server.DataContracts;
    using System.ServiceModel;
    
    /// <summary>
    /// [Draft] Contract definition for the SharePoint FTP Operations
    /// These are not final contracts!! Comment them as implementation is stabilized
    /// </summary>
    [ServiceContract(Namespace = "http://www.4guysfrompalmira.net/contracts/2013/04/04")]
    public interface ISharePointFtpService
    {
        [OperationContract]
        OpenResponse Open(string url, string username, string password);

        [OperationContract]
        CloseResponse Close(string sessionId);

        [OperationContract]
        string[] List(string currentFolder);

        [OperationContract]
        bool ChangeDirectory(string path);

        [OperationContract]
        byte[] Get(string file);
    }
}
