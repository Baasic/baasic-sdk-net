using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Rating used to transfer data from and to controller.
    /// </summary>
    public class Rating : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the article id.
        /// </summary>
        public virtual SGuid ArticleId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public virtual SGuid UserId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rating value.
        /// </summary>
        public virtual int Value
        {
            get;
            set;
        }

        #endregion Properties
    }
}