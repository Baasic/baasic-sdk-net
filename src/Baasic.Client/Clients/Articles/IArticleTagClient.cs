using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.ArticleModule
{
    /// <summary>
    /// Article Tag Module Client.
    /// </summary>
    public interface IArticleTagClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously removes the <see cref="Tag"/> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Tag"/> is removed, otherwise false.</returns>
        Task<bool> DeleteAsync(object key);

        /// <summary>
        /// Asynchronously get <see cref="Tag"/> entries.
        /// </summary>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>List of <see cref="Tag"/> .</returns>
        Task<CollectionModelBase<Tag>> GetAsync(string searchQuery,
            int page, int rpp,
            string sort, string embed);

        /// <summary>
        /// Asynchronously gets the <see cref="Tag"/> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>If found <see cref="Tag"/> is returned, otherwise null.</returns>
        Task<Tag> GetAsync(object key, string embed = "");

        /// <summary>
        /// Asynchronously adds the <see cref="Tag"/> .
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="Tag"/> is returned, otherwise null.</returns>
        Task<Tag> InsertAsync(Tag entry);

        /// <summary>
        /// Asynchronously update the <see cref="Tag"/> in the system.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>Updated <see cref="Tag"/> .</returns>
        Task<Tag> UpdateAsync(Tag tag);

        #endregion Methods
    }
}