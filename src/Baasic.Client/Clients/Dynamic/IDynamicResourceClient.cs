using Baasic.Client.Core;
using Baasic.Client.Model;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Modules.DynamicResource
{
    /// <summary>
    /// Dynamic resource client.
    /// </summary>
    public interface IDynamicResourceClient<T> where T : IModel
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the dynamic resource of <see cref="T" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if dynamic resource is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(SGuid id);

        /// <summary>
        /// Asynchronously find <see cref="T" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="T" /> s.</returns>
        Task<CollectionModelBase<T>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults, string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="T" /> by provided key.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns><see cref="T" /> .</returns>
        Task<T> GetAsync(SGuid id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously insert the <see cref="T" /> into the system.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>Newly created <see cref="T" /> .</returns>
        Task<T> InsertAsync(T resource);

        /// <summary>
        /// Patches the <see cref="T" /> asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="resource">The resource.</param>
        /// <returns>True if updated, false otherwise.</returns>
        Task<bool> PatchAsync<T>(SGuid id, T resource);

        /// <summary>
        /// Patches the <see cref="T" /> asynchronous.
        /// </summary>
        /// <param name="schemaName">The schema name.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="resource">The resource.</param>
        /// <returns>True if updated, false otherwise.</returns>
        Task<bool> PatchAsync<T>(string schemaName, SGuid id, T resource);

        /// <summary>
        /// Asynchronously update the <see cref="T" /> in the system.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>Updated <see cref="T" /> .</returns>
        Task<T> UpdateAsync(T resource);

        #endregion Methods
    }
}