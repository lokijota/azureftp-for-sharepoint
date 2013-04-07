namespace AzureFtpForSharePoint.Server.DataContracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// Class that represents an item
    /// </summary>
    [DataContract]
    [KnownType(typeof(ItemTypeOption))]
    public class FolderItem
    {
        /// <summary>
        /// Gets or sets the type of the item.
        /// </summary>
        [DataMember]
        public ItemTypeOption ItemType { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the item.
        /// </summary>
        [DataMember]
        public string UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the local ID of the item (for list items)
        /// </summary>
        [DataMember]
        public int LocalId { get; set; }

        /// <summary>
        /// Gets or sets the item's title.
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the item size (only for binaries)
        /// </summary>
        [DataMember]
        public int Size { get; set; }
    }
}
