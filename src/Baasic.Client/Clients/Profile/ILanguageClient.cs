using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Profile;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.Profile
{
    /// <summary>
    /// The language client contract.
    /// </summary>
    public interface ILanguageClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="Language" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="Language" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Asynchronously find <see cref="Language" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Language" /> s.</returns>
        Task<CollectionModelBase<Language>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="Language" /> s.
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
        /// <returns>List of <see cref="Language" /> s.</returns>
        Task<CollectionModelBase<Language>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="Language" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Language" />.</typeparam>
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
            where T : Language;

        /// <summary>
        /// Asynchronously gets the <see cref="Language" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="Language" /> is returned, otherwise null.</returns>
        Task<Language> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="Language" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Language" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : Language;

        /// <summary>
        /// Asynchronously insert the <see cref="Language" /> into the system.
        /// </summary>
        /// <param name="language">Resource instance.</param>
        /// <returns>Newly created <see cref="Language" /> .</returns>
        Task<Language> InsertAsync(Language language);

        /// <summary>
        /// Asynchronously insert the <see cref="Language" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Language" />.</typeparam>
        /// <param name="language">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T language) where T : Language;

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Language" /> into the system.
        /// </summary>
        /// <param name="language">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="Language" /> .</returns>
        Task<Language[]> InsertAsync(Language[] language);

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Language" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Language" />.</typeparam>
        /// <param name="languages">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        Task<T[]> InsertAsync<T>(T[] languages) where T : Language;

        /// <summary>
        /// Asynchronously update the <see cref="Language" /> in the system.
        /// </summary>
        /// <param name="language">Resource instance.</param>
        /// <returns>True if <see cref="Language" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync(Language language);

        /// <summary>
        /// Asynchronously update the <see cref="Language" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Language" />.</typeparam>
        /// <param name="language">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync<T>(T language) where T : Language;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Language" /> into the system.
        /// </summary>
        /// <param name="languages">Resource instance.</param>
        /// <returns>Collection of updated <see cref="Language" /> .</returns>
        Task<Language[]> UpdateAsync(Language[] languages);

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Language" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Language" />.</typeparam>
        /// <param name="languages">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task<T[]> UpdateAsync<T>(T[] languages) where T : Language;

        #endregion Methods
    }
}