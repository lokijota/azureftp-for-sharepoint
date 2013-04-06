namespace AzureFtpForSharePoint.Server.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class that holds the information stored for each FTP session.
    /// </summary>
    internal class SessionData
    {
        /// <summary>
        /// Get or sets the username for SharePoint authentication.
        /// </summary>
        /// <value>The username used to authenticate on SharePoint.</value>
        internal string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for SharePoint authentication.
        /// </summary>
        /// <value>The password used to authenticate on SharePoint.</value>
        internal string Password { get; set; }

        /// <summary>
        /// Gets or sets the current web URL, for context.
        /// </summary>
        /// <value>Current web URL.</value>
        internal string WebUrl { get; set; }

        /// <summary>
        /// Gets or sets the current list name, for context.
        /// </summary>
        /// <value>Current list name.</value>
        internal string ListName { get; set; }
    }
}
