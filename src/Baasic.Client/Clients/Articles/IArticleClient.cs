using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.KeyValueModule
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
        /// Asynchronously gets the <see cref="Article"/> by provided key.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">The embed.</param>
        /// <returns><see cref="Article"/> .</returns>
        Task<Article> GetAsync(object key, string embed);

        /// <summary>
        /// Asynchronously gets <see cref="Article"/> s for provided page.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>List of <see cref="Article"/> s.</returns>
        Task<CollectionModelBase<Article>> GetAsync(string searchQuery, int page, int rpp, string sort, string embed);

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

        #region Tags

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="articleId">Tag will be added under the specified article id.</param>
        /// <param name="tag">The new or existing tag.</param>
        /// <returns>If tag is added <see cref="TagEntry"/> is returned, otherwise null.</returns>
        Task<TagEntry> AddTagToArticleAsync(Guid articleId, string tag);

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="TagEntry"/> is returned, otherwise null.</returns>
        Task<TagEntry> AddTagToArticleAsync(TagEntry entry);

        /// <summary>
        /// Asynchronously get <see cref="TagEntry"/> entries.
        /// </summary>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>List of <see cref="TagEntry"/> .</returns>
        Task<CollectionModelBase<TagEntry>> GetTagEntriesAsync(string searchQuery,
            int page, int rpp,
            string sort, string embed);

        /// <summary>
        /// Asynchronously gets the <see cref="TagEntry"/> from the system.
        /// </summary>
        /// <param name="articleId">Article id.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>If found <see cref="TagEntry"/> is returned, otherwise null.</returns>
        Task<TagEntry> GetTagEntryAsync(Guid articleId, object key, string embed = "");

        /// <summary>
        /// Asynchronously removes all <see cref="TagEntry"/> from the system.
        /// </summary>
        /// <param name="articleId">Article id used to remove tags.</param>
        /// <returns>True if <see cref="TagEntry"/> s are removed, false otherwise.</returns>
        Task<bool> RemoveAllTagsFromArticleAsync(Guid articleId);

        /// <summary>
        /// Asynchronously removes the <see cref="TagEntry"/> from the system.
        /// </summary>
        /// <param name="articleId">Article id to used to remove tag.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="TAgEntry"/> is removed, otherwise false.</returns>
        Task<bool> RemoveTagFromArticleAsync(Guid articleId, object key);

        #endregion Tags

        #endregion Methods
    }
}