using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.CMS;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.CMS
{
    /// <summary>
    /// The page file client contract.
    /// </summary>
    public interface IPageFileClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="PageFile" /> from the system.
        /// </summary>
        /// <param name="deleteRequests">The collection of delete requests.</param>
        /// <returns>True if the collection <see cref="PageFile" /> is deleted, false otherwise.</returns>
        Task<bool> BulkDeleteAsync(FileEntryDeleteRequest[] deleteRequests);

        /// <summary>
        /// Asynchronously deletes the <see cref="PageFile" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="PageFile" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Asynchronously find <see cref="PageFile" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="PageFile" /> s.</returns>
        Task<CollectionModelBase<PageFile>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="PageFileEntry" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="pageIds">The page ids.</param>
        /// <param name="fileIds">The file ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="PageFile" /> s.</returns>
        Task<CollectionModelBase<PageFile>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null, string pageIds = null,
            string fileIds = null, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="PageFile" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="pageIds">The page ids.</param>
        /// <param name="fileIds">The file ids.</param>
        /// <param name="tags">The article tags.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null, string pageIds = null,
            string fileIds = null, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields)
            where T : PageFile;

        /// <summary>
        /// Asynchronously gets the <see cref="PageFile" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="PageFile" /> is returned, otherwise null.</returns>
        Task<PageFile> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="PageFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : PageFile;

        /// <summary>
        /// Asynchronously insert the <see cref="PageFile" /> into the system.
        /// </summary>
        /// <param name="pageFile">Resource instance.</param>
        /// <returns>Newly created <see cref="PageFile" /> .</returns>
        Task<PageFile> InsertAsync(PageFile pageFile);

        /// <summary>
        /// Asynchronously insert the <see cref="PageFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="pageFile">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T pageFile) where T : PageFile;

        /// <summary>
        /// Asynchronously insert the collection of <see cref="PageFile" /> into the system.
        /// </summary>
        /// <param name="pageFiles">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="PageFile" /> .</returns>
        Task<PageFile[]> InsertAsync(PageFile[] pageFiles);

        /// <summary>
        /// Asynchronously insert the collection of <see cref="PageFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="pageFiles">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        Task<T[]> InsertAsync<T>(T[] pageFiles) where T : PageFile;

        /// <summary>
        /// Asynchronously update the <see cref="PageFile" /> in the system.
        /// </summary>
        /// <param name="pageFile">Resource instance.</param>
        /// <returns>True if <see cref="PageFile" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync(PageFile pageFile);

        /// <summary>
        /// Asynchronously update the <see cref="PageFile" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="pageFile">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync<T>(T pageFile) where T : PageFile;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="PageFile" /> into the system.
        /// </summary>
        /// <param name="pageFiles">Resource instance.</param>
        /// <returns>Collection of updated <see cref="PageFile" /> .</returns>
        Task<PageFile[]> UpdateAsync(PageFile[] pageFiles);

        /// <summary>
        /// Asynchronously updates the collection of <see cref="PageFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="pageFiles">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task<T[]> UpdateAsync<T>(T[] pageFiles) where T : PageFile;

        #endregion Methods
    }
}