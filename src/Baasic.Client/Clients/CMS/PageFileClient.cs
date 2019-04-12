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
    /// The page file client class.
    /// </summary>
    public class PageFileClient : ClientBase, IPageFileClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PageFileClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public PageFileClient(IClientConfiguration configuration,
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
            get { return "cms/page-files"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="PageFile" /> from the system.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        /// <returns>True if the collection <see cref="PageFile" /> is deleted, false otherwise.</returns>
        public virtual Task<bool> BulkDeleteAsync(object ids)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return client.DeleteAsync(client.GetApiUrl(string.Format("{0}/batch/unlink", ModuleRelativePath)), ids);
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
        /// Asynchronously deletes the <see cref="PageFile" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="PageFile" /> is deleted, false otherwise.</returns>
        public virtual async Task<bool> DeleteAsync(object id)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.DeleteAsync(client.GetApiUrl(String.Format("{0}/unlink/{{0}}", ModuleRelativePath), id));
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
        /// Asynchronously find <see cref="PageFile" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="PageFile" /> s.</returns>
        public virtual Task<CollectionModelBase<PageFile>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync(searchQuery, null, null, null, null, null, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="PageFileEntry" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="pageIds">The page ids.</param>
        /// <param name="fileIds">The file ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="PageFile" /> s.</returns>
        public virtual Task<CollectionModelBase<PageFile>> FindAsync(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null, string pageIds = null,
            string fileIds = null, int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<PageFile>(searchQuery, from, to, ids, pageIds, fileIds, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="PageFile" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="pageIds">The page ids.</param>
        /// <param name="fileIds">The file ids.</param>
        /// <param name="tags">The article tags.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        public virtual async Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null, string pageIds = null,
            string fileIds = null, int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : PageFile
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "from", from);
                InitializeQueryStringPair(uriBuilder, "to", to);
                InitializeQueryStringPair(uriBuilder, "ids", ids);
                InitializeQueryStringPair(uriBuilder, "pageIds", pageIds);
                InitializeQueryStringPair(uriBuilder, "fileIds", fileIds);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="PageFile" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="PageFile" /> is returned, otherwise null.</returns>
        public virtual Task<PageFile> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetAsync<PageFile>(key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="PageFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetAsync<T>(object id, string embed = DefaultEmbed, string fields = DefaultFields) where T : PageFile
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="PageFile" /> into the system.
        /// </summary>
        /// <param name="pageFile">Resource instance.</param>
        /// <returns>Newly created <see cref="PageFile" /> .</returns>
        public virtual Task<PageFile> InsertAsync(PageFile pageFile)
        {
            return InsertAsync<PageFile>(pageFile);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="PageFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="pageFile">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T pageFile) where T : PageFile
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(String.Format("{0}/link", client.GetApiUrl(ModuleRelativePath)), pageFile);
            }
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="PageFile" /> into the system.
        /// </summary>
        /// <param name="pageFiles">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="PageFile" /> .</returns>
        public virtual Task<PageFile[]> InsertAsync(PageFile[] pageFiles)
        {
            return InsertAsync<PageFile>(pageFiles);
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="PageFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="pageFiles">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> InsertAsync<T>(T[] pageFiles) where T : PageFile
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T[]>(client.GetApiUrl(String.Format("{0}/batch/link", ModuleRelativePath)), pageFiles);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="PageFile" /> in the system.
        /// </summary>
        /// <param name="pageFile">Resource instance.</param>
        /// <returns>True if <see cref="PageFile" /> is successfully updated, false otherwise.</returns>
        public virtual Task<bool> UpdateAsync(PageFile pageFile)
        {
            return UpdateAsync<PageFile>(pageFile);
        }

        /// <summary>
        /// Asynchronously update the <see cref="PageFile" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="pageFile">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync<T>(T pageFile) where T : PageFile
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    var result = await client.PutAsync<PageFile, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, pageFile.Id)), pageFile);
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
        /// Asynchronously updates the collection of <see cref="PageFile" /> into the system.
        /// </summary>
        /// <param name="pageFiles">Resource instance.</param>
        /// <returns>Collection of updated <see cref="PageFile" /> .</returns>
        public virtual Task<PageFile[]> UpdateAsync(PageFile[] pageFiles)
        {
            return UpdateAsync<PageFile>(pageFiles);
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="PageFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="pageFiles">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> UpdateAsync<T>(T[] pageFiles) where T : PageFile
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), pageFiles);
            }
        }

        #endregion Methods
    }
}