namespace Baasic.Client.Model.MediaVault
{
    /// <summary>
    /// File owner model.
    /// </summary>
    /// <seealso cref="Baasic.Client.Model.BuiltInModelBase" />
    public class FileOwner : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the author display name.
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

        #endregion Properties
    }
}