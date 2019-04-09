using Baasic.Client.Common;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// The page file class.
    /// </summary>
    public class PageFile : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Page file entry.
        /// </summary>
        /// <value>The Page file entry.</value>
        public PageFileEntry PageFileEntry { get; set; }

        /// <summary>
        /// Gets or sets the Page file entry identifier.
        /// </summary>
        /// <value>The Page file entry identifier.</value>
        public SGuid PageFileEntryId { get; set; }

        /// <summary>
        /// Gets or sets the Page identifier.
        /// </summary>
        /// <value>The Page identifier.</value>
        public SGuid PageId { get; set; }

        #endregion Properties
    }
}