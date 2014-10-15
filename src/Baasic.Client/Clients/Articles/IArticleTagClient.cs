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
        /// Asynchronously removes the <see cref="ArticleTag"/> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="ArticleTag"/> is removed, otherwise false.</returns>
        Task<bool> DeleteAsync(object key);

        /// <summary>
        /// Asynchronously get <see cref="ArticleTag"/> entries.
        /// </summary>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>List of <see cref="ArticleTag"/> .</returns>
        Task<CollectionModelBase<ArticleTag>> GetAsync(string searchQuery,
            int page, int rpp,
            string sort, string embed);

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleTag"/> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>If found <see cref="ArticleTag"/> is returned, otherwise null.</returns>
        Task<ArticleTag> GetAsync(object key, string embed = "");

        /// <summary>
        /// Asynchronously adds the <see cref="ArticleTag"/> .
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="ArticleTag"/> is returned, otherwise null.</returns>
        Task<ArticleTag> InsertAsync(ArticleTag entry);

        /// <summary>
        /// Asynchronously update the <see cref="ArticleTag"/> .
        /// </summary>
        /// <param name="tag">The new or existing <see cref="ArticleTag"/> .</param>
        /// <returns>If tag is updated <see cref="ArticleTag"/> is returned, otherwise null.</returns>
        Task<ArticleTag> UpdateAsync(ArticleTag tag);

        #endregion Methods
    }
}