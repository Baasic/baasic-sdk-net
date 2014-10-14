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
        /// Asynchronously removes the <see cref="Tag"/> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Tag"/> is removed, otherwise false.</returns>
        public virtual Task<bool> DeleteAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously get <see cref="Tag"/> entries.
        /// </summary>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>List of <see cref="Tag"/> .</returns>
        public virtual Task<CollectionModelBase<Tag>> GetAsync(string searchQuery = "",
            int page = DefaultPage, int rpp = MaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed);
                return client.GetAsync<CollectionModelBase<Tag>>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="Tag"/> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>If found <see cref="Tag"/> is returned, otherwise null.</returns>
        public virtual Task<Tag> GetAsync(object key, string embed = "")
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.GetAsync<Tag>(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously adds the <see cref="Tag"/> .
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="Tag"/> is returned, otherwise null.</returns>
        public virtual Task<Tag> InsertAsync(Tag entry)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<Tag>(client.GetApiUrl(ModuleRelativePath), entry);
            }
        }

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="articleId">Tag will be added under the specified article id.</param>
        /// <param name="tag">The new or existing tag.</param>
        /// <returns>If tag is added <see cref="TagEntry"/> is returned, otherwise null.</returns>
        public virtual Task<Tag> UpdateAsync(Tag tag)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<Tag>(client.GetApiUrl(ModuleRelativePath), tag);
            }
        }

        #endregion Methods
    }
}