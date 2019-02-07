using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.CMS;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.CMS
{
    /// <summary>
    /// The page status client contract.
    /// </summary>
    public interface IPageStatusClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="PageStatus" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="PageStatus" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Asynchronously find <see cref="PageStatus" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="PageStatus" /> s.</returns>
        Task<CollectionModelBase<PageStatus>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="PageStatus" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="PageStatus" /> s.</returns>
        Task<CollectionModelBase<PageStatus>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="PageStatus" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageStatus" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="tags">The article tags.</param>
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
            where T : PageStatus;

        /// <summary>
        /// Asynchronously gets the <see cref="PageStatus" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="PageStatus" /> is returned, otherwise null.</returns>
        Task<PageStatus> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="PageStatus" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageStatus" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : PageStatus;

        /// <summary>
        /// Asynchronously insert the <see cref="PageStatus" /> into the system.
        /// </summary>
        /// <param name="page">Resource instance.</param>
        /// <returns>Newly created <see cref="PageStatus" /> .</returns>
        Task<PageStatus> InsertAsync(PageStatus page);

        /// <summary>
        /// Asynchronously insert the <see cref="PageStatus" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageStatus" />.</typeparam>
        /// <param name="page">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T page) where T : PageStatus;

        /// <summary>
        /// Asynchronously insert the collection of <see cref="PageStatus" /> into the system.
        /// </summary>
        /// <param name="pages">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="PageStatus" /> .</returns>
        Task<PageStatus[]> InsertAsync(PageStatus[] pages);

        /// <summary>
        /// Asynchronously insert the collection of <see cref="PageStatus" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageStatus" />.</typeparam>
        /// <param name="pages">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        Task<T[]> InsertAsync<T>(T[] pages) where T : PageStatus;

        /// <summary>
        /// Asynchronously update the <see cref="PageStatus" /> in the system.
        /// </summary>
        /// <param name="page">Resource instance.</param>
        /// <returns>True if <see cref="PageStatus" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync(PageStatus page);

        /// <summary>
        /// Asynchronously update the <see cref="PageStatus" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageStatus" />.</typeparam>
        /// <param name="page">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync<T>(T page) where T : PageStatus;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="PageStatus" /> into the system.
        /// </summary>
        /// <param name="pages">Resource instance.</param>
        /// <returns>Collection of updated <see cref="PageStatus" /> .</returns>
        Task<PageStatus[]> UpdateAsync(PageStatus[] pages);

        #endregion Methods
    }
}