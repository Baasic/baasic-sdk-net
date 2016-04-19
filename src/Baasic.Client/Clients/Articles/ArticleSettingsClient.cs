using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model.Articles;
using Baasic.Client.Utility;
using System;
using System.Threading.Tasks;

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
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}", ModuleRelativePath)));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<ArticleSettings>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="ArticleSettings" /> .
        /// </summary>
        /// <param name="settings">The new or existing <see cref="ArticleSettings" /> .</param>
        /// <returns>If tag is updated <see cref="ArticleSettings" /> is returned, otherwise null.</returns>
        public virtual Task<ArticleSettings> UpdateAsync(ArticleSettings settings)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<ArticleSettings>(client.GetApiUrl(string.Format("{0}/{1}", ModuleRelativePath, settings.Id)), settings);
            }
        }

        #endregion Methods
    }
}