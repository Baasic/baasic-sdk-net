using Baasic.Client.Common;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Blogs;
using Baasic.Client.Utility;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.Blogs
{
    /// <summary>
    /// The page status client class. <seealso cref="ClientBase" /><seealso cref="IBlogPostStatusClient" />
    /// </summary>
    public class BlogPostStatusClient : ClientBase, IBlogPostStatusClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogPostStatusClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public BlogPostStatusClient(IClientConfiguration configuration,
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
            get { return "lookups/blog-post-status"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="BlogPostStatus" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="BlogPostStatus" /> is deleted, false otherwise.</returns>
        public virtual async Task<bool> DeleteAsync(object id)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously find <see cref="BlogPostStatus" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="PageStatus" /> s.</returns>
        public virtual Task<CollectionModelBase<BlogPostStatus>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync(searchQuery, null, null, null, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="BlogPostStatus" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogPostStatus" /> s.</returns>
        public virtual Task<CollectionModelBase<BlogPostStatus>> FindAsync(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<BlogPostStatus>(searchQuery, from, to, ids, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="BlogPostStatus" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostStatus" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="tags">The article tags.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        public virtual async Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : BlogPostStatus
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "from", from);
                InitializeQueryStringPair(uriBuilder, "to", to);
                InitializeQueryStringPair(uriBuilder, "ids", ids);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPostStatus" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="BlogPostStatus" /> is returned, otherwise null.</returns>
        public virtual Task<BlogPostStatus> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetAsync<BlogPostStatus>(key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPostStatus" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostStatus" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetAsync<T>(object id, string embed = DefaultEmbed, string fields = DefaultFields) where T : BlogPostStatus
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPostStatus" /> into the system.
        /// </summary>
        /// <param name="blogPost">Resource instance.</param>
        /// <returns>Newly created <see cref="BlogPostStatus" /> .</returns>
        public virtual Task<BlogPostStatus> InsertAsync(BlogPostStatus blogPost)
        {
            return InsertAsync<BlogPostStatus>(blogPost);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPostStatus" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostStatus" />.</typeparam>
        /// <param name="blogPost">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T blogPost) where T : BlogPostStatus
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(client.GetApiUrl(ModuleRelativePath), blogPost);
            }
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogPostStatus" /> into the system.
        /// </summary>
        /// <param name="blogPosts">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="BlogPostStatus" /> .</returns>
        public virtual Task<BatchResult<BlogPostStatus>[]> InsertAsync(BlogPostStatus[] blogPosts)
        {
            return InsertAsync<BlogPostStatus>(blogPosts);
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogPostStatus" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostStatus" />.</typeparam>
        /// <param name="pages">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        public virtual Task<BatchResult<T>[]> InsertAsync<T>(T[] pages) where T : BlogPostStatus
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T[], BatchResult<T>[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), pages);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="BlogPostStatus" /> in the system.
        /// </summary>
        /// <param name="page">Resource instance.</param>
        /// <returns>True if <see cref="BlogPostStatus" /> is successfully updated, false otherwise.</returns>
        public virtual Task<bool> UpdateAsync(BlogPostStatus page)
        {
            return UpdateAsync<BlogPostStatus>(page);
        }

        /// <summary>
        /// Asynchronously update the <see cref="BlogPostStatus" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostStatus" />.</typeparam>
        /// <param name="page">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync<T>(T page) where T : BlogPostStatus
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    var result = await client.PutAsync<BlogPostStatus, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, page.Id)), page);
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
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogPostStatus" /> into the system.
        /// </summary>
        /// <param name="pages">Resource instance.</param>
        /// <returns>Collection of updated <see cref="BlogPostStatus" /> .</returns>
        public virtual Task<BatchResult<BlogPostStatus>[]> UpdateAsync(BlogPostStatus[] blogPostStatuses)
        {
            return UpdateAsync<BlogPostStatus>(blogPostStatuses);
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogPostStatus" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostStatus" />.</typeparam>
        /// <param name="blogPostStatuses">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task<BatchResult<T>[]> UpdateAsync<T>(T[] blogPostStatuses) where T : BlogPostStatus
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T[], BatchResult<T>[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), blogPostStatuses);
            }
        }

        #endregion Methods
    }
}