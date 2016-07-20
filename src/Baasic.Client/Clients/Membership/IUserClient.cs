using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Membership;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Membership
{
    /// <summary>
    /// Membership Module User Client.
    /// </summary>
    public interface IUserClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously approves the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>True if <see cref="User" /> is approved, false otherwise.</returns>
        Task<bool> ApproveUserAsync(string userName);

        /// <summary>
        /// Asynchronously deletes the <see cref="User" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="User" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Asynchronously disapproves the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>True if <see cref="User" /> is disapproved, false otherwise.</returns>
        Task<bool> DisapproveUserAsync(string userName);

        /// <summary>
        /// Asynchronously disconnects social login provider from the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns>True if <see cref="User" /> is disconnected from social login provider, false otherwise.</returns>
        Task<bool> DisconnectSNProviderAsync(string userName, string providerName);

        /// <summary>
        /// Asynchronously find <see cref="User" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="User" /> s.</returns>
        Task<CollectionModelBase<User>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults, string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="User" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="User" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <typeparamref name="T" /> s.</returns>
        Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : User;

        /// <summary>
        /// Asynchronously gets the <see cref="User" /> by provided id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <returns><see cref="User" /> .</returns>
        Task<User> GetAsync(object id, string embed = ClientBase.DefaultEmbed);

        /// <summary>
        /// Asynchronously gets the <see cref="User" /> by provided id.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="User" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <returns><typeparamref name="T" /> .</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed) where T : User;

        /// <summary>
        /// Asynchronously insert the <see cref="NewUser" /> into the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <see cref="NewUser" /> .</returns>
        Task<NewUser> InsertAsync(NewUser content);

        /// <summary>
        /// Asynchronously insert the <see cref="User" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="User" />.</typeparam>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T content) where T : NewUser;

        /// <summary>
        /// Asynchronously locks the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>True if <see cref="User" /> is locked, false otherwise.</returns>
        Task<bool> LockUserAsync(string userName);

        /// <summary>
        /// Asynchronously unlocks the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>True if <see cref="User" /> is unlocked, false otherwise.</returns>
        Task<bool> UnlockUserAsync(string userName);

        /// <summary>
        /// Asynchronously update the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>True if <see cref="User" /> is updated, false otherwise.</returns>
        Task<bool> UpdateAsync(User content);

        /// <summary>
        /// Asynchronously update the <see cref="User" /> password in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="recoveryParams">The recovery parameters.</param>
        /// <returns>True if <see cref="User" /> password is updated, false otherwise.</returns>
        Task<bool> UpdatePasswordAsync(string userName, UpdatePasswordDTO recoveryParams);

        #endregion Methods
    }
}