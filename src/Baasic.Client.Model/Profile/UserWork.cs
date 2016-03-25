using System;

namespace Baasic.Client.Model.Profile
{
    public partial class UserWork : BuiltInModelBase
    {
        #region Class Property Declarations

        /// <summary>
        /// Gets or sets the core company.
        /// </summary>
        /// <value>The core company.</value>
        public Company Company { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public Nullable<System.Guid> CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public System.String CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public Nullable<System.DateTime> EndDate { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public System.DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public System.String Summary { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public System.Guid UserId { get; set; }

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