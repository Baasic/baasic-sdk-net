using Baasic.Client.Model.Membership;
using System;

namespace Baasic.Client.Model.Profile
{
    public partial class UserProfile : BuiltInModelBase
    {
        #region Class Property Declarations

        /// <summary>
        /// Gets or sets the about my self.
        /// </summary>
        /// <value>The about my self.</value>
        public virtual System.String AboutMySelf
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public virtual System.String Address
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public virtual System.String City
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public virtual System.String Country
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public virtual System.String DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dob.
        /// </summary>
        /// <value>The dob.</value>
        public virtual Nullable<System.DateTime> Dob
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public virtual System.String FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public virtual System.String LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the mobile phone.
        /// </summary>
        /// <value>The mobile phone.</value>
        public virtual System.String MobilePhone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time zone identifier.
        /// </summary>
        /// <value>The time zone identifier.</value>
        public virtual System.String TimeZoneId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the core user.
        /// </summary>
        /// <value>The core user.</value>
        public User User
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the core user educations.
        /// </summary>
        /// <value>The core user educations.</value>
        public System.Collections.Generic.IList<UserEducation> UserEducations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the core user skills.
        /// </summary>
        /// <value>The core user skills.</value>
        public System.Collections.Generic.IList<UserSkill> UserSkills
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the core user works.
        /// </summary>
        /// <value>The core user works.</value>
        public System.Collections.Generic.IList<UserWork> UserWorks
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the web sites.
        /// </summary>
        /// <value>The web sites.</value>
        public virtual System.String WebSites
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>The zip code.</value>
        public virtual System.String ZipCode
        {
            get;
            set;
        }

        #endregion Class Property Declarations
    }
}