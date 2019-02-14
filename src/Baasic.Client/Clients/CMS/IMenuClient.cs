using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.CMS;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.CMS
{
    /// <summary>
    /// The menu client contract.
    /// </summary>
    public interface IMenuClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="Menu" /> from the system.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        /// <returns>True if the collection <see cref="Menu" /> is deleted, false otherwise.</returns>
        Task<bool> BulkDeleteAsync(object ids);

        /// <summary>
        /// Asynchronously deletes the <see cref="Menu" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="Menu" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Asynchronously find <see cref="Menu" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Menu" /> s.</returns>
        Task<CollectionModelBase<Menu>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="Menu" /> s.
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
        /// <returns>List of <see cref="Menu" /> s.</returns>
        Task<CollectionModelBase<Menu>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="Menu" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
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
            where T : Menu;

        /// <summary>
        /// Asynchronously gets the <see cref="Menu" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="Menu" /> is returned, otherwise null.</returns>
        Task<Menu> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="Menu" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : Menu;

        /// <summary>
        /// Asynchronously insert the <see cref="Menu" /> into the system.
        /// </summary>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Newly created <see cref="Menu" /> .</returns>
        Task<Menu> InsertAsync(Menu menu);

        /// <summary>
        /// Asynchronously insert the <see cref="Menu" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="menu">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T menu) where T : Menu;

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Menu" /> into the system.
        /// </summary>
        /// <param name="menus">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="Menu" /> .</returns>
        Task<Menu[]> InsertAsync(Menu[] menus);

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Menu" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="menus">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        Task<T[]> InsertAsync<T>(T[] menus) where T : Menu;

        /// <summary>
        /// Asynchronously update the <see cref="Menu" /> in the system.
        /// </summary>
        /// <param name="page">Resource instance.</param>
        /// <returns>True if <see cref="Menu" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync(Menu menu);

        /// <summary>
        /// Asynchronously update the <see cref="Menu" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="menu">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync<T>(T menu) where T : Menu;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Menu" /> into the system.
        /// </summary>
        /// <param name="menus">Resource instance.</param>
        /// <returns>Collection of updated <see cref="Menu" /> .</returns>
        Task<Menu[]> UpdateAsync(Menu[] menus);

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Menu" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="menus">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task<T[]> UpdateAsync<T>(T[] menus) where T : Menu;

        #endregion Methods
    }
}