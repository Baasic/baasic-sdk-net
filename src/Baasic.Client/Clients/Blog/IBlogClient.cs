using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Blogs;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.Blogs
{
    /// <summary>
    /// Blog client contract.
    /// </summary>
    public interface IBlogClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="Blog" /> from the system.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        /// <returns>True if the collection <see cref="Blog" /> is deleted, false otherwise.</returns>
        Task<bool> BulkDeleteAsync(object ids);

        /// <summary>
        /// Asynchronously deletes the <see cref="Blog" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="Blog" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Asynchronously find <see cref="Blog" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Blog" /> s.</returns>
        Task<CollectionModelBase<Blog>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="Blog" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="languageIds">The language ids.</param>
        /// <param name="abrv">The blog abbrevation.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Blog" /> s.</returns>
        Task<CollectionModelBase<Blog>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null, string languageIds = null,
            string abrv = null, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="Blog" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Blog" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="languageIds">The language ids.</param>
        /// <param name="abrv">The blog abbrevation.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null, string languageIds = null,
            string abrv = null, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields)
            where T : Blog;

        /// <summary>
        /// Asynchronously gets the <see cref="Blog" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="Blog" /> is returned, otherwise null.</returns>
        Task<Blog> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="Blog" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Blog" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : Blog;

        /// <summary>
        /// Asynchronously insert the <see cref="Blog" /> into the system.
        /// </summary>
        /// <param name="blog">The blog.</param>
        /// <returns>Newly created <see cref="Blog" /> .</returns>
        Task<Blog> InsertAsync(Blog blog);

        /// <summary>
        /// Asynchronously insert the <see cref="Blog" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Blog" />.</typeparam>
        /// <param name="blog">The blog.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T blog) where T : Blog;

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Blog" /> into the system.
        /// </summary>
        /// <param name="blogs">The blogs.</param>
        /// <returns>Collection of newly created <see cref="Blog" /> .</returns>
        Task<Blog[]> InsertAsync(Blog[] blogs);

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Blog" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Blog" />.</typeparam>
        /// <param name="blogs">The blogs.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        Task<T[]> InsertAsync<T>(T[] blogs) where T : Blog;

        /// <summary>
        /// Asynchronously update the <see cref="Blog" /> in the system.
        /// </summary>
        /// <param name="blog">The blog.</param>
        /// <returns>True if <see cref="Blog" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync(Blog blog);

        /// <summary>
        /// Asynchronously update the <see cref="Blog" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Blog" />.</typeparam>
        /// <param name="blog">The blog.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync<T>(T blog) where T : Blog;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Blog" /> into the system.
        /// </summary>
        /// <param name="blogs">The blogs.</param>
        /// <returns>Collection of updated <see cref="Blog" /> .</returns>
        Task<Blog[]> UpdateAsync(Blog[] blogs);

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Blog" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Blog" />.</typeparam>
        /// <param name="blogs">The blogs.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task<T[]> UpdateAsync<T>(T[] blogs) where T : Blog;

        #endregion Methods
    }
}