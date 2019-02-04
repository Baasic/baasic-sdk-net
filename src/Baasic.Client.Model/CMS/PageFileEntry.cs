using Baasic.Client.Common;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// The page file entry class.
    /// </summary>
    public class PageFileEntry : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the file derived entries.
        /// </summary>
        public DerivedPageFileEntry[] DerivedEntries { get; set; }

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
        /// Gets or sets the json.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        public object MetaData { get; set; }

        /// <summary>
        /// Gets or sets the owner user.
        /// </summary>
        public PageFileOwner OwnerUser { get; set; }

        /// <summary>
        /// Gets or sets the owner user id.
        /// </summary>
        public SGuid? OwnerUserId { get; set; }

        /// <summary>
        /// Gets or sets the Page identifier.
        /// </summary>
        /// <value>The Page identifier.</value>
        public SGuid PageId { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        public SGuid ParentId { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int? Width { get; set; }

        #endregion Properties
    }
}