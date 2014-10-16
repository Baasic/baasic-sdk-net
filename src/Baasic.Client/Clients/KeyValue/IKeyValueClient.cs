using Baasic.Client.Model;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.KeyValueModule
{
    /// <summary>
    /// Key Value Module Client.
    /// </summary>
    public interface IKeyValueClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="KeyValue"/> from the system.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>True if <see cref="KeyValue"/> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object key);

        /// <summary>
        /// Asynchronously find <see cref="KeyValue"/> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="KeyValue"/> s.</returns>
        Task<CollectionModelBase<KeyValue>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults, string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="KeyValue"/> by provided key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns><see cref="KeyValue"/> .</returns>
        Task<KeyValue> GetAsync(object key);

        /// <summary>
        /// Asynchronously insert the <see cref="KeyValue"/> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <see cref="KeyValue"/> .</returns>
        Task<KeyValue> InsertAsync(KeyValue content);

        /// <summary>
        /// Asynchronously update the <see cref="KeyValue"/> in the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="content">Resource instance.</param>
        /// <returns>Updated <see cref="KeyValue"/> .</returns>
        Task<KeyValue> UpdateAsync(KeyValue content);

        #endregion Methods
    }
}