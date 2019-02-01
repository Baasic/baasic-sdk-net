using Baasic.Client.Common;
using System;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// The page class.
    /// </summary>
    public class Page : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets author identifier.
        /// </summary>
        /// <value>The author identifier.</value>
        public SGuid AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the language identifier.
        /// </summary>
        /// <value>The language identifier.</value>
        public SGuid? LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the layout.
        /// </summary>
        /// <value>The layout.</value>
        public string Layout { get; set; }

        /// <summary>
        /// The Name property of the Page.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the page status.
        /// </summary>
        /// <value>The page status.</value>
        public PageStatus PageStatus { get; set; }

        /// <summary>
        /// Gets or sets the page status identifier.
        /// </summary>
        /// <value>The page status identifier.</value>
        public SGuid PageStatusId { get; set; }

        /// <summary>
        /// Gets or sets the sort index.
        /// </summary>
        public int SortIndex { get; set; }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public string Template { get; set; }

        /// <summary>
        /// Gets or sets the concurrency time stamp.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL for SEO.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }

        #endregion Properties
    }
}