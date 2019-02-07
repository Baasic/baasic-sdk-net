using Baasic.Client.Common;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// The derived page file entry class.
    /// </summary>
    public class DerivedPageFileEntry : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the media vault entry identifier.
        /// </summary>
        public SGuid FileEntryId { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        public long? FileSize { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        public object MetaData { get; set; }

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
        /// Gets or sets the width.
        /// </summary>
        public int? Width { get; set; }

        #endregion Properties
    }
}