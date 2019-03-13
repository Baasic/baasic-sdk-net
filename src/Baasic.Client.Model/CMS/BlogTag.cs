namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// Blog tag model.
    /// </summary>
    public class BlogTag : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        /// <value>The slug.</value>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public string Tag { get; set; }

        #endregion Properties
    }
}