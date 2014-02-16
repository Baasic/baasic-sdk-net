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
    public class KeyValueClient
    {
        #region Fields
        protected const string ClientRelativePath = "KeyValue";
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        public string ApplicationIdentifier { get; protected set; }

        private string _baseAddress;

        public string BaseAddress
        {
            get
            {
                return _baseAddress;
            }
            set
            {
                _baseAddress = value;
            }
        }

        private string _defaultMediaType;

        public string DefaultMediaType
        {
            get
            {
                return _defaultMediaType;
            }
            set { _defaultMediaType = value; }
        }
        
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationIdentifier">Application identifier.</param>
        public KeyValueClient(string applicationIdentifier)
        {
            ApplicationIdentifier = applicationIdentifier;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="baseAddress">Baasic API address.</param>
        /// <param name="applicationIdentifier">Application identifier.</param>
        public KeyValueClient(string baseAddress, string applicationIdentifier)
        {
            ApplicationIdentifier = applicationIdentifier;
            BaseAddress = baseAddress;
        } 
        #endregion

        #region Methods
        public virtual async Task<KeyValue> GetAsync(object key)
        {
            using (BaasicClient client = new BaasicClient(BaseAddress, ApplicationIdentifier))
            {
                var response = await client.GetAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ClientRelativePath), key));
                if (response.IsSuccessStatusCode && response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    //TODO: Add HAL deserialization, is this OK place ?
                    return JsonConvert.DeserializeObject<KeyValue>(await response.Content.ReadAsStringAsync());
                }
                return default(KeyValue);
            }
        }
        #endregion
    }
}
