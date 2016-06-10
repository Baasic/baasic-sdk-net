using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client.Modules.Articles
{
    /// <summary>
    /// Article comment replies client.
    /// </summary>
    public interface IArticleCommentRepliesClient
    {
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
        Task<CollectionModelBase<ArticleCommentReply>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
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
        Task<ArticleCommentReply> GetAsync(SGuid commentId, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Inserts the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        Task<ArticleCommentReply> InsertCommentAsync(CreateArticleCommentReply comment);

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
        Task<ArticleCommentReply> UpdateCommentAsync(ArticleCommentReply comment);
    }
}
