namespace Baasic.Client.Model.Blogs
{
    /// <summary>
    /// Blog post author used to transfer data from and to controller.
    /// </summary>
    public class BlogPostAuthor : ModelBase
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