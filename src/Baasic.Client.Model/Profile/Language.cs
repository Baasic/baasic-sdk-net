namespace Baasic.Client.Model.Profile
{
    /// <summary>
    /// The language model class. <seealso cref="BuiltInModelBase" />
    /// </summary>
    public class Language : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets slug.
        /// </summary>
        public string Slug { get; set; }

        #endregion Properties
    }
}