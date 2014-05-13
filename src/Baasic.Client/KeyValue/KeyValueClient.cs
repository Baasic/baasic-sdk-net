using Baasic.Client.Internals;
using Baasic.Client.Model;
using System;
using System.Threading.Tasks;

namespace Baasic.Client
{
    /// <summary>
    /// Key Value Module Client.
    /// </summary>
    public class KeyValueClient : ClientBase
    {
        #region Properties

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
        public KeyValueClient() :
            this(new StandardFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueClient" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public KeyValueClient(IFactory factory)
            : base(factory)
        {
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
            using (IBaasicClient client = Factory.CreateBaasicClient())
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
            using (IBaasicClient client = Factory.CreateBaasicClient())
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
            using (IBaasicClient client = Factory.CreateBaasicClient())
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
        public virtual Task<KeyValue> PostAsync(KeyValue content)
        {
            using (IBaasicClient client = Factory.CreateBaasicClient())
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
        public virtual Task<KeyValue> PutAsync(KeyValue content)
        {
            using (IBaasicClient client = Factory.CreateBaasicClient())
            {
                return client.PutAsync<KeyValue>(client.GetApiUrl(ModuleRelativePath), content);
            }
        }

        #endregion Methods
    }
}