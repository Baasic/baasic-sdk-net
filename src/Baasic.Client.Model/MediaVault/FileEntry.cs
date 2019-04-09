using Baasic.Client.Common;

namespace Baasic.Client.Model.MediaVault
{
    /// <summary>
    /// File entry model.
    /// </summary>
    /// <seealso cref="Baasic.Client.Model.BuiltInModelBase" />
    public class FileEntry : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the file derived entries.
        /// </summary>
        public DerivedEntry[] DerivedEntries { get; set; }

        /// <summary>
        /// Gets or sets the resource description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        public object MetaData { get; set; }

        /// <summary>
        /// Gets or sets the owner user.
        /// </summary>
        public FileOwner OwnerUser { get; set; }

        /// <summary>
        /// Gets or sets the owner user id.
        /// </summary>
        public SGuid? OwnerUserId { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the policies.
        /// </summary>
        public FilePolicy[] Policies { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int? Width { get; set; }

        #endregion Properties
    }
}