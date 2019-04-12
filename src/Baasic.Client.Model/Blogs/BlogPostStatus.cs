namespace Baasic.Client.Model.Blogs
{
    public class BlogPostStatus : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        /// <value>The abbreviation.</value>
        public string Abrv { get; set; }

        /// <summary>
        /// The Description property of the blog post status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Name property of the blog post status.
        /// </summary>
        public string Name { get; set; }

        #endregion Properties
    }
}