using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using Baasic.Client.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client.Modules.Articles
{
    /// <summary>
    /// Article comment replies client.
    /// </summary>
    public class ArticleCommentRepliesClient : ClientBase, IArticleCommentRepliesClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleCommentsClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public ArticleCommentRepliesClient(IClientConfiguration configuration,
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
            get { return "article-comment-replies"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Approves the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public virtual async Task<bool> ApproveAsync(SGuid commentId, ArticleCommentOptions options)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var result = await client.PutAsync<ArticleCommentOptions, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{{0}}/approve", ModuleRelativePath), commentId), options);
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
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual Task<bool> DeleteAsync(SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), commentId));
            }
        }

        /// <summary>
        /// Finds the comments asynchronous.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="statuses">The statuses.</param>
        /// <param name="page">The page.</param>
        /// <param name="rpp">The RPP.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        public virtual async Task<CollectionModelBase<ArticleCommentReply>> FindAsync(string searchQuery = DefaultSearchQuery,
            string statuses = "", int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "statuses", statuses);
                var result = await client.GetAsync<CollectionModelBase<ArticleCommentReply>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<ArticleCommentReply>();
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
        public virtual async Task<bool> FlagAsync(SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/flag", ModuleRelativePath), commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Gets the comment asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="embed">The embed.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        public virtual Task<ArticleCommentReply> GetAsync(SGuid commentId, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), commentId));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<ArticleCommentReply>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="CreateArticleCommentReply" /> into the system.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>Newly created <see cref="ArticleCommentReply" /> .</returns>
        public virtual Task<ArticleCommentReply> InsertCommentAsync(CreateArticleCommentReply comment)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<ArticleCommentReply>(client.GetApiUrl(ModuleRelativePath), comment);
            }
        }

        /// <summary>
        /// Marks the comment as spam asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> MarkAsSpamAsync(SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/spam", ModuleRelativePath), commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Reports the comment asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public virtual async Task<bool> ReportCommentAsync(SGuid commentId, ArticleCommentOptions options)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var result = await client.PutAsync<ArticleCommentOptions, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{{0}}/report", ModuleRelativePath), commentId), options);
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
        /// Uns the approve comment asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnApproveAsync(SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/unapprove", ModuleRelativePath), commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Uns the flag comment asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnFlagAsync(SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/unflag", ModuleRelativePath), commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Uns the mark comment as spam asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnMarkAsSpamAsync(SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/unspam", ModuleRelativePath), commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Uns the report comment asynchronous.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> UnReportAsync(SGuid commentId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(String.Format("{0}/{{0}}/unreport", ModuleRelativePath), commentId));
                var result = await client.SendAsync(request);
                return result.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Updates the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public virtual Task<ArticleCommentReply> UpdateCommentAsync(ArticleCommentReply comment)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<ArticleCommentReply>(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), comment.Id), comment);
            }
        }

        #endregion Methods
    }
}