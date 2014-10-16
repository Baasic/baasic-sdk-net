using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.ArticleModule
{
    /// <summary>
    /// Article Module Client.
    /// </summary>
    public interface IArticleClient
    {
        #region Methods

        #region Article

        /// <summary>
        /// Asynchronously archives the <see cref="Article"/> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article"/> is archived, false otherwise.</returns>
        Task<bool> ArchiveAsync(object key);

        /// <summary>
        /// Asynchronously deletes the <see cref="Article"/> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article"/> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object key);

        /// <summary>
        /// Asynchronously find <see cref="Article"/> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Article"/> s.</returns>
        Task<CollectionModelBase<Article>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults, string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="Article"/> s.
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
        /// <returns>List of <see cref="Article"/> s.</returns>
        Task<CollectionModelBase<Article>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery, DateTime? startDate = null, DateTime? endDate = null,
            string statuses = "", string tags = "", int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults, string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="Article"/> by provided key.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns><see cref="Article"/> .</returns>
        Task<Article> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously insert the <see cref="Article"/> into the system.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns>Newly created <see cref="Article"/> .</returns>
        Task<Article> InsertAsync(Article article);

        /// <summary>
        /// Asynchronously publish the <see cref="Article"/> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article"/> is published, false otherwise.</returns>
        Task<bool> PublishAsync(object key);

        /// <summary>
        /// Asynchronously restore the <see cref="Article"/> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article"/> is restored, false otherwise.</returns>
        Task<bool> RestoreAsync(object key);

        /// <summary>
        /// Asynchronously update the <see cref="Article"/> in the system.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns>Updated <see cref="Article"/> .</returns>
        Task<Article> UpdateAsync(Article article);

        #endregion Article

        #region Tag Entry

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="articleKey">Tag will be added under the specified article id or slug.</param>
        /// <param name="tag">The new or existing tag.</param>
        /// <returns>If tag is added <see cref="ArticleTagEntry"/> is returned, otherwise null.</returns>
        Task<ArticleTagEntry> AddTagToArticleAsync(object articleKey, string tag);

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="ArticleTagEntry"/> is returned, otherwise null.</returns>
        Task<ArticleTagEntry> AddTagToArticleAsync(ArticleTagEntry entry);

        /// <summary>
        /// Asynchronously find <see cref="ArticleTagEntry"/> entries.
        /// </summary>
        /// <param name="articleKey">Article id or slug.</param>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="ArticleTagEntry"/> .</returns>
        Task<CollectionModelBase<ArticleTagEntry>> FindTagEntriesAsync(object articleKey, string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleTagEntry"/> from the system.
        /// </summary>
        /// <param name="articleKey">Article id or slug.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="ArticleTagEntry"/> is returned, otherwise null.</returns>
        Task<ArticleTagEntry> GetTagEntryAsync(object articleKey, object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously removes all <see cref="ArticleTagEntry"/> from the system.
        /// </summary>
        /// <param name="articleKey">Article id or slug used to remove tags.</param>
        /// <returns>True if <see cref="ArticleTagEntry"/> s are removed, false otherwise.</returns>
        Task<bool> RemoveAllTagsFromArticleAsync(object articleKey);

        /// <summary>
        /// Asynchronously removes the <see cref="ArticleTagEntry"/> from the system.
        /// </summary>
        /// <param name="articleKey">Article id or slug to used to remove tag.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="TAgEntry"/> is removed, otherwise false.</returns>
        Task<bool> RemoveTagFromArticleAsync(object articleKey, object key);

        #endregion Tag Entry

        #endregion Methods
    }
}