using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Comment used to transfer data from and to controller.
    /// </summary>
    public class Comment : ModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the article id.
        /// </summary>
        public virtual Guid ArticleId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Author.
        /// </summary>
        public virtual string Author
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the comment content.
        /// </summary>
        public virtual string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public virtual string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IsApproved.
        /// </summary>
        public virtual bool IsApproved
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IsSpam.
        /// </summary>
        public virtual bool IsSpam
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public virtual Guid? UserId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Website.
        /// </summary>
        public virtual string Website
        {
            get;
            set;
        }

        #endregion Properties
    }
}