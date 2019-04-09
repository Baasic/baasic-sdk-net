using Baasic.Client.Common;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.MediaVault;
using Baasic.Client.Utility;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.MediaVault
{
    /// <summary>
    /// The file client. <seealso cref="IFileClient" /><seealso cref="ClientBase" />
    /// </summary>
    public class FileClient : ClientBase, IFileClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public FileClient(
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
            get { return "files"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="FileEntry" /> from the system.
        /// </summary>
        /// <param name="deleteRequests">The collection of delete requests.</param>
        /// <returns>True if the collection <see cref="FileEntry" /> is deleted, false otherwise.</returns>
        public virtual Task<bool> BulkDeleteAsync(FileEntryDeleteRequest[] deleteRequests)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return client.DeleteAsync(client.GetApiUrl(string.Format("{0}/batch/unlink", ModuleRelativePath)), deleteRequests);
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
        /// Asynchronously deletes the <see cref="FileEntry" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fileFormat">The file format.</param>
        /// <returns>True if <see cref="FileEntry" /> is deleted, false otherwise.</returns>
        public virtual async Task<bool> DeleteAsync(object id, object fileFormat = null)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/unlink/{{0}}", ModuleRelativePath, id)));
                    InitializeQueryStringPair(uriBuilder, "fileFormat", fileFormat);
                    return await client.DeleteAsync(uriBuilder.ToString());
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
        /// Asynchronously find <see cref="FileEntry" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="FileEntry" /> s.</returns>
        public virtual Task<CollectionModelBase<FileEntry>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync(searchQuery, null, null, null, null, null, null, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="FileEntryEntry" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="minFileSize">The min file size.</param>
        /// <param name="maxFileSize">The max file size.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="FileEntry" /> s.</returns>
        public virtual Task<CollectionModelBase<FileEntry>> FindAsync(string searchQuery = DefaultSearchQuery,
            string fileName = null,
            DateTime? from = null, DateTime? to = null, string ids = null, int? minFileSize = null, int? maxFileSize = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<FileEntry>(searchQuery, fileName, from, to, ids, minFileSize, maxFileSize, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="FileEntry" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="maxFileSize">The max file size.</param>
        /// <param name="minFileSize">The min file size.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        public virtual async Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = DefaultSearchQuery,
            string fileName = null,
            DateTime? from = null, DateTime? to = null, string ids = null, int? minFileSize = null, int? maxFileSize = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : FileEntry
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "fileName", fileName);
                InitializeQueryStringPair(uriBuilder, "from", from);
                InitializeQueryStringPair(uriBuilder, "to", to);
                InitializeQueryStringPair(uriBuilder, "ids", ids);
                InitializeQueryStringPair(uriBuilder, "minFileSize", minFileSize);
                InitializeQueryStringPair(uriBuilder, "maxFileSize", maxFileSize);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="FileEntry" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="FileEntry" /> is returned, otherwise null.</returns>
        public virtual Task<FileEntry> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetAsync<FileEntry>(key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="FileEntry" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetAsync<T>(object id, string embed = DefaultEmbed, string fields = DefaultFields) where T : FileEntry
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <param name="fileEntry">Resource instance.</param>
        /// <returns>Newly created <see cref="FileEntry" /> .</returns>
        public virtual Task<FileEntry> InsertAsync(FileEntry fileEntry)
        {
            return InsertAsync<FileEntry>(fileEntry);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="fileEntry">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T fileEntry) where T : FileEntry
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(String.Format("{0}/link", client.GetApiUrl(ModuleRelativePath)), fileEntry);
            }
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <param name="fileEntries">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="FileEntry" /> .</returns>
        public virtual Task<FileEntry[]> InsertAsync(FileEntry[] fileEntries)
        {
            return InsertAsync<FileEntry>(fileEntries);
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="fileEntries">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> InsertAsync<T>(T[] fileEntries) where T : FileEntry
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T[]>(client.GetApiUrl(String.Format("{0}/batch/link", ModuleRelativePath)), fileEntries);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="FileEntry" /> in the system.
        /// </summary>
        /// <param name="fileEntry">Resource instance.</param>
        /// <returns>True if <see cref="FileEntry" /> is successfully updated, false otherwise.</returns>
        public virtual Task<bool> UpdateAsync(FileEntry fileEntry)
        {
            return UpdateAsync<FileEntry>(fileEntry);
        }

        /// <summary>
        /// Asynchronously update the <see cref="FileEntry" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="fileEntry">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync<T>(T fileEntry) where T : FileEntry
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    var result = await client.PutAsync<FileEntry, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, fileEntry.Id)), fileEntry);
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
        /// Asynchronously updates the collection of <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <param name="fileEntries">Resource instance.</param>
        /// <returns>Collection of updated <see cref="FileEntry" /> .</returns>
        public virtual Task<FileEntry[]> UpdateAsync(FileEntry[] fileEntries)
        {
            return UpdateAsync<FileEntry>(fileEntries);
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="fileEntries">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> UpdateAsync<T>(T[] fileEntries) where T : FileEntry
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), fileEntries);
            }
        }

        #endregion Methods
    }
}