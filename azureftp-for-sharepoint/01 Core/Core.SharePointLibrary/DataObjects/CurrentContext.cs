namespace AzureFtpForSharePoint.Core.SharePointLibrary.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Current connection context.
    /// </summary>
    public class CurrentContext
    {
        /// <summary>
        /// Gets or sets the absolute URL for the current website.
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// Gets or sets the server-relative URL for the current folder.
        /// </summary>
        public string FolderUrl { get; set; }
    }
}
