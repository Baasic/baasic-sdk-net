using Baasic.Client.Common;
using System;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// Blog post model.
    /// </summary>
    /// <seealso cref="Baasic.Client.Model.BuiltInModelBase" />
    public class BlogPost : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public BlogOwner Author { get; set; }

        /// <summary>
        /// Gets or sets the blog identifier.
        /// </summary>
        /// <value>The blog identifier.</value>
        public SGuid BlogId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [comments allowed].
        /// </summary>
        /// <value><c>true</c> if [comments allowed]; otherwise, <c>false</c>.</value>
        public bool CommentsAllowed { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the featured image identifier.
        /// </summary>
        /// <value>The featured image identifier.</value>
        public SGuid FeaturedImageId { get; set; }

        /// <summary>
        /// Gets or sets the json.
        /// </summary>
        /// <value>The json.</value>
        public string Json { get; set; }

        /// <summary>
        /// Gets or sets the publish on.
        /// </summary>
        /// <value>The publish on.</value>
        public DateTime PublishOn { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public BlogPostStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }

        #endregion Properties
    }
}