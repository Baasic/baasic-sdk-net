using Baasic.Client.Common;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Tag entry used to transfer data from and to controller.
    /// </summary>
    public class ArticleTagEntry : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the article identifier.
        /// </summary>
        /// <value>The article identifier.</value>
        public virtual SGuid ArticleId
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
        public virtual SGuid TagId
        {
            get;
            set;
        }

        #endregion Properties
    }
}