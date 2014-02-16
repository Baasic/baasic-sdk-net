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
        /// <param name="applicationIdentifier">Application identifier.</param>
        public KeyValueClient(string applicationIdentifier)
            : base(applicationIdentifier)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="baseAddress">Baasic API address.</param>
        /// <param name="applicationIdentifier">Application identifier.</param>
        public KeyValueClient(string baseAddress, string applicationIdentifier) :
            base(baseAddress, applicationIdentifier)
        {
        }
        #endregion

        #region Methods
        public virtual async Task<KeyValue> GetAsync(object key)
        {
            using (BaasicClient client = new BaasicClient(BaseAddress, ApplicationIdentifier))
            {
                var response = await client.GetAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
                if (response.IsSuccessStatusCode && response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    //TODO: Add HAL deserialization, is this OK place ?
                    return JsonConvert.DeserializeObject<KeyValue>(await response.Content.ReadAsStringAsync());
                }
                return default(KeyValue);
            }
        }

        public virtual async Task<CollectionModelBase<KeyValue>> GetAsync(string searchQuery = "",
            int page = DefaultPage, int rpp = MaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed)
        {
            using (BaasicClient client = new BaasicClient(BaseAddress, ApplicationIdentifier))
            {
                UriBuilder uriBuilder = new UriBuilder(client.GetApiUrl(ModuleRelativePath));
                //TODO: Build full query
                var response = await client.GetAsync(uriBuilder.Uri);
                if (response.IsSuccessStatusCode && response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    //TODO: Add HAL deserialization, is this OK place ?
                    return JsonConvert.DeserializeObject<CollectionModelBase<KeyValue>>(await response.Content.ReadAsStringAsync());
                }
                return new CollectionModelBase<KeyValue>();
            }
        }

        #endregion

    }
}
