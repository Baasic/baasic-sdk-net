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
            get { return "articles"; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleClient" /> class.
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
        /// Asynchronously archives the <see cref="Article" /> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article" /> is archived, false otherwise.</returns>
        public virtual async Task<bool> ArchiveAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/archive", ModuleRelativePath), key));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Asynchronously deletes the <see cref="Article" /> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article" /> is deleted, false otherwise.</returns>
        public virtual Task<bool> DeleteAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
            }
        }

        /// <summary>
        /// Asynchronously find <see cref="Article" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Article" /> s.</returns>
        public virtual Task<CollectionModelBase<Article>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync(searchQuery, null, null, "", "", page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="Article" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="statuses">The article statuses.</param>
        /// <param name="tags">The article tags.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Article" /> s.</returns>
        public virtual async Task<CollectionModelBase<Article>> FindAsync(string searchQuery = DefaultSearchQuery,
            DateTime? startDate = null, DateTime? endDate = null,
            string statuses = "", string tags = "",
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "startDate", startDate);
                InitializeQueryStringPair(uriBuilder, "endDate", endDate);
                InitializeQueryStringPair(uriBuilder, "statuses", statuses);
                InitializeQueryStringPair(uriBuilder, "tags", tags);
                var result = await client.GetAsync<CollectionModelBase<Article>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<Article>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="Article" /> by provided key.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns><see cref="Article" /> .</returns>
        public virtual Task<Article> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<Article>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="Article" /> into the system.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns>Newly created <see cref="Article" /> .</returns>
        public virtual Task<Article> InsertAsync(Article article)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<Article>(client.GetApiUrl(ModuleRelativePath), article);
            }
        }

        /// <summary>
        /// Asynchronously publish the <see cref="Article" /> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article" /> is published, false otherwise.</returns>
        public virtual async Task<bool> PublishAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/publish", ModuleRelativePath), key));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Asynchronously restore the <see cref="Article" /> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article" /> is restored, false otherwise.</returns>
        public virtual async Task<bool> RestoreAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/restore", ModuleRelativePath), key));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="Article" /> in the system.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns>Updated <see cref="Article" /> .</returns>
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
        /// <param name="articleKey">Tag will be added under the specified article id or slug.</param>
        /// <param name="tag">The new or existing tag.</param>
        /// <returns>If tag is added <see cref="ArticleTagEntry" /> is returned, otherwise null.</returns>
        public virtual Task<ArticleTagEntry> AddTagToArticleAsync(object articleKey, string tag)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<ArticleTagEntry>(client.GetApiUrl(String.Format("{0}/{{0}}/tags/{{1}}", ModuleRelativePath), articleKey, tag), null);
            }
        }

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="ArticleTagEntry" /> is returned, otherwise null.</returns>
        public virtual Task<ArticleTagEntry> AddTagToArticleAsync(ArticleTagEntry entry)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<ArticleTagEntry>(client.GetApiUrl(String.Format("{0}/tags/", ModuleRelativePath)), entry);
            }
        }

        /// <summary>
        /// Asynchronously find <see cref="ArticleTagEntry" /> entries.
        /// </summary>
        /// <param name="articleKey">Article id or slug.</param>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="ArticleTagEntry" /> .</returns>
        public virtual async Task<CollectionModelBase<ArticleTagEntry>> FindTagEntriesAsync(object articleKey, string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}/tags", ModuleRelativePath), articleKey));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                var result = await client.GetAsync<CollectionModelBase<ArticleTagEntry>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<ArticleTagEntry>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleTagEntry" /> from the system.
        /// </summary>
        /// <param name="articleKey">Article id or slug.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="ArticleTagEntry" /> is returned, otherwise null.</returns>
        public virtual Task<ArticleTagEntry> GetTagEntryAsync(object articleKey, object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}/tags/{{1}}", ModuleRelativePath), articleKey, key));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<ArticleTagEntry>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously removes all <see cref="ArticleTagEntry" /> from the system.
        /// </summary>
        /// <param name="articleKey">Article id or slug used to remove tags.</param>
        /// <returns>True if <see cref="ArticleTagEntry" /> s are removed, false otherwise.</returns>
        public virtual Task<bool> RemoveAllTagsFromArticleAsync(object articleKey)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}/tags", ModuleRelativePath), articleKey));
            }
        }

        /// <summary>
        /// Asynchronously removes the <see cref="ArticleTagEntry" /> from the system.
        /// </summary>
        /// <param name="articleKey">Article id or slug to used to remove tag.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="TAgEntry" /> is removed, otherwise false.</returns>
        public virtual Task<bool> RemoveTagFromArticleAsync(object articleKey, object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}/tags/{{1}}", ModuleRelativePath), articleKey, key));
            }
        }

        #endregion Tags

        #endregion Methods
    }
}