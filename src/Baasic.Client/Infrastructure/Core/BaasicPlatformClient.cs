using Baasic.Client.Common.Configuration;
using Baasic.Client.Formatters;

namespace Baasic.Client.Core
{
    public class BaasicPlatformClient : BaasicClient, IBaasicPlatformClient
    {
        #region Constructors

        public BaasicPlatformClient(IPlatformClientConfiguration platformConfiguration, IHttpClientFactory httpClientFactory, IJsonFormatter jsonFormatter)
            : base(platformConfiguration, httpClientFactory, jsonFormatter)
        {
        }

        #endregion Constructors
    }
}