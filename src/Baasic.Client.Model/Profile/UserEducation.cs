using System;

namespace Baasic.Client.Model.Profile
{
    public partial class UserEducation : BuiltInModelBase
    {
        #region Class Property Declarations

        /// <summary>
        /// Gets or sets the degree.
        /// </summary>
        /// <value>The degree.</value>
        public virtual System.String Degree
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public virtual DateTime? EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the grade.
        /// </summary>
        /// <value>The grade.</value>
        public virtual System.String Grade
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>The organization.</value>
        public Organization Organization
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>The organization identifier.</value>
        public virtual Guid? OrganizationId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the organization.
        /// </summary>
        /// <value>The name of the organization.</value>
        public virtual System.String OrganizationName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public virtual DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public virtual System.String Summary
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public virtual System.Guid UserId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user profile.
        /// </summary>
        /// <value>The user profile.</value>
        public UserProfile UserProfile
        {
            get;
            set;
        }

        #endregion Class Property Declarations
    }
}