using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Modules.Articles
{
    /// <summary>
    /// Article Module Client.
    /// </summary>
    public interface IArticleClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="articleKey">Tag will be added under the specified article id or slug.</param>
        /// <param name="tag">The new or existing tag.</param>
        /// <returns>If tag is added <see cref="ArticleTagEntry" /> is returned, otherwise null.</returns>
        Task<ArticleTagEntry> AddTagToArticleAsync(object articleKey, string tag);

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="ArticleTagEntry" /> is returned, otherwise null.</returns>
        Task<ArticleTagEntry> AddTagToArticleAsync(ArticleTagEntry entry);

        /// <summary>
        /// Approves the comment.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<bool> ApproveCommentAsync(object articleKey, SGuid commentId, ArticleCommentOptions options);

        /// <summary>
        /// Approves the comment reply.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<bool> ApproveCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId, ArticleCommentOptions options);

        /// <summary>
        /// Asynchronously archives the <see cref="Article" /> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article" /> is archived, false otherwise.</returns>
        Task<bool> ArchiveAsync(object key);

        /// <summary>
        /// Deletes all comment replies asynchronous for the specified comment.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteAllCommentRepliesAsync(object articleKey, SGuid commentId);

        /// <summary>
        /// Deletes all comments related to the specified article including comment replies.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <returns></returns>
        Task<bool> DeleteAllCommentsAsync(object articleKey);

        /// <summary>
        /// Asynchronously deletes the <see cref="Article" /> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object key);

        /// <summary>
        /// Deletes the comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteCommentAsync(object articleKey, SGuid commentId);

        /// <summary>
        /// Deletes the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        Task<bool> DeleteCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId);

        /// <summary>
        /// Asynchronously find <see cref="Article" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Article" /> s.</returns>
        Task<CollectionModelBase<Article>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults, string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="Article" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="statuses">The article statuses.</param>
        /// <param name="tags">The article tags.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Article" /> s.</returns>
        Task<CollectionModelBase<Article>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery, DateTime? startDate = null, DateTime? endDate = null,
            string statuses = "", string tags = "", int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults, string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Finds the comment replies asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="statuses">The statuses.</param>
        /// <param name="page">The page.</param>
        /// <param name="rpp">The RPP.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        Task<CollectionModelBase<ArticleCommentReply>> FindCommentRepliesAsync(object articleKey, SGuid commentId, string searchQuery = ClientBase.DefaultSearchQuery,
            string statuses = "", int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Finds the comments asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="statuses">The statuses.</param>
        /// <param name="page">The page.</param>
        /// <param name="rpp">The RPP.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        Task<CollectionModelBase<ArticleComment>> FindCommentsAsync(object articleKey, string searchQuery = ClientBase.DefaultSearchQuery,
            string statuses = "", int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="ArticleTagEntry" /> entries.
        /// </summary>
        /// <param name="articleKey">Article id or slug.</param>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="ArticleTagEntry" /> .</returns>
        Task<CollectionModelBase<ArticleTagEntry>> FindTagEntriesAsync(object articleKey, string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Flags the comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> FlagCommentAsync(object articleKey, SGuid commentId);

        /// <summary>
        /// Flags the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        Task<bool> FlagCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId);

        /// <summary>
        /// Asynchronously gets the <see cref="Article" /> by provided key.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns><see cref="Article" /> .</returns>
        Task<Article> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Gets the comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        Task<ArticleComment> GetCommentAsync(object articleKey, SGuid commentId, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Gets the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        Task<ArticleCommentReply> GetCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleTagEntry" /> from the system.
        /// </summary>
        /// <param name="articleKey">Article id or slug.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="ArticleTagEntry" /> is returned, otherwise null.</returns>
        Task<ArticleTagEntry> GetTagEntryAsync(object articleKey, object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously insert the <see cref="Article" /> into the system.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns>Newly created <see cref="Article" /> .</returns>
        Task<Article> InsertAsync(Article article);

        /// <summary>
        /// Inserts the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        Task<ArticleComment> InsertCommentAsync(CreateArticleComment comment);

        /// <summary>
        /// Inserts the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        Task<ArticleCommentReply> InsertCommentReplyAsync(object articleKey, CreateArticleCommentReply comment);

        /// <summary>
        /// Marks the comment as spam asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> MarkCommentAsSpamAsync(object articleKey, SGuid commentId);

        /// <summary>
        /// Marks the comment reply as spam asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        Task<bool> MarkCommentReplyAsSpamAsync(object articleKey, SGuid commentId, SGuid commentReplyId);

        /// <summary>
        /// Asynchronously publish the <see cref="Article" /> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article" /> is published, false otherwise.</returns>
        Task<bool> PublishAsync(object key);

        /// <summary>
        /// Asynchronously removes all <see cref="ArticleTagEntry" /> from the system.
        /// </summary>
        /// <param name="articleKey">Article id or slug used to remove tags.</param>
        /// <returns>True if <see cref="ArticleTagEntry" /> s are removed, false otherwise.</returns>
        Task<bool> RemoveAllTagsFromArticleAsync(object articleKey);

        /// <summary>
        /// Asynchronously removes the <see cref="ArticleTagEntry" /> from the system.
        /// </summary>
        /// <param name="articleKey">Article id or slug to used to remove tag.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="TAgEntry" /> is removed, otherwise false.</returns>
        Task<bool> RemoveTagFromArticleAsync(object articleKey, object key);

        /// <summary>
        /// Reports the comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<bool> ReportCommentAsync(object articleKey, SGuid commentId, ArticleCommentOptions options);

        /// <summary>
        /// Reports the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<bool> ReportCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId, ArticleCommentOptions options);

        /// <summary>
        /// Asynchronously restore the <see cref="Article" /> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article" /> is restored, false otherwise.</returns>
        Task<bool> RestoreAsync(object key);

        /// <summary>
        /// Uns the approve comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> UnApproveCommentAsync(object articleKey, SGuid commentId);

        /// <summary>
        /// Uns the approve comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        Task<bool> UnApproveCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId);

        /// <summary>
        /// Uns the flag comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> UnFlagCommentAsync(object articleKey, SGuid commentId);

        /// <summary>
        /// Uns the flag comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        Task<bool> UnFlagCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId);

        /// <summary>
        /// Uns the mark comment as spam asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> UnMarkCommentAsSpamAsync(object articleKey, SGuid commentId);

        /// <summary>
        /// Uns the mark comment reply as spam.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        Task<bool> UnMarkCommentReplyAsSpam(object articleKey, SGuid commentId, SGuid commentReplyId);

        /// <summary>
        /// Uns the report comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> UnReportCommentAsync(object articleKey, SGuid commentId);

        /// <summary>
        /// Uns the report comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        Task<bool> UnReportCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId);

        /// <summary>
        /// Asynchronously update the <see cref="Article" /> in the system.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns>Updated <see cref="Article" /> .</returns>
        Task<Article> UpdateAsync(Article article);

        /// <summary>
        /// Updates the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        Task<ArticleComment> UpdateCommentAsync(ArticleComment comment);

        /// <summary>
        /// Updates the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        Task<ArticleCommentReply> UpdateCommentReplyAsync(object articleKey, ArticleCommentReply comment);

        #endregion Methods
    }
}