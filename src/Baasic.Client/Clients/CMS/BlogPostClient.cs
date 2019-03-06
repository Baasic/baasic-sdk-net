using Baasic.Client.Common;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.CMS;
using Baasic.Client.Utility;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.CMS
{
    public class BlogPostClient : ClientBase, IBlogPostClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogPostClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public BlogPostClient(
            IClientConfiguration configuration,
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
            get { return "cms/blog-posts"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="BlogPost" /> from the system.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        /// <returns>True if the collection <see cref="BlogPost" /> is deleted, false otherwise.</returns>
        public virtual Task<bool> BulkDeleteAsync(object ids)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return client.DeleteAsync(client.GetApiUrl(string.Format("{0}/batch", ModuleRelativePath)), ids);
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return Task.FromResult(false);
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes the <see cref="BlogPost" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="BlogPost" /> is deleted, false otherwise.</returns>
        public virtual async Task<bool> DeleteAsync(object id)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.DeleteAsync(client.GetApiUrl(string.Format("{0}/{{0}}", ModuleRelativePath), id));
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
        /// Asynchronously find <see cref="BlogPost" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogPost" /> s.</returns>
        public virtual Task<CollectionModelBase<BlogPost>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync(searchQuery, null, null, null, null, null, null, null, null, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="BlogPost" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="publishedFrom">The published from date.</param>
        /// <param name="publishedTo">The published to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="url">The url.</param>
        /// <param name="template">The template.</param>
        /// <param name="pageStatusIds">The page status ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogPost" /> s.</returns>
        public virtual Task<CollectionModelBase<BlogPost>> FindAsync(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, DateTime? publishedFrom = null, DateTime? publishedTo = null,
            string ids = null, string url = null, string template = null, string blogPostStatusIds = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<BlogPost>(searchQuery, from, to, publishedFrom, publishedTo, ids, url, template, blogPostStatusIds, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="BlogPost" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="publishedFrom">The published from date.</param>
        /// <param name="publishedTo">The published to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="url">The url.</param>
        /// <param name="template">The template.</param>
        /// <param name="pageStatusIds">The page status ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        public virtual async Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, DateTime? publishedFrom = null, DateTime? publishedTo = null, string ids = null,
            string url = null, string template = null, string blogPostStatusIds = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : BlogPost
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "from", from);
                InitializeQueryStringPair(uriBuilder, "to", to);
                InitializeQueryStringPair(uriBuilder, "publishedFrom", publishedFrom);
                InitializeQueryStringPair(uriBuilder, "publishedTo", publishedTo);
                InitializeQueryStringPair(uriBuilder, "ids", ids);
                InitializeQueryStringPair(uriBuilder, "url", url);
                InitializeQueryStringPair(uriBuilder, "template", template);
                InitializeQueryStringPair(uriBuilder, "blogPostStatusIds", blogPostStatusIds);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPost" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="BlogPost" /> is returned, otherwise null.</returns>
        public virtual Task<BlogPost> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetAsync<BlogPost>(key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPost" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetAsync<T>(object id, string embed = DefaultEmbed, string fields = DefaultFields) where T : BlogPost
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(string.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <param name="BlogPost">The blog post.</param>
        /// <returns>Newly created <see cref="BlogPost" /> .</returns>
        public virtual Task<BlogPost> InsertAsync(BlogPost BlogPost)
        {
            return InsertAsync<BlogPost>(BlogPost);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="BlogPost">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T BlogPost) where T : BlogPost
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                return client.PostAsync<T>(uriBuilder.ToString(), BlogPost);
            }
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <param name="BlogPosts">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="BlogPost" /> .</returns>
        public virtual Task<BlogPost[]> InsertAsync(BlogPost[] BlogPosts)
        {
            return InsertAsync<BlogPost>(BlogPosts);
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="BlogPosts">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> InsertAsync<T>(T[] BlogPosts) where T : BlogPost
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T[]>(client.GetApiUrl(string.Format("{0}/batch", ModuleRelativePath)), BlogPosts);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="BlogPost" /> in the system.
        /// </summary>
        /// <param name="BlogPost">The blog post.</param>
        /// <returns>True if <see cref="BlogPost" /> is successfully updated, false otherwise.</returns>
        public virtual Task<bool> UpdateAsync(BlogPost BlogPost)
        {
            return UpdateAsync<BlogPost>(BlogPost);
        }

        /// <summary>
        /// Asynchronously update the <see cref="BlogPost" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="BlogPost">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync<T>(T BlogPost) where T : BlogPost
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(string.Format("{0}/{1}", ModuleRelativePath, BlogPost.Id)));
                    var result = await client.PutAsync<BlogPost, HttpStatusCode>(uriBuilder.ToString(), BlogPost);
                    switch (result)
                    {
                        case HttpStatusCode.Created:
                        case HttpStatusCode.NoContent:
                        case HttpStatusCode.OK:
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
        /// Asynchronously updates the collection of <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <param name="BlogPosts">Resource instance.</param>
        /// <returns>Collection of updated <see cref="BlogPost" /> .</returns>
        public virtual Task<BlogPost[]> UpdateAsync(BlogPost[] BlogPosts)
        {
            return UpdateAsync<BlogPost>(BlogPosts);
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="BlogPosts">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> UpdateAsync<T>(T[] BlogPosts) where T : BlogPost
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T[]>(client.GetApiUrl(string.Format("{0}/batch", ModuleRelativePath)), BlogPosts);
            }
        }

        #endregion Methods
    }
}