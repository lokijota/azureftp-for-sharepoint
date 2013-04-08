namespace AzureFtpForSharePoint.Server.Implementation.Mappers
{
    using AzureFtpForSharePoint.Core.SharePointLibrary.DataObjects;
    using AzureFtpForSharePoint.Server.DataContracts;
    using AzureFtpForSharePoint.Server.DataContracts.Parameters;

    /// <summary>
    /// Mapper class to map objects to <see cref="OpenResponse"/> object.
    /// </summary>
    internal static class OpenResponseMapper
    {
        /// <summary>
        /// Maps a <see cref="CurrentContext"/> object to an <see cref="OpenResponse"/> object.
        /// </summary>
        /// <param name="source">Source object.</param>
        /// <returns>Mapped <see cref="OpenResponse"/> object.</returns>
        internal static OpenResponse MapFrom(CurrentContext source)
        {
            return new OpenResponse()
            {
                WebUrl = source.WebUrl,
                FolderUrl = source.FolderUrl
            };
        }
    }
}
