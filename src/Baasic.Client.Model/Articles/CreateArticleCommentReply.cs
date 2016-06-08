using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Create comment used to transfer data to create comment DTO.
    /// </summary>
    public class CreateArticleCommentReply : ArticleCommentReply
    {
        #region Properties

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        public ArticleCommentOptions Options { get; set; }

        #endregion Properties
    }
}