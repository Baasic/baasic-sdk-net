namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// Author used to transfer data from and to controller.
    /// </summary>
    public class PageAuthor : ModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// gets or sets user name.
        /// </summary>
        public string UserName { get; set; }

        #endregion Properties
    }
}