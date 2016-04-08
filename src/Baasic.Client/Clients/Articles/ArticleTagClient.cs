using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using Baasic.Client.Utility;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Modules.Articles
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
            get { return "article-tags"; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleTagClient" /> class.
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
        /// Asynchronously removes the <see cref="ArticleTag" /> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="ArticleTag" /> is removed, otherwise false.</returns>
        public virtual Task<bool> DeleteAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously find <see cref="ArticleTag" /> entries.
        /// </summary>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="ArticleTag" /> .</returns>
        public virtual async Task<CollectionModelBase<ArticleTag>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                var result = await client.GetAsync<CollectionModelBase<ArticleTag>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<ArticleTag>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleTag" /> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="ArticleTag" /> is returned, otherwise null.</returns>
        public virtual Task<ArticleTag> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<ArticleTag>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously adds the <see cref="ArticleTag" /> .
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="ArticleTag" /> is returned, otherwise null.</returns>
        public virtual Task<ArticleTag> InsertAsync(ArticleTag entry)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<ArticleTag>(client.GetApiUrl(ModuleRelativePath), entry);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="ArticleTag" /> .
        /// </summary>
        /// <param name="tag">The new or existing <see cref="ArticleTag" /> .</param>
        /// <returns>If tag is updated <see cref="ArticleTag" /> is returned, otherwise null.</returns>
        public virtual Task<ArticleTag> UpdateAsync(ArticleTag tag)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<ArticleTag>(client.GetApiUrl(string.Format("{0}/{1}", ModuleRelativePath, tag.Id)), tag);
            }
        }

        #endregion Methods
    }
}