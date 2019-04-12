using Baasic.Client.Common;
using System;
using System.Collections.Generic;

namespace Baasic.Client.Model.Blogs
{
    /// <summary>
    /// Blog post model.
    /// </summary>
    /// <seealso cref="Baasic.Client.Model.BuiltInModelBase" />
    public class BlogPost : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the post author.
        /// </summary>
        /// <value>The post author.</value>
        public BlogPostAuthor Author { get; set; }

        /// <summary>
        /// Gets or sets the author identifier.
        /// </summary>
        /// <value>The author identifier.</value>
        public SGuid AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the blog identifier.
        /// </summary>
        /// <value>The blog identifier.</value>
        public SGuid BlogId { get; set; }

        /// <summary>
        /// Gets or sets the blog post status.
        /// </summary>
        /// <value>The blog post status.</value>
        public BlogPostStatus BlogPostStatus { get; set; }

        /// <summary>
        /// Gets or sets the blog post status identifier.
        /// </summary>
        /// <value>The blog post status identifier.</value>
        public SGuid BlogPostStatusId { get; set; }

        /// <summary>
        /// Gets or sets the blog tags.
        /// </summary>
        /// <value>The blog tags.</value>
        public IEnumerable<BlogTag> BlogTags { get; set; }

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
        public SGuid? FeaturedImageId { get; set; }

        /// <summary>
        /// Gets or sets the publish on.
        /// </summary>
        /// <value>The publish on.</value>
        public DateTime PublishOn { get; set; }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public string Template { get; set; }

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