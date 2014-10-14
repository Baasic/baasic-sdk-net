using Baasic.Client.Configuration;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using Baasic.Client.Utility;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baasic.Client.ArticleModule
{
    /// <summary>
    /// Article Module Client.
    /// </summary>
    public class ArticleClient : ClientBase, IArticleClient
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
            get { return "article"; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleClient"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public ArticleClient(IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory)
            : base(configuration)
        {
            BaasicClientFactory = baasicClientFactory;
        }

        #endregion Constructor

        #region Methods

        #region Article

        /// <summary>
        /// Asynchronously archives the <see cref="Article"/> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article"/> is archived, false otherwise.</returns>
        public virtual async Task<bool> ArchiveAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/archive/{{0}}", ModuleRelativePath), key));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Asynchronously deletes the <see cref="Article"/> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article"/> is deleted, false otherwise.</returns>
        public virtual Task<bool> DeleteAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="Article"/> by provided key.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">The embed.</param>
        /// <returns><see cref="Article"/> .</returns>
        public virtual Task<Article> GetAsync(object key, string embed = "")
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.GetAsync<Article>(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously gets <see cref="Article"/> s for provided page.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>List of <see cref="Article"/> s.</returns>
        public virtual Task<CollectionModelBase<Article>> GetAsync(string searchQuery = "",
            int page = DefaultPage, int rpp = MaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed);
                return client.GetAsync<CollectionModelBase<Article>>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="Article"/> into the system.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns>Newly created <see cref="Article"/> .</returns>
        public virtual Task<Article> InsertAsync(Article article)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<Article>(client.GetApiUrl(ModuleRelativePath), article);
            }
        }

        /// <summary>
        /// Asynchronously publish the <see cref="Article"/> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article"/> is published, false otherwise.</returns>
        public virtual async Task<bool> PublishAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/publish/{{0}}", ModuleRelativePath), key));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Asynchronously restore the <see cref="Article"/> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article"/> is restored, false otherwise.</returns>
        public virtual async Task<bool> RestoreAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/restore/{{0}}", ModuleRelativePath), key));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="Article"/> in the system.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns>Updated <see cref="Article"/> .</returns>
        public virtual Task<Article> UpdateAsync(Article article)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<Article>(client.GetApiUrl(ModuleRelativePath), article);
            }
        }

        #endregion Article

        #region Tags

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="articleId">Tag will be added under the specified article id.</param>
        /// <param name="tag">The new or existing tag.</param>
        /// <returns>If tag is added <see cref="TagEntry"/> is returned, otherwise null.</returns>
        public virtual Task<TagEntry> AddTagToArticleAsync(Guid articleId, string tag)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<TagEntry>(client.GetApiUrl(String.Format("{0}/tags/article/{{0}}/tag/{{1}}", ModuleRelativePath), articleId, tag), null);
            }
        }

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="TagEntry"/> is returned, otherwise null.</returns>
        public virtual Task<TagEntry> AddTagToArticleAsync(TagEntry entry)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<TagEntry>(client.GetApiUrl(String.Format("{0}/tags/", ModuleRelativePath)), entry);
            }
        }

        /// <summary>
        /// Asynchronously get <see cref="TagEntry"/> entries.
        /// </summary>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>List of <see cref="TagEntry"/> .</returns>
        public virtual Task<CollectionModelBase<TagEntry>> GetTagEntriesAsync(string searchQuery = "",
            int page = DefaultPage, int rpp = MaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/tags", ModuleRelativePath)));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed);
                return client.GetAsync<CollectionModelBase<TagEntry>>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="TagEntry"/> from the system.
        /// </summary>
        /// <param name="articleId">Article id.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>If found <see cref="TagEntry"/> is returned, otherwise null.</returns>
        public virtual Task<TagEntry> GetTagEntryAsync(Guid articleId, object key, string embed = "")
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.GetAsync<TagEntry>(client.GetApiUrl(String.Format("{0}/tags/article/{{0}}/tag/{{1}}", ModuleRelativePath), articleId, key));
            }
        }

        /// <summary>
        /// Asynchronously removes all <see cref="TagEntry"/> from the system.
        /// </summary>
        /// <param name="articleId">Article id used to remove tags.</param>
        /// <returns>True if <see cref="TagEntry"/> s are removed, false otherwise.</returns>
        public virtual Task<bool> RemoveAllTagsFromArticleAsync(Guid articleId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/tags/article/{{0}}", ModuleRelativePath), articleId));
            }
        }

        /// <summary>
        /// Asynchronously removes the <see cref="TagEntry"/> from the system.
        /// </summary>
        /// <param name="articleId">Article id to used to remove tag.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="TAgEntry"/> is removed, otherwise false.</returns>
        public virtual Task<bool> RemoveTagFromArticleAsync(Guid articleId, object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/tags/article/{{0}}/tag/{{1}}", ModuleRelativePath), articleId, key));
            }
        }

        #endregion Tags

        #endregion Methods
    }
}