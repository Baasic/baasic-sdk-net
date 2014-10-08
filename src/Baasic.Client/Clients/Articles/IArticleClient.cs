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

        #endregion Methods
    }
}