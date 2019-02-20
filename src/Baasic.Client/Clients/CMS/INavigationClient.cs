using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.CMS;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.CMS
{
    /// <summary>
    /// The navigation client contract.
    /// </summary>
    public interface INavigationClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="Navigation" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="Navigation" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Asynchronously deletes the <see cref="Navigation" /> from the system assigned to specific <see cref="Menu" />.
        /// </summary>
        /// <param name="menuId">The <see cref="Menu" /> identifier.</param>
        /// <returns>True if <see cref="Navigation" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteByMenuAsync(object menuId);

        /// <summary>
        /// Asynchronously find <see cref="Navigation" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Navigation" /> s.</returns>
        Task<CollectionModelBase<Navigation>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="Navigation" /> s.
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
        /// <returns>List of <see cref="Navigation" /> s.</returns>
        Task<CollectionModelBase<Navigation>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="Navigation" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
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
            where T : Navigation;

        /// <summary>
        /// Asynchronously gets the <see cref="Navigation" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="Navigation" /> is returned, otherwise null.</returns>
        Task<Navigation> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="Navigation" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : Navigation;

        /// <summary>
        /// Asynchronously insert the <see cref="Navigation" /> into the system.
        /// </summary>
        /// <param name="navigation">Resource instance.</param>
        /// <returns>Newly created <see cref="Navigation" /> .</returns>
        Task<Navigation> InsertAsync(Navigation navigation);

        /// <summary>
        /// Asynchronously insert the <see cref="Navigation" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigation">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T navigation) where T : Navigation;

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="Navigation" /> .</returns>
        Task<Navigation[]> InsertAsync(Navigation[] navigations);

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        Task<T[]> InsertAsync<T>(T[] navigations) where T : Navigation;

        /// <summary>
        /// Asynchronously update the <see cref="Navigation" /> in the system.
        /// </summary>
        /// <param name="page">Resource instance.</param>
        /// <returns>True if <see cref="Navigation" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync(Navigation navigation);

        /// <summary>
        /// Asynchronously update the <see cref="Navigation" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigation">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync<T>(T navigation) where T : Navigation;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of updated <see cref="Navigation" /> .</returns>
        Task<Navigation[]> UpdateAsync(Navigation[] navigations);

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task<T[]> UpdateAsync<T>(T[] navigations) where T : Navigation;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task UpdateMenuNavigationAsync(Guid menuId, Navigation[] navigations);

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task UpdateMenuNavigationAsync<T>(Guid menuId, T[] navigations) where T : Navigation;

        #endregion Methods
    }
}