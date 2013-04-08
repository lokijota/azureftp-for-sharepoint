namespace AzureFtpForSharePoint.Core.SharePointLibrary.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Exception thrown if an Invalid URL is requested.
    /// </summary>
    public class InvalidUrlException : ApplicationException
    {
        /// <summary>
        /// Gets the requested URL.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url">Requested URL.</param>
        public InvalidUrlException(string url)
            : base()
        {
            Url = url;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url">Requested URL.</param>
        /// <param name="message">Error message.</param>
        public InvalidUrlException(string url, string message)
            : base(message)
        {
            Url = url;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url">Requested URL.</param>
        /// <param name="message">Error message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InvalidUrlException(string url, string message, Exception innerException)
            : base(message, innerException)
        {
            Url = url;
        }
    }
}
