using Baasic.Client.Configuration;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using Baasic.Client.Utility;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.ArticleModule
{
    /// <summary>
    /// Article Tag Module Client.
    /// </summary>
    public class ArticleTagClient : ClientBase, IArticleTagClient
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
            get { return "articletag"; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleTagClient"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public ArticleTagClient(IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory)
            : base(configuration)
        {
            BaasicClientFactory = baasicClientFactory;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Asynchronously removes the <see cref="ArticleTag"/> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="ArticleTag"/> is removed, otherwise false.</returns>
        public virtual Task<bool> DeleteAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously get <see cref="ArticleTag"/> entries.
        /// </summary>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>List of <see cref="ArticleTag"/> .</returns>
        public virtual Task<CollectionModelBase<ArticleTag>> GetAsync(string searchQuery = "",
            int page = DefaultPage, int rpp = MaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed);
                return client.GetAsync<CollectionModelBase<ArticleTag>>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleTag"/> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>If found <see cref="ArticleTag"/> is returned, otherwise null.</returns>
        public virtual Task<ArticleTag> GetAsync(object key, string embed = "")
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.GetAsync<ArticleTag>(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously adds the <see cref="ArticleTag"/> .
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="ArticleTag"/> is returned, otherwise null.</returns>
        public virtual Task<ArticleTag> InsertAsync(ArticleTag entry)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<ArticleTag>(client.GetApiUrl(ModuleRelativePath), entry);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="ArticleTag"/> .
        /// </summary>
        /// <param name="tag">The new or existing <see cref="ArticleTag"/> .</param>
        /// <returns>If tag is updated <see cref="ArticleTag"/> is returned, otherwise null.</returns>
        public virtual Task<ArticleTag> UpdateAsync(ArticleTag tag)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<ArticleTag>(client.GetApiUrl(ModuleRelativePath), tag);
            }
        }

        #endregion Methods
    }
}