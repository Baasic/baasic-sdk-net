using Baasic.Client.Configuration;
using Baasic.Client.Model;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.KeyValueModule
{
    /// <summary>
    /// Key Value Module Client.
    /// </summary>
    public class KeyValueClient : ClientBase, IKeyValueClient
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
            get { return "KeyValue"; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public KeyValueClient(IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory)
            : base(configuration)
        {
            BaasicClientFactory = baasicClientFactory;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="KeyValue" /> from the system.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>True if <see cref="KeyValue" /> is deleted, false otherwise.</returns>
        public virtual Task<bool> DeleteAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="KeyValue" /> by provided key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns><see cref="KeyValue" />.</returns>
        public virtual Task<KeyValue> GetAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.GetAsync<KeyValue>(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously gets <see cref="KeyValue" />s for provided page.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>List of <see cref="KeyValue" />s.</returns>
        public virtual Task<CollectionModelBase<KeyValue>> GetAsync(string searchQuery = "",
            int page = DefaultPage, int rpp = MaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed);
                return client.GetAsync<CollectionModelBase<KeyValue>>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="KeyValue" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <see cref="KeyValue" />.</returns>
        public virtual Task<KeyValue> InsertAsync(KeyValue content)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<KeyValue>(client.GetApiUrl(ModuleRelativePath), content);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="KeyValue" /> in the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="content">Resource instance.</param>
        /// <returns>Updated <see cref="KeyValue" />.</returns>
        public virtual Task<KeyValue> UpdateAsync(KeyValue content)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<KeyValue>(client.GetApiUrl(ModuleRelativePath), content);
            }
        }

        #endregion Methods
    }
}