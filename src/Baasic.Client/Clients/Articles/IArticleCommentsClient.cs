using Baasic.Client.Common;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
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
        /// Finds the comments asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleComment" />.</typeparam>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="statuses">The statuses.</param>
        /// <param name="page">The page.</param>
        /// <param name="rpp">The RPP.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns>Collection of <typeparamref name="T" /></returns>
        Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = ClientBase.DefaultSearchQuery,
            string statuses = "", int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : ArticleComment;

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
        /// Gets the comment asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleComment" />.</typeparam>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns><typeparamref name="T" /></returns>
        Task<T> GetAsync<T>(SGuid commentId, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : ArticleComment;

        /// <summary>
        /// Inserts the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        Task<ArticleComment> InsertCommentAsync(CreateArticleComment comment);

        /// <summary>
        /// Asynchronously insert the <see cref="CreateArticleComment" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleComment" />.</typeparam>
        /// <param name="comment">The comment.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertCommentAsync<T>(T comment);

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

        /// <summary>
        /// Updates the comment asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleComment" />.</typeparam>
        /// <param name="comment">The comment.</param>
        /// <returns>Updated <typeparamref name="T" /></returns>
        Task<T> UpdateCommentAsync<T>(T comment) where T : ArticleComment;

        #endregion Methods
    }
}