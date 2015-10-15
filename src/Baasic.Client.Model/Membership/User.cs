using System;
using System.Collections.Generic;

namespace Baasic.Client.Model.Membership
{
    /// <summary>
    /// User used to transfer data from and to controller.
    /// </summary>
    public class User : ModelBase
    {
        #region Properties

        /// <summary>
        /// Gets the date and time when the user was added to the membership data store.
        /// </summary>
        public virtual DateTime CreationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the author display name.
        /// </summary>
        public virtual string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the e-mail address for the membership user.
        /// </summary>
        public virtual string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the author id.
        /// </summary>
        public new SGuid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the membership user can be authenticated.
        /// </summary>
        public virtual bool IsApproved
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the membership user is locked out and unable to be validated.
        /// </summary>
        public virtual bool IsLockedOut
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the log-on name of the membership user.
        /// </summary>
        public virtual string Name
        {
            get { return UserName; }
            set { UserName = value; }
        }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        /// <value>User roles.</value>
        public virtual List<Role> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the log-on name of the membership user.
        /// </summary>
        public virtual string UserName
        {
            get;
            set;
        }

        #endregion Properties
    }
}