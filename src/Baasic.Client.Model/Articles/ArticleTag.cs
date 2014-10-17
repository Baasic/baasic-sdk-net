using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Tag used to transfer data from and to controller.
    /// </summary>
    public class ArticleTag : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the tag slug.
        /// </summary>
        public virtual string Slug
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tag sort order.
        /// </summary>
        public virtual int SortOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        public virtual string Tag
        {
            get;
            set;
        }

        #endregion Properties
    }
}