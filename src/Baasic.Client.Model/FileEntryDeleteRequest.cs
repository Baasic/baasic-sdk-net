namespace Baasic.Client.Model
{
    /// <summary>
    /// The file entry delete request model.
    /// </summary>
    public class FileEntryDeleteRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the file format of the derived entry.
        /// </summary>
        public object FileFormat { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        public string Id { get; set; }

        #endregion Properties
    }
}