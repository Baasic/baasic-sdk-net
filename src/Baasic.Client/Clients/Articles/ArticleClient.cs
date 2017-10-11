using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using Baasic.Client.Utility;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Baasic.Client.Common.Configuration;

namespace Baasic.Client.Modules.Articles
{
    /// <summary>
    /// Article Module Client.
    /// </summary>
    public class ArticleClient : ClientBase, IArticleClient
    {

        #region Constructors

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
            get { return "articles"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="articleKey">Tag will be added under the specified article id or slug.</param>
        /// <param name="tag">The new or existing tag.</param>
        /// <returns>If tag is added <see cref="ArticleTagEntry" /> is returned, otherwise null.</returns>
        public virtual Task<ArticleTagEntry> AddTagToArticleAsync(object articleKey, string tag)
        {
            return AddTagToArticleAsync<ArticleTagEntry>(articleKey, tag);
        }

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleTagEntry" />.</typeparam>
        /// <param name="articleKey">Tag will be added under the specified article id or slug.</param>
        /// <param name="tag">The new or existing tag.</param>
        /// <returns>If tag is added <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> AddTagToArticleAsync<T>(object articleKey, string tag) where T : ArticleTagEntry
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(client.GetApiUrl(String.Format("{0}/{{0}}/tags/{{1}}", ModuleRelativePath), articleKey, tag), null);
            }
        }

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="ArticleTagEntry" /> is returned, otherwise null.</returns>
        public virtual Task<ArticleTagEntry> AddTagToArticleAsync(ArticleTagEntry entry)
        {
            return AddTagToArticleAsync<ArticleTagEntry>(entry);
        }

        /// <summary>
        /// Asynchronously adds the tag to article tags.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleTagEntry" />.</typeparam>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="ArticleTagEntry" /> is returned, otherwise null.</returns>
        public virtual Task<T> AddTagToArticleAsync<T>(T entry) where T : ArticleTagEntry
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(client.GetApiUrl(String.Format("{0}/tags/", ModuleRelativePath)), entry);
            }
        }

        /// <summary>
        /// Approves the comment.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public virtual async Task<bool> ApproveCommentAsync(object articleKey, SGuid commentId, ArticleCommentOptions options)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var result = await client.PutAsync<ArticleCommentOptions, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/approve", ModuleRelativePath), articleKey, commentId), options);
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

        /// <summary>
        /// Approves the comment reply.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public virtual async Task<bool> ApproveCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId, ArticleCommentOptions options)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var result = await client.PutAsync<ArticleCommentOptions, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}/approve", ModuleRelativePath), articleKey, commentId, commentReplyId), options);
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
        /// Deletes all comment replies asynchronous for the specified comment.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual Task<bool> DeleteAllCommentRepliesAsync(object articleKey, SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies", ModuleRelativePath), articleKey, commentId));
            }
        }

        /// <summary>
        /// Deletes all comments related to the specified article including comment replies.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <returns></returns>
        public virtual Task<bool> DeleteAllCommentsAsync(object articleKey)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}/comments", ModuleRelativePath), articleKey));
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
        /// Deletes the comment.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual Task<bool> DeleteCommentAsync(object articleKey, SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}", ModuleRelativePath), articleKey, commentId));
            }
        }

        /// <summary>
        /// Deletes the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        public virtual Task<bool> DeleteCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}", ModuleRelativePath), articleKey, commentId, commentReplyId));
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
        public virtual Task<CollectionModelBase<Article>> FindAsync(string searchQuery = DefaultSearchQuery,
            DateTime? startDate = null, DateTime? endDate = null,
            string statuses = "", string tags = "",
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<Article>(searchQuery, startDate, endDate, statuses, tags, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="Article" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Article" />.</typeparam>
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
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        public virtual async Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = DefaultSearchQuery,
            DateTime? startDate = null, DateTime? endDate = null,
            string statuses = "", string tags = "",
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : Article
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "startDate", startDate);
                InitializeQueryStringPair(uriBuilder, "endDate", endDate);
                InitializeQueryStringPair(uriBuilder, "statuses", statuses);
                InitializeQueryStringPair(uriBuilder, "tags", tags);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously finds the comment replies.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="statuses">The statuses.</param>
        /// <param name="page">The page.</param>
        /// <param name="rpp">The RPP.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        public virtual Task<CollectionModelBase<ArticleCommentReply>> FindCommentRepliesAsync(object articleKey, SGuid commentId, string searchQuery = DefaultSearchQuery,
            string statuses = "", int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindCommentRepliesAsync<ArticleCommentReply>(articleKey, commentId, searchQuery, statuses, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously finds the comment replies.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleCommentReply" />.</typeparam>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="statuses">The statuses.</param>
        /// <param name="page">The page.</param>
        /// <param name="rpp">The RPP.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns>Collection of <typeparamref name="T" />.</returns>
        public virtual async Task<CollectionModelBase<T>> FindCommentRepliesAsync<T>(object articleKey, SGuid commentId, string searchQuery = DefaultSearchQuery,
            string statuses = "", int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : ArticleCommentReply
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies", ModuleRelativePath), articleKey, commentId));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "statuses", statuses);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Finds the comments asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="statuses">The statuses.</param>
        /// <param name="page">The page.</param>
        /// <param name="rpp">The RPP.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns>Collection of <see cref="ArticleComment" />.</returns>
        public virtual Task<CollectionModelBase<ArticleComment>> FindCommentsAsync(object articleKey, string searchQuery = DefaultSearchQuery,
            string statuses = "", int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindCommentsAsync<ArticleComment>(articleKey, searchQuery, statuses, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Finds the comments asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="statuses">The statuses.</param>
        /// <param name="page">The page.</param>
        /// <param name="rpp">The RPP.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns>Collection of <typeparamref name="T" />.</returns>
        public virtual async Task<CollectionModelBase<T>> FindCommentsAsync<T>(object articleKey, string searchQuery = DefaultSearchQuery,
            string statuses = "", int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields) where T : ArticleComment
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}/comments", ModuleRelativePath), articleKey));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "statuses", statuses);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
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
        public virtual Task<CollectionModelBase<ArticleTagEntry>> FindTagEntriesAsync(object articleKey, string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindTagEntriesAsync<ArticleTagEntry>(articleKey, searchQuery, page, rpp, sort, embed, fields);
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
        /// <returns>Collection of <typeparamref name="T" /> .</returns>
        public virtual async Task<CollectionModelBase<T>> FindTagEntriesAsync<T>(object articleKey, string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields) where T : ArticleTagEntry
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}/tags", ModuleRelativePath), articleKey));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Flags the comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> FlagCommentAsync(object articleKey, SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/flag", ModuleRelativePath), articleKey, commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Flags the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> FlagCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}/flag", ModuleRelativePath), articleKey, commentId, commentReplyId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
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
            return GetAsync<Article>(key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="Article" /> by provided key.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Article" />.</typeparam>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns><typeparamref name="T" />.</returns>
        public virtual Task<T> GetAsync<T>(object key, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : Article
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), key));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Gets the comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        public virtual Task<ArticleComment> GetCommentAsync(object articleKey, SGuid commentId, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetCommentAsync<ArticleComment>(articleKey, commentId, embed, fields);
        }

        /// <summary>
        /// Gets the comment asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleComment" />.</typeparam>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns><typeparamref name="T" />.</returns>
        public virtual Task<T> GetCommentAsync<T>(object articleKey, SGuid commentId, string embed = DefaultEmbed, string fields = DefaultFields) where T : ArticleComment
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}", ModuleRelativePath), articleKey, commentId));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Gets the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        public virtual Task<ArticleCommentReply> GetCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetCommentReplyAsync<ArticleCommentReply>(articleKey, commentId, commentReplyId, embed, fields);
        }

        /// <summary>
        /// Gets the comment reply asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleCommentReply" />.</typeparam>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns><typeparamref name="T" />.</returns>
        public virtual Task<T> GetCommentReplyAsync<T>(object articleKey, SGuid commentId, SGuid commentReplyId, string embed = DefaultEmbed, string fields = DefaultFields) where T : ArticleCommentReply
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}", ModuleRelativePath), articleKey, commentId, commentReplyId));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
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
            return GetTagEntryAsync(articleKey, key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleTagEntry" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleTagEntry" />.</typeparam>
        /// <param name="articleKey">Article id or slug.</param>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetTagEntryAsync<T>(object articleKey, object key, string embed = DefaultEmbed, string fields = DefaultFields) where T : ArticleTagEntry
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}/tags/{{1}}", ModuleRelativePath), articleKey, key));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="Article" /> into the system.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns>Newly created <see cref="Article" /> .</returns>
        public virtual Task<Article> InsertAsync(Article article)
        {
            return InsertAsync<Article>(article);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="Article" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Article" />.</typeparam>
        /// <param name="article">The article.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T article) where T : Article
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(client.GetApiUrl(ModuleRelativePath), article);
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="CreateArticleComment" /> into the system.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>Newly created <see cref="ArticleComment" /> .</returns>
        public virtual Task<ArticleComment> InsertCommentAsync(CreateArticleComment comment)
        {
            return InsertCommentAsync<CreateArticleComment, ArticleComment>(comment);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="CreateArticleComment" /> into the system.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="comment">The comment.</param>
        /// <returns>Newly created <typeparamref name="TOut" /> .</returns>
        public virtual Task<TOut> InsertCommentAsync<TIn, TOut>(TIn comment)
            where TIn : CreateArticleComment
            where TOut : ArticleComment
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<TIn, TOut>(client.GetApiUrl(String.Format("{0}/{{0}}/comments/", ModuleRelativePath), comment.ArticleId), comment);
            }
        }

        /// <summary>
        /// Inserts the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public virtual Task<ArticleCommentReply> InsertCommentReplyAsync(object articleKey, CreateArticleCommentReply comment)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<ArticleCommentReply>(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies", ModuleRelativePath), articleKey, comment.CommentId), comment);
            }
        }

        /// <summary>
        /// Inserts the comment reply asynchronous.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="articleKey">The article key.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>Newly inserted <typeparamref name="TOut" /> .</returns>
        public virtual Task<TOut> InsertCommentReplyAsync<TIn, TOut>(object articleKey, TIn comment)
            where TIn : CreateArticleCommentReply
            where TOut : ArticleCommentReply
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<TIn, TOut>(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies", ModuleRelativePath), articleKey, comment.CommentId), comment);
            }
        }

        /// <summary>
        /// Marks the comment as spam asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> MarkCommentAsSpamAsync(object articleKey, SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/spam", ModuleRelativePath), articleKey, commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Marks the comment reply as spam asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> MarkCommentReplyAsSpamAsync(object articleKey, SGuid commentId, SGuid commentReplyId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}/spam", ModuleRelativePath), articleKey, commentId, commentReplyId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Asynchronously publish the <see cref="Article" /> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="options">The options.</param>
        /// <returns>True if <see cref="Article" /> is published, false otherwise.</returns>
        public virtual async Task<bool> PublishAsync(object key, ArticleOptions options)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var result = await client.PutAsync<ArticleOptions, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{{0}}/publish", ModuleRelativePath), key), options);
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

        /// <summary>
        /// Reports the comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public virtual async Task<bool> ReportCommentAsync(object articleKey, SGuid commentId, ArticleCommentOptions options)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var result = await client.PutAsync<ArticleCommentOptions, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/report", ModuleRelativePath), articleKey, commentId), options);
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

        /// <summary>
        /// Reports the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public virtual async Task<bool> ReportCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId, ArticleCommentOptions options)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var result = await client.PutAsync<ArticleCommentOptions, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}/report", ModuleRelativePath), articleKey, commentId, commentReplyId), options);
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
        /// Uns the approve comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnApproveCommentAsync(object articleKey, SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/unapprove", ModuleRelativePath), articleKey, commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Uns the approve comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnApproveCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}/unapprove", ModuleRelativePath), articleKey, commentId, commentReplyId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Uns the flag comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnFlagCommentAsync(object articleKey, SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/unflag", ModuleRelativePath), articleKey, commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Uns the flag comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnFlagCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}/unflag", ModuleRelativePath), articleKey, commentId, commentReplyId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Uns the mark comment as spam asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnMarkCommentAsSpamAsync(object articleKey, SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/unspam", ModuleRelativePath), articleKey, commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Uns the mark comment reply as spam.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnMarkCommentReplyAsSpam(object articleKey, SGuid commentId, SGuid commentReplyId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}/unspam", ModuleRelativePath), articleKey, commentId, commentReplyId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Asynchronously unpublish the <see cref="Article" /> in the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="Article" /> is unpublished, false otherwise.</returns>
        public virtual async Task<bool> UnpublishAsync(object key)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/unpublish", ModuleRelativePath), key));
                var result = await client.SendAsync(request);

                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Uns the report comment asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnReportCommentAsync(object articleKey, SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/unreport", ModuleRelativePath), articleKey, commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Uns the report comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="commentReplyId">The comment reply identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnReportCommentReplyAsync(object articleKey, SGuid commentId, SGuid commentReplyId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}/unreport", ModuleRelativePath), articleKey, commentId, commentReplyId));
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
            return UpdateAsync<Article>(article);
        }

        /// <summary>
        /// Asynchronously update the <see cref="Article" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Article" />.</typeparam>
        /// <param name="article">The article.</param>
        /// <returns>Updated <typeparamref name="T" /> .</returns>
        public virtual Task<T> UpdateAsync<T>(T article) where T : Article
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T>(client.GetApiUrl(string.Format("{0}/{1}", ModuleRelativePath, article.Id)), article);
            }
        }

        /// <summary>
        /// Updates the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public virtual Task<ArticleComment> UpdateCommentAsync(ArticleComment comment)
        {
            return UpdateCommentAsync<ArticleComment>(comment);
        }

        /// <summary>
        /// Updates the comment asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleComment" />.</typeparam>
        /// <param name="comment">The comment.</param>
        /// <returns>Updated <typeparamref name="T" /> .</returns>
        public virtual Task<T> UpdateCommentAsync<T>(T comment) where T : ArticleComment
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T>(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}", ModuleRelativePath), comment.ArticleId, comment.Id), comment);
            }
        }

        /// <summary>
        /// Updates the comment reply asynchronous.
        /// </summary>
        /// <param name="articleKey">The article key.</param>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public virtual Task<ArticleCommentReply> UpdateCommentReplyAsync(object articleKey, ArticleCommentReply comment)
        {
            return UpdateCommentReplyAsync<ArticleCommentReply>(articleKey, comment);
        }

        /// <summary>
        /// Updates the comment reply asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleComment" />.</typeparam>
        /// <param name="articleKey">The article key.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>Updated <typeparamref name="T" /> .</returns>
        public virtual Task<T> UpdateCommentReplyAsync<T>(object articleKey, T comment) where T : ArticleCommentReply
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T>(client.GetApiUrl(String.Format("{0}/{{0}}/comments/{{1}}/replies/{{2}}", ModuleRelativePath), articleKey, comment.CommentId, comment.Id), comment);
            }
        }

        #endregion Methods

    }
}