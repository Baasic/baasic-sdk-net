using Baasic.Client.Common;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Membership;
using Baasic.Client.Utility;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Baasic.Client.Membership
{
    /// <summary>
    /// Membership Module User Client.
    /// </summary>
    public class UserClient : ClientBase, IUserClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public UserClient(IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory)
            : base(configuration)
        {
            BaasicClientFactory = baasicClientFactory;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the baasic client factory.
        /// </summary>
        /// <value>The baasic client factory.</value>
        protected virtual IBaasicClientFactory BaasicClientFactory { get; set; }

        /// <summary>
        /// Gets the module relative path.
        /// </summary>
        protected override string ModuleRelativePath
        {
            get { return "users"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously approves the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="User" /> is approved, false otherwise.</returns>
        public virtual async Task<bool> ApproveUserAsync(object id)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.PutAsync<object, bool>(client.GetApiUrl(String.Format("{0}/{{0}}/approve", ModuleRelativePath), id), new { });
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes the <see cref="User" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="User" /> is deleted, false otherwise.</returns>
        public virtual async Task<bool> DeleteAsync(object id)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously disapproves the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="User" /> is disapproved, false otherwise.</returns>
        public virtual async Task<bool> DisapproveUserAsync(object id)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.PutAsync<object, bool>(client.GetApiUrl(String.Format("{0}/{{0}}/disapprove", ModuleRelativePath), id), new { });
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously disconnects social login provider from the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns>True if <see cref="User" /> is disconnected from social login provider, false otherwise.</returns>
        public virtual async Task<bool> DisconnectSNProviderAsync(string userName, string providerName)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.DeleteAsync(client.GetApiUrl(String.Format("{0}/social-login/{{0}}/{{1}}", ModuleRelativePath), userName, providerName));
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

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
        public virtual Task<CollectionModelBase<User>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<User>(searchQuery, page, rpp, sort, embed, fields);
        }

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
        public virtual async Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields) where T : User
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="User" /> by provided id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <returns><see cref="User" /> .</returns>
        public virtual Task<User> GetAsync(object id, string embed = DefaultEmbed)
        {
            return GetAsync<User>(id, embed);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="User" /> by provided id.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="User" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <returns><typeparamref name="T" /> .</returns>
        public virtual Task<T> GetAsync<T>(object id, string embed = DefaultEmbed) where T : User
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, "");
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="User" /> into the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <see cref="User" /> .</returns>
        public virtual Task<NewUser> InsertAsync(NewUser content)
        {
            return InsertAsync<NewUser>(content);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="User" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="User" />.</typeparam>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T content) where T : NewUser
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(client.GetApiUrl(ModuleRelativePath), content);
            }
        }

        /// <summary>
        /// Asynchronously locks the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="User" /> is locked, false otherwise.</returns>
        public virtual async Task<bool> LockUserAsync(object id)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.PutAsync<object, bool>(client.GetApiUrl(String.Format("{0}/{{0}}/lock", ModuleRelativePath), id), new { });
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously unlocks the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="User" /> is unlocked, false otherwise.</returns>
        public virtual async Task<bool> UnlockUserAsync(object id)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.PutAsync<object, bool>(client.GetApiUrl(String.Format("{0}/{{0}}/unlock", ModuleRelativePath), id), new { });
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>True if <see cref="User" /> is updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync(User content)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    var result = await client.PutAsync<User, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, content.Id)), content);
                    switch (result)
                    {
                        case System.Net.HttpStatusCode.Created:
                        case System.Net.HttpStatusCode.NoContent:
                        case System.Net.HttpStatusCode.OK:
                            return true;

                        default:
                            return false;
                    }
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="User" /> password in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="recoveryParams">The recovery parameters.</param>
        /// <returns>True if <see cref="User" /> password is updated, false otherwise.</returns>
        public virtual async Task<bool> UpdatePasswordAsync(string userName, UpdatePasswordDTO recoveryParams)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.PutAsync<UpdatePasswordDTO, bool>(client.GetApiUrl(String.Format("{0}/{1}/change-password", ModuleRelativePath, userName)), recoveryParams);
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Methods
    }
}