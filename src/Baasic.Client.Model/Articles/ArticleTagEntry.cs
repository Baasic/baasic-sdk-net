using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Tag entry used to transfer data from and to controller.
    /// </summary>
    public class ArticleTagEntry : ModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the article identifier.
        /// </summary>
        /// <value>The article identifier.</value>
        public virtual Guid ArticleId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public virtual ArticleTag Tag { get; set; }

        /// <summary>
        /// Gets or sets the tag identifier.
        /// </summary>
        /// <value>The tag identifier.</value>
        public virtual Guid TagId
        {
            get;
            set;
        }

        #endregion Properties
    }
}