namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// The page status class.
    /// </summary>
    public class PageStatus : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        /// <value>The abbreviation.</value>
        public string Abrv { get; set; }

        /// <summary>
        /// The Json property of the address type.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// The Name property of the address type.
        /// </summary>
        public string Name { get; set; }

        #endregion Properties
    }
}