using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model.Articles;
using Baasic.Client.Utility;
using System;
using System.Net;
using System.Threading.Tasks;
using Baasic.Client.Common.Configuration;

namespace Baasic.Client.Modules.Articles
{
    /// <summary>
    /// Article Settings Module Client.
    /// </summary>
    public class ArticleSettingsClient : ClientBase, IArticleSettingsClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleSettingsClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public ArticleSettingsClient(IClientConfiguration configuration,
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
            get { return "article-settings"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleSettings" /> from the system.
        /// </summary>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="ArticleSettings" /> is returned, otherwise null.</returns>
        public virtual Task<ArticleSettings> GetAsync(string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetAsync<ArticleSettings>(embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleSettings" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleComment" />.</typeparam>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetAsync<T>(string embed = DefaultEmbed, string fields = DefaultFields) where T : ArticleSettings
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}", ModuleRelativePath)));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="ArticleSettings" /> .
        /// </summary>
        /// <param name="settings">The new or existing <see cref="ArticleSettings" /> .</param>
        /// <returns>True if successfully updated <see cref="ArticleSettings" />.</returns>
        public virtual async Task<bool> UpdateAsync(ArticleSettings settings)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var result = await client.PutAsync<ArticleSettings, HttpStatusCode>(client.GetApiUrl(string.Format("{0}/{1}", ModuleRelativePath, settings.Id)), settings);
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

        #endregion Methods
    }
}