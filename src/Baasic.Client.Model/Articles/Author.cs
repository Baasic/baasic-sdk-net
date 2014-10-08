using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Author used to transfer data from and to controller.
    /// </summary>
    public class Author : ModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the author display name.
        /// </summary>
        public virtual string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the author id.
        /// </summary>
        public new System.Guid Id
        {
            get;
            set;
        }

        #endregion Properties
    }
}