using System;

namespace Baasic.Client.Model.Profile
{
    public partial class Organization : BuiltInModelBase
    {
        #region Class Property Declarations

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual String Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>The slug.</value>
        public virtual String Slug
        {
            get;
            set;
        }

        #endregion Class Property Declarations
    }
}