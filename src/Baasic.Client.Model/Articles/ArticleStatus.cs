using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Article service status.
    /// </summary>
    [Flags]
    public enum ArticleStatus
    {
        /// <summary>
        /// The none.
        /// </summary>
        None = 0,

        /// <summary>
        /// The published.
        /// </summary>
        Published = 2,

        /// <summary>
        /// The draft.
        /// </summary>
        Draft = 1,

        /// <summary>
        /// The archive.
        /// </summary>
        Archive = 4
    }
}