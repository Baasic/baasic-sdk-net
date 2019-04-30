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
    /// <summary>
    /// The page client class.
    /// </summary>
    public class PageClient : ClientBase, IPageClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PageClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public PageClient(IClientConfiguration configuration,
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
            get { return "cms/pages"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="Page" /> from the system.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        /// <returns>True if the collection <see cref="Page" /> is deleted, false otherwise.</returns>
        public virtual Task<bool> BulkDeleteAsync(object ids)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), ids);
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
        /// Asynchronously deletes the <see cref="Page" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="Page" /> is deleted, false otherwise.</returns>
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
        /// Asynchronously find <see cref="Page" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Page" /> s.</returns>
        public virtual Task<CollectionModelBase<Page>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync(searchQuery, null, null, null, null, null, null, null, null, null, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="Page" /> s.
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
        /// <param name="languageIds">The language ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Page" /> s.</returns>
        public virtual Task<CollectionModelBase<Page>> FindAsync(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, DateTime? publishedFrom = null, DateTime? publishedTo = null, string ids = null,
            string url = null, string template = null, string pageStatusIds = null, string languageIds = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<Page>(searchQuery, from, to, publishedFrom, publishedTo, ids, url, template, pageStatusIds, languageIds, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="Page" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Page" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="publishedFrom">The published from date.</param>
        /// <param name="publishedTo">The published to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="url">The url.</param>
        /// <param name="template">The template.</param>
        /// <param name="pageStatusIds">The page status ids.</param>
        /// <param name="languageIds">The language ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        public virtual async Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, DateTime? publishedFrom = null, DateTime? publishedTo = null, string ids = null,
            string url = null, string template = null, string pageStatusIds = null, string languageIds = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : Page
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
                InitializeQueryStringPair(uriBuilder, "pageStatusIds", pageStatusIds);
                InitializeQueryStringPair(uriBuilder, "languageIds", languageIds);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="Page" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="Page" /> is returned, otherwise null.</returns>
        public virtual Task<Page> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetAsync<Page>(key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="Page" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Page" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetAsync<T>(object id, string embed = DefaultEmbed, string fields = DefaultFields) where T : Page
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="Page" /> into the system.
        /// </summary>
        /// <param name="page">Resource instance.</param>
        /// <returns>Newly created <see cref="Page" /> .</returns>
        public virtual Task<Page> InsertAsync(Page page)
        {
            return InsertAsync<Page>(page);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="Page" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Page" />.</typeparam>
        /// <param name="page">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T page) where T : Page
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(client.GetApiUrl(ModuleRelativePath), page);
            }
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Page" /> into the system.
        /// </summary>
        /// <param name="pages">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="Page" /> .</returns>
        public virtual Task<BatchResult<Page>[]> InsertAsync(Page[] pages)
        {
            return InsertAsync<Page>(pages);
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Page" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Page" />.</typeparam>
        /// <param name="pages">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        public virtual Task<BatchResult<T>[]> InsertAsync<T>(T[] pages) where T : Page
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T[], BatchResult<T>[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), pages);
            }
        }

        /// <summary>
        /// Asynchronously checks the URL uniqness.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>True if url is unique</returns>
        public virtual async Task<bool> IsUrlUniqueAsync(string url)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(string.Format("{0}/unique/url", ModuleRelativePath), url));
                    InitializeQueryStringPair(uriBuilder, "url", url);
                    await client.GetAsync<bool>(uriBuilder.ToString());
                    return true;
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.Conflict)
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
        /// Asynchronously update the <see cref="Page" /> in the system.
        /// </summary>
        /// <param name="page">Resource instance.</param>
        /// <returns>True if <see cref="Page" /> is successfully updated, false otherwise.</returns>
        public virtual Task<bool> UpdateAsync(Page page)
        {
            return UpdateAsync<Page>(page);
        }

        /// <summary>
        /// Asynchronously update the <see cref="Page" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Page" />.</typeparam>
        /// <param name="page">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync<T>(T page) where T : Page
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    var result = await client.PutAsync<Page, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, page.Id)), page);
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
        /// Asynchronously updates the collection of <see cref="Page" /> into the system.
        /// </summary>
        /// <param name="pages">Resource instance.</param>
        /// <returns>Collection of updated <see cref="Page" /> .</returns>
        public virtual Task<BatchResult<Page>[]> UpdateAsync(Page[] pages)
        {
            return UpdateAsync<Page>(pages);
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Page" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Page" />.</typeparam>
        /// <param name="pages">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task<BatchResult<T>[]> UpdateAsync<T>(T[] pages) where T : Page
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T[], BatchResult<T>[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), pages);
            }
        }

        #endregion Methods
    }
}