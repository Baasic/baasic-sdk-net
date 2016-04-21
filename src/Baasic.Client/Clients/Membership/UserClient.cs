using Baasic.Client.Configuration;
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
        /// <param name="userName">Name of the user.</param>
        /// <returns>True if <see cref="User" /> is approved, false otherwise.</returns>
        public virtual Task<bool> ApproveUserAsync(string userName)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/approve/{{0}}", ModuleRelativePath), userName));
            }
        }

        /// <summary>
        /// Asynchronously deletes the <see cref="User" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="User" /> is deleted, false otherwise.</returns>
        public virtual Task<bool> DeleteAsync(object id)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
            }
        }

        /// <summary>
        /// Asynchronously disapproves the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>True if <see cref="User" /> is disapproved, false otherwise.</returns>
        public virtual Task<bool> DisapproveUserAsync(string userName)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/disapprove/{{0}}", ModuleRelativePath), userName));
            }
        }

        /// <summary>
        /// Asynchronously disconnects social login provider from the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns>True if <see cref="User" /> is disconnected from social login provider, false otherwise.</returns>
        public virtual Task<bool> DisconnectSNProviderAsync(string userName, string providerName)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/social-login/{{0}}/{{1}}", ModuleRelativePath), userName, providerName));
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
        public virtual async Task<CollectionModelBase<User>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                var result = await client.GetAsync<CollectionModelBase<User>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<User>();
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
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, "");
                return client.GetAsync<User>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="User" /> into the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <see cref="User" /> .</returns>
        public virtual Task<User> InsertAsync(User content)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<User>(client.GetApiUrl(ModuleRelativePath), content);
            }
        }

        /// <summary>
        /// Asynchronously locks the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>True if <see cref="User" /> is locked, false otherwise.</returns>
        public virtual Task<bool> LockUserAsync(string userName)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}/lock", ModuleRelativePath), userName));
            }
        }

        /// <summary>
        /// Asynchronously unlocks the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>True if <see cref="User" /> is unlocked, false otherwise.</returns>
        public virtual Task<bool> UnlockUserAsync(string userName)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}/unlock", ModuleRelativePath), userName));
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="User" /> in the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>True if <see cref="User" /> is updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync(User content)
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

        /// <summary>
        /// Asynchronously update the <see cref="User" /> password in the system.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="recoveryParams">The recovery parameters.</param>
        /// <returns>True if <see cref="User" /> password is updated, false otherwise.</returns>
        public virtual async Task<bool> UpdatePasswordAsync(string userName, UpdatePasswordDTO recoveryParams)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return await client.PutAsync<UpdatePasswordDTO, bool>(client.GetApiUrl(String.Format("{0}/{1}/change-password", ModuleRelativePath, userName)), recoveryParams);
            }
        }

        #endregion Methods
    }
}