using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Comment options used to transfer options to comments endpoint.
    /// </summary>
    public class ArticleCommentOptions
    {
        #region Properties

        /// <summary>
        /// Gets or sets the comment URL.
        /// </summary>
        /// <value>The comment URL.</value>
        public string CommentUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [subscribe author].
        /// </summary>
        /// <value><c>true</c> if [subscribe author]; otherwise, <c>false</c>.</value>
        public bool SubscribeAuthor { get; set; }

        #endregion Properties
    }
}