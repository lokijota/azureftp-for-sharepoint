namespace AzureFtpForSharePoint.Server.DataContracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// Enumeration that specifies the type of an item.
    /// </summary>
    [DataContract]
    public enum ItemTypeOption
    {
        /// <summary>
        /// Item is a document.
        /// </summary>
        [EnumMember]
        Document = 0,

        /// <summary>
        /// Item is a list item.
        /// </summary>
        [EnumMember]
        ListItem = 1,

        /// <summary>
        /// Item is a folder.
        /// </summary>
        [EnumMember]
        Folder = 2,

        /// <summary>
        /// Item is a list (or document library)
        /// </summary>
        [EnumMember]
        List = 3,

        /// <summary>
        /// Item is a website.
        /// </summary>
        [EnumMember]
        Website = 4
    }
}
