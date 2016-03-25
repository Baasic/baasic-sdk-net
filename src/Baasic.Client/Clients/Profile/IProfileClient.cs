using Baasic.Client.Model;
using Baasic.Client.Model.Profile;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.ProfileModule
{
    /// <summary>
    /// Profile Module Client.
    /// </summary>
    public interface IProfileClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="UserProfile" /> from the system.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>True if <see cref="UserProfile" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object key);

        /// <summary>
        /// Asynchronously find <see cref="UserProfile" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="UserProfile" /> s.</returns>
        Task<CollectionModelBase<UserProfile>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults, string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="UserProfile" /> by provided key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns><see cref="UserProfile" /> .</returns>
        Task<KeyValue> GetAsync(object key);

        /// <summary>
        /// Asynchronously insert the <see cref="UserProfile" /> into the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <see cref="UserProfile" /> .</returns>
        Task<UserProfile> InsertAsync(UserProfile content);

        /// <summary>
        /// Asynchronously update the <see cref="KeyValue" /> in the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>Updated <see cref="UserProfile" /> .</returns>
        Task<UserProfile> UpdateAsync(UserProfile content);

        #endregion Methods
    }
}