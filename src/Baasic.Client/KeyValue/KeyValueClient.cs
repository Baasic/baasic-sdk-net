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

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Client configuration.</param>
        public KeyValueClient(IClientConfiguration configuration)
            : base(configuration)
        {
        }
        #endregion

        #region Methods
        public virtual Task<KeyValue> GetAsync(object key)
        {
            using (BaasicClient client = new BaasicClient(Configuration))
            {
                return client.GetAsync<KeyValue>(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

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

        #endregion

    }
}
