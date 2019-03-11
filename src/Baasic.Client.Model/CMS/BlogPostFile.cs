using Baasic.Client.Common;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// The page file class.
    /// </summary>
    public class BlogPostFile : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the BlogPost file entry.
        /// </summary>
        /// <value>The BlogPost file entry.</value>
        public BlogPostFileEntry BlogPostFileEntry { get; set; }

        /// <summary>
        /// Gets or sets the BlogPost file entry identifier.
        /// </summary>
        /// <value>The BlogPost file entry identifier.</value>
        public SGuid BlogPostFileEntryId { get; set; }

        /// <summary>
        /// Gets or sets the BlogPost identifier.
        /// </summary>
        /// <value>The BlogPost identifier.</value>
        public SGuid BlogPostId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the JSON object.
        /// </summary>
        public string Json { get; set; }

        #endregion Properties
    }
}