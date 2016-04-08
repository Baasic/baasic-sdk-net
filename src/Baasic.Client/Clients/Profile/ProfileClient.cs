using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Profile;
using Baasic.Client.Utility;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Modules.Profile
{
    /// <summary>
    /// Profile Module Client.
    /// </summary>
    public class ProfileClient : ClientBase, IProfileClient
    {
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
            get { return "profiles"; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public ProfileClient(IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory)
            : base(configuration)
        {
            BaasicClientFactory = baasicClientFactory;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="UserProfile" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="UserProfile" /> is deleted, false otherwise.</returns>
        public virtual Task<bool> DeleteAsync(object id)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
            }
        }

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
        public virtual async Task<CollectionModelBase<UserProfile>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                var result = await client.GetAsync<CollectionModelBase<UserProfile>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<UserProfile>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="UserProfile" /> by provided id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <returns><see cref="UserProfile" /> .</returns>
        public virtual Task<UserProfile> GetAsync(object id, string embed = DefaultEmbed)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, "");
                return client.GetAsync<UserProfile>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="UserProfile" /> into the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <see cref="UserProfile" /> .</returns>
        public virtual Task<UserProfile> InsertAsync(UserProfile content)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<UserProfile>(client.GetApiUrl(ModuleRelativePath), content);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="UserProfile" /> in the system.
        /// </summary>
        /// <param name="content">Resource instance.</param>
        /// <returns>Updated <see cref="UserProfile" /> .</returns>
        public virtual Task<UserProfile> UpdateAsync(UserProfile content)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<UserProfile>(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, content.Id)), content);
            }
        }

        #endregion Methods
    }
}