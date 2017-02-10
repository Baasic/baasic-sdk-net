using System;
using System.Collections.Generic;

namespace Baasic.Client.Model.Membership
{
    /// <summary>
    /// User used to transfer data from and to Baasic.
    /// </summary>
    public class User : BuiltInModelBase
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
        /// Gets or sets the e-mail address for the membership user.
        /// </summary>
        public virtual string Email
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
        /// The LastActivityDate property of the Entity CoreUser <br /><br />
        /// </summary>
        /// <value>The last activity date.</value>
        public virtual System.DateTime LastActivityDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last lockout date.
        /// </summary>
        /// <value>The last lockout date.</value>
        public virtual System.DateTime LastLockoutDate { get; set; }

        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        /// <value>The last login date.</value>
        public virtual System.DateTime LastLoginDate { get; set; }

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