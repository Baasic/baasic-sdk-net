using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Tag used to transfer data from and to controller.
    /// </summary>
    public class Tag : ModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

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

        #endregion Properties
    }
}