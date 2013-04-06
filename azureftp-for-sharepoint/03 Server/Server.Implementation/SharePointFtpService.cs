namespace AzureFtpForSharePoint.Server.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AzureFtpForSharePoint.Core.SharePointLibrary;
    using AzureFtpForSharePoint.Server.ServiceContracts;
    using AzureFtpForSharePoint.Server.DataContracts;

    /// <summary>
    /// Implementation of the SharePoint FTP Service
    /// [DRAFT]
    /// </summary>
    public class SharePointFtpService : ISharePointFtpService
    {
        /// <summary>
        /// Dictionary used to store session data.
        /// </summary>
        private static Dictionary<string, SessionData> _sessions = new Dictionary<string, SessionData>();

        public bool ChangeDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] Get(string file)
        {
            throw new NotImplementedException();
        }

        public string[] List(string currentFolder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens a connection and sets up an FTP session.
        /// </summary>
        /// <param name="url">Start URL.</param>
        /// <param name="username">Username used for authentication.</param>
        /// <param name="password">Password used for authentication.</param>
        /// <returns>Specific response object with operation status and session ID.</returns>
        public OpenResponse Open(string url, string username, string password)
        {
            try
            {
                SharePointManager2013 manager = new SharePointManager2013(username, password);
                if (manager.IsWeb(url))
                {
                    // generate new session data
                    Guid sessionId = Guid.NewGuid();
                    SessionData sessionData = new SessionData()
                    {
                        Username = username,
                        Password = password,
                        WebUrl = url
                    };

                    // store session data in dictionary
                    _sessions.Add(sessionId.ToString(), sessionData);

                    return new OpenResponse()
                    {
                        StatusCode = (int)OpenResponse.StatusOption.Success,
                        StatusMessage = OpenResponse.StatusOption.Success.ToString(),
                        SessionId = sessionId.ToString()
                    };
                }
                else
                {
                    return new OpenResponse()
                    {
                        StatusCode = (int)OpenResponse.StatusOption.InvalidUrl,
                        StatusMessage = OpenResponse.StatusOption.InvalidUrl.ToString()
                    };
                }
            }
            catch (Exception e)
            {
                return new OpenResponse()
                {
                    StatusCode = (int)OpenResponse.StatusOption.UnknownError,
                    StatusMessage = e.Message
                };
            }
        }

        /// <summary>
        /// Closes the connection and removes the session data.
        /// </summary>
        /// <param name="sessionId">Session ID.</param>
        /// <returns>Specific response object with operation status.</returns>
        public CloseResponse Close(string sessionId)
        {
            try
            {
                if (!string.IsNullOrEmpty(sessionId) && _sessions.ContainsKey(sessionId))
                {
                    _sessions.Remove(sessionId);
                    return new CloseResponse()
                    {
                        StatusCode = (int)CloseResponse.StatusOption.Success,
                        StatusMessage = CloseResponse.StatusOption.Success.ToString()
                    };
                }
                else
                {
                    return new CloseResponse()
                    {
                        StatusCode = (int)CloseResponse.StatusOption.InvalidSession,
                        StatusMessage = CloseResponse.StatusOption.InvalidSession.ToString()
                    };
                }
            }
            catch (Exception e)
            {
                return new CloseResponse()
                {
                    StatusCode = (int)CloseResponse.StatusOption.UnknownError,
                    StatusMessage = e.Message
                };
            }
        }
    }
}