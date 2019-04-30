using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Blogs;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.Blogs
{
    public interface IBlogTagClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="BlogTag" /> from the system.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        /// <returns>True if the collection <see cref="BlogTag" /> is deleted, false otherwise.</returns>
        Task<bool> BulkDeleteAsync(object ids);

        /// <summary>
        /// Asynchronously deletes the <see cref="BlogTag" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="BlogTag" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Asynchronously find <see cref="BlogTag" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogTag" /> s.</returns>
        Task<CollectionModelBase<BlogTag>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="BlogTag" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="languageIds">The language ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogTag" /> s.</returns>
        Task<CollectionModelBase<BlogTag>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="BlogTag" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="languageIds">The language ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields)
            where T : BlogTag;

        /// <summary>
        /// Asynchronously gets the <see cref="BlogTag" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="BlogTag" /> is returned, otherwise null.</returns>
        Task<BlogTag> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="BlogTag" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : BlogTag;

        /// <summary>
        /// Asynchronously insert the <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <param name="BlogTag">The blogTag.</param>
        /// <returns>Newly created <see cref="BlogTag" /> .</returns>
        Task<BlogTag> InsertAsync(BlogTag blogTag);

        /// <summary>
        /// Asynchronously insert the <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="blogTag">The blog tag.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T blogTag) where T : BlogTag;

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <param name="blogTags">The blog tags.</param>
        /// <returns>Collection of newly created <see cref="BlogTag" /> .</returns>
        Task<BatchResult<BlogTag>[]> InsertAsync(BlogTag[] blogTags);

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="blogTags">The blog tags.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        Task<BatchResult<T>[]> InsertAsync<T>(T[] blogTags) where T : BlogTag;

        /// <summary>
        /// Asynchronously update the <see cref="BlogTag" /> in the system.
        /// </summary>
        /// <param name="BlogTag">The blogTag.</param>
        /// <returns>True if <see cref="BlogTag" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync(BlogTag blogTag);

        /// <summary>
        /// Asynchronously update the <see cref="BlogTag" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="blogTag">The blog tag.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync<T>(T blogTag) where T : BlogTag;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <param name="blogTags">The blog tags.</param>
        /// <returns>Collection of updated <see cref="BlogTag" /> .</returns>
        Task<BatchResult<BlogTag>[]> UpdateAsync(BlogTag[] blogTags);

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="blogTags">The blog tags.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task<BatchResult<T>[]> UpdateAsync<T>(T[] blogTags) where T : BlogTag;

        #endregion Methods
    }
}