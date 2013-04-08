namespace AzureFtpForSharePoint.Server.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AzureFtpForSharePoint.Core.SharePointLibrary;
    using AzureFtpForSharePoint.Server.ServiceContracts;
    using AzureFtpForSharePoint.Server.DataContracts;
    using AzureFtpForSharePoint.Core.SharePointLibrary.DataObjects;
    using AzureFtpForSharePoint.Server.Implementation.Mappers;
    using AzureFtpForSharePoint.Server.DataContracts.Parameters;
    using AzureFtpForSharePoint.Core.SharePointLibrary.Exceptions;
    using AzureFtpForSharePoint.Server.DataContracts.Faults;
    using System.ServiceModel;
    using AzureFtpForSharePoint.Core.SharePointLibrary.Interfaces;

    /// <summary>
    /// Implementation of the SharePoint FTP Service
    /// [DRAFT]
    /// </summary>
    public class SharePointFtpService : ISharePointFtpService
    {
        public ChangeDirectoryResponse ChangeDirectory(ChangeDirectoryRequest request)
        {
            throw new NotImplementedException();
        }

        public GetResponse Get(GetRequest request)
        {
            throw new NotImplementedException();
        }

        public ListResponse List(ListRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens a connection and sets up an FTP session.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <returns>Specific response object with operation status and session ID.</returns>
        public OpenResponse Open(OpenRequest request)
        {
            try
            {
                ISharePointManager manager = new SharePointManager2013();
                CurrentContext currentContext = manager.Open(request.Url, request.Username, request.Password);
                return OpenResponseMapper.MapFrom(currentContext);
            }
            catch (InvalidUrlException e)
            {
                InvalidUrlFault fault = new InvalidUrlFault()
                {
                    Url = e.Url,
                    Message = e.Message
                };

                throw new FaultException<InvalidUrlFault>(fault, "Error opening the requested URL.");
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "Unexpected error opening the requested URL.");
            }
        }
    }
}