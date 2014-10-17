using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Article used to transfer data from and to controller.
    /// </summary>
    public class Article : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the value indicating if comments are allowed.
        /// </summary>
        public bool AllowComments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the archive date.
        /// </summary>
        public System.DateTime? ArchiveDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the author.
        /// </summary>
        public Author Author
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the author id.
        /// </summary>
        public System.Guid AuthorId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        public Comment[] Comments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the article content.
        /// </summary>
        public virtual string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        public System.DateTime? PublishDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the ratings.
        /// </summary>
        public Rating[] Ratings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        public virtual string Slug
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public virtual int Status
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        public ArticleTag[] Tags
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the article title.
        /// </summary>
        public virtual string Title
        {
            get;
            set;
        }

        #endregion Properties
    }
}