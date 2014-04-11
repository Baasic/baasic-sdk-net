using Baasic.Client.Configuration;
using Baasic.Client.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client
{
    /// <summary>
    /// Key Value Module Client.
    /// </summary>
    public class KeyValueClient : ClientBase
    {
        #region Properties

        protected override string ModuleRelativePath
        {
            get { return "KeyValue"; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Client configuration.</param>
        public KeyValueClient(IClientConfiguration configuration)
            : base(configuration)
        {
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="KeyValue"/> from the system.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>True if <see cref="KeyValue"/> is deleted, false otherwise.</returns>
        public virtual Task<bool> DeleteAsync(object key)
        {
            using (BaasicClient client = new BaasicClient(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="KeyValue"/> by provided key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns><see cref="KeyValue"/>.</returns>
        public virtual Task<KeyValue> GetAsync(object key)
        {
            using (BaasicClient client = new BaasicClient(Configuration))
            {
                return client.GetAsync<KeyValue>(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously gets <see cref="KeyValue"/>s for provided page.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>List of <see cref="KeyValue"/>s.</returns>
        public virtual Task<CollectionModelBase<KeyValue>> GetAsync(string searchQuery = "",
            int page = DefaultPage, int rpp = MaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed)
        {
            using (BaasicClient client = new BaasicClient(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed);
                return client.GetAsync<CollectionModelBase<KeyValue>>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="KeyValue"/> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <see cref="KeyValue"/>.</returns>
        public virtual Task<KeyValue> PostAsync(KeyValue content)
        {
            using (BaasicClient client = new BaasicClient(Configuration))
            {
                return client.PostAsync<KeyValue>(client.GetApiUrl(ModuleRelativePath), content);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="KeyValue"/> in the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="content">Resource instance.</param>
        /// <returns>Updated <see cref="KeyValue"/>.</returns>
        public virtual Task<KeyValue> PutAsync(KeyValue content)
        {
        }

        #endregion Methods
    }
}