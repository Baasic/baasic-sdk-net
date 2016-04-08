using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Membership;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Membership
{
    /// <summary>
    /// Membership Module Role Client.
    /// </summary>
    public interface IRoleClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="Role" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="Role" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Asynchronously find <see cref="Role" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Role" /> s.</returns>
        Task<CollectionModelBase<Role>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults, string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="Role" /> by provided id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <returns><see cref="Role" /> .</returns>
        Task<Role> GetAsync(object id, string embed = ClientBase.DefaultEmbed);

        /// <summary>
        /// Asynchronously insert the <see cref="Role" /> into the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <see cref="Role" /> .</returns>
        Task<Role> InsertAsync(Role content);

        /// <summary>
        /// Asynchronously update the <see cref="Role" /> in the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>Updated <see cref="Role" /> .</returns>
        Task<Role> UpdateAsync(Role content);

        #endregion Methods
    }
}