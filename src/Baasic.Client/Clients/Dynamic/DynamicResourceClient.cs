using Baasic.Client.Configuration;
using Baasic.Client.Model;
using Baasic.Client.Utility;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.DynamicResource
{
    /// <summary>
    /// Dynamic resource client.
    /// </summary>
    public class DynamicResourceClient<T> : ClientBase, IDynamicResourceClient<T>
        where T : IModel
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
            get { return "resources"; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicResourceClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public DynamicResourceClient(IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory)
            : base(configuration)
        {
            BaasicClientFactory = baasicClientFactory;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Asynchronously deletes the dynamic resource of <see cref="T" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if dynamic resource is deleted, false otherwise.</returns>
        public virtual Task<bool> DeleteAsync(SGuid id)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{1}/{{0}}", ModuleRelativePath, typeof(T).Name), id));
            }
        }

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
        public virtual async Task<CollectionModelBase<T>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults, string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl("{0}/{1}", ModuleRelativePath, typeof(T).Name));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                CollectionModelBase<T> result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="T" /> by provided key.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns><see cref="T" /> .</returns>
        public virtual Task<T> GetAsync(SGuid id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{1}/{{0}}", ModuleRelativePath, typeof(T).Name), id));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="T" /> into the system.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>Newly created <see cref="T" /> .</returns>
        public virtual Task<T> InsertAsync(T resource)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(client.GetApiUrl("{0}/{1}", ModuleRelativePath, typeof(T).Name), resource);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="T" /> in the system.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>Updated <see cref="T" /> .</returns>
        public virtual Task<T> UpdateAsync(T resource)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T>(client.GetApiUrl("{0}/{1}", ModuleRelativePath, typeof(T).Name), resource);
            }
        }

        #endregion Methods
    }
}