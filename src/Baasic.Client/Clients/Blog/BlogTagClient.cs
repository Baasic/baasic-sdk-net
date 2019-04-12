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
    public class BlogTagClient : ClientBase, IBlogTagClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogTagClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public BlogTagClient(
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
            get { return "blog/blog-tags"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="BlogTag" /> from the system.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        /// <returns>True if the collection <see cref="BlogTag" /> is deleted, false otherwise.</returns>
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
        /// Asynchronously deletes the <see cref="BlogTag" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="BlogTag" /> is deleted, false otherwise.</returns>
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
        /// Asynchronously find <see cref="BlogTag" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogTag" /> s.</returns>
        public virtual Task<CollectionModelBase<BlogTag>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync(searchQuery, null, null, null, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="BlogTag" /> s.
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
        /// <returns>List of <see cref="BlogTag" /> s.</returns>
        public virtual Task<CollectionModelBase<BlogTag>> FindAsync(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<BlogTag>(searchQuery, from, to, ids, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="BlogTag" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
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
            where T : BlogTag
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
        /// Asynchronously gets the <see cref="BlogTag" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="BlogTag" /> is returned, otherwise null.</returns>
        public virtual Task<BlogTag> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetAsync<BlogTag>(key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="BlogTag" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetAsync<T>(object id, string embed = DefaultEmbed, string fields = DefaultFields) where T : BlogTag
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(string.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <param name="blogTag">The blog tag.</param>
        /// <returns>Newly created <see cref="BlogTag" /> .</returns>
        public virtual Task<BlogTag> InsertAsync(BlogTag blogTag)
        {
            return InsertAsync<BlogTag>(blogTag);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="blogTag">The blogTag.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T blogTag) where T : BlogTag
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                return client.PostAsync<T>(uriBuilder.ToString(), blogTag);
            }
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <param name="blogTags">The blogTags.</param>
        /// <returns>Collection of newly created <see cref="BlogTag" /> .</returns>
        public virtual Task<BlogTag[]> InsertAsync(BlogTag[] blogTags)
        {
            return InsertAsync<BlogTag>(blogTags);
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="blogTags">The blogTags.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> InsertAsync<T>(T[] blogTags) where T : BlogTag
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T[]>(client.GetApiUrl(string.Format("{0}/batch", ModuleRelativePath)), blogTags);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="BlogTag" /> in the system.
        /// </summary>
        /// <param name="blogTag">The blogTag.</param>
        /// <returns>True if <see cref="BlogTag" /> is successfully updated, false otherwise.</returns>
        public virtual Task<bool> UpdateAsync(BlogTag blogTag)
        {
            return UpdateAsync<BlogTag>(blogTag);
        }

        /// <summary>
        /// Asynchronously update the <see cref="BlogTag" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="blogTag">The blogTag.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync<T>(T blogTag) where T : BlogTag
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(string.Format("{0}/{1}", ModuleRelativePath, blogTag.Id)));
                    var result = await client.PutAsync<BlogTag, HttpStatusCode>(uriBuilder.ToString(), blogTag);
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
        /// Asynchronously updates the collection of <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <param name="blogTags">The blogTags.</param>
        /// <returns>Collection of updated <see cref="BlogTag" /> .</returns>
        public virtual Task<BlogTag[]> UpdateAsync(BlogTag[] blogTags)
        {
            return UpdateAsync<BlogTag>(blogTags);
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogTag" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogTag" />.</typeparam>
        /// <param name="blogTags">The blogTags.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> UpdateAsync<T>(T[] blogTags) where T : BlogTag
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T[]>(client.GetApiUrl(string.Format("{0}/batch", ModuleRelativePath)), blogTags);
            }
        }

        #endregion Methods
    }
}