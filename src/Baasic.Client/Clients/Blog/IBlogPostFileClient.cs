using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Blogs;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.Blogs
{
    /// <summary>
    /// The blogPost file client contract.
    /// </summary>
    public interface IBlogPostFileClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        /// <returns>True if the collection <see cref="BlogPostFile" /> is deleted, false otherwise.</returns>
        Task<bool> BulkDeleteAsync(object ids);

        /// <summary>
        /// Asynchronously deletes the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="BlogPostFile" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Asynchronously find <see cref="BlogPostFile" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogPostFile" /> s.</returns>
        Task<CollectionModelBase<BlogPostFile>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="BlogPostFileEntry" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="blogPostIds">The blog post ids.</param>
        /// <param name="fileIds">The file ids.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogPostFile" /> s.</returns>
        Task<CollectionModelBase<BlogPostFile>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null, string blogPostIds = null, string fileIds = null,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="BlogPostFile" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="blogPostIds">The blog post ids.</param>
        /// <param name="fileIds">The file ids.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null, string blogPostIds = null, string fileIds = null,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields)
            where T : BlogPostFile;

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="BlogPostFile" /> is returned, otherwise null.</returns>
        Task<BlogPostFile> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : BlogPostFile;

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <param name="blogPostFile">Resource instance.</param>
        /// <returns>Newly created <see cref="BlogPostFile" /> .</returns>
        Task<BlogPostFile> InsertAsync(BlogPostFile blogPostFile);

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="blogPostFile">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T blogPostFile) where T : BlogPostFile;

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <param name="blogPostFiles">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="BlogPostFile" /> .</returns>
        Task<BlogPostFile[]> InsertAsync(BlogPostFile[] blogPostFiles);

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="blogPostFiles">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        Task<T[]> InsertAsync<T>(T[] blogPostFiles) where T : BlogPostFile;

        /// <summary>
        /// Asynchronously update the <see cref="BlogPostFile" /> in the system.
        /// </summary>
        /// <param name="blogPostFile">Resource instance.</param>
        /// <returns>True if <see cref="BlogPostFile" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync(BlogPostFile blogPostFile);

        /// <summary>
        /// Asynchronously update the <see cref="BlogPostFile" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="blogPostFile">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync<T>(T blogPostFile) where T : BlogPostFile;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <param name="blogPostFiles">Resource instance.</param>
        /// <returns>Collection of updated <see cref="BlogPostFile" /> .</returns>
        Task<BlogPostFile[]> UpdateAsync(BlogPostFile[] blogPostFiles);

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="blogPostFiles">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task<T[]> UpdateAsync<T>(T[] blogPostFiles) where T : BlogPostFile;

        #endregion Methods
    }
}