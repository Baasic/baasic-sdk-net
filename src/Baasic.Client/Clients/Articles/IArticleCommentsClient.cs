using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Modules.Articles
{
    /// <summary>
    /// Article comments client.
    /// </summary>
    public interface IArticleCommentsClient
    {
        #region Methods

        /// <summary>
        /// Approves the asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<bool> ApproveAsync(SGuid commentId, ArticleCommentOptions options);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(SGuid commentId);

        /// <summary>
        /// Finds the comments asynchronous.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="statuses">The statuses.</param>
        /// <param name="page">The page.</param>
        /// <param name="rpp">The RPP.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        Task<CollectionModelBase<ArticleComment>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            string statuses = "", int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Flags the asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> FlagAsync(SGuid commentId);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        Task<ArticleComment> GetAsync(SGuid commentId, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Inserts the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        Task<ArticleComment> InsertCommentAsync(CreateArticleComment comment);

        /// <summary>
        /// Marks as spam asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> MarkAsSpamAsync(SGuid commentId);

        /// <summary>
        /// Reports the comment asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<bool> ReportCommentAsync(SGuid commentId, ArticleCommentOptions options);

        /// <summary>
        /// Uns the approve asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> UnApproveAsync(SGuid commentId);

        /// <summary>
        /// Uns the flag asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> UnFlagAsync(SGuid commentId);

        /// <summary>
        /// Uns the mark as spam asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> UnMarkAsSpamAsync(SGuid commentId);

        /// <summary>
        /// Uns the report asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> UnReportAsync(SGuid commentId);

        /// <summary>
        /// Updates the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        Task<ArticleComment> UpdateCommentAsync(ArticleComment comment);

        #endregion Methods
    }
}