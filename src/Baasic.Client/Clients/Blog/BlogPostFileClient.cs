﻿using Baasic.Client.Common;
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
    /// The blogPost file client class.
    /// </summary>
    public class BlogPostFileClient : ClientBase, IBlogPostFileClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogPostFileClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public BlogPostFileClient(
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
            get { return "blog/blog-post-files"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <param name="deleteRequests">The collection of delete requests.</param>
        /// <returns>True if the collection <see cref="BlogPostFile" /> is deleted, false otherwise.</returns>
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
        /// Asynchronously deletes the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="BlogPostFile" /> is deleted, false otherwise.</returns>
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
        /// Asynchronously find <see cref="BlogPostFile" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogPostFile" /> s.</returns>
        public virtual Task<CollectionModelBase<BlogPostFile>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync(searchQuery, null, null, null, null, null, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="BlogPostFileEntry" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="blogPostIds">The blog post ids.</param>
        /// <param name="fileIds">The file ids.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogPostFile" /> s.</returns>
        public virtual Task<CollectionModelBase<BlogPostFile>> FindAsync(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null, string blogPostIds = null,
            string fileIds = null, int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<BlogPostFile>(searchQuery, from, to, ids, blogPostIds, fileIds, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="BlogPostFile" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="blogPostIds">The blog post ids.</param>
        /// <param name="fileIds">The file ids.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        public virtual async Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null, string blogPostIds = null,
            string fileIds = null, int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : BlogPostFile
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "from", from);
                InitializeQueryStringPair(uriBuilder, "to", to);
                InitializeQueryStringPair(uriBuilder, "ids", ids);
                InitializeQueryStringPair(uriBuilder, "blogPostIds", blogPostIds);
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
        /// Asynchronously gets the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="BlogPostFile" /> is returned, otherwise null.</returns>
        public virtual Task<BlogPostFile> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetAsync<BlogPostFile>(key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetAsync<T>(object id, string embed = DefaultEmbed, string fields = DefaultFields) where T : BlogPostFile
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <param name="blogPostFile">Resource instance.</param>
        /// <returns>Newly created <see cref="BlogPostFile" /> .</returns>
        public virtual Task<BlogPostFile> InsertAsync(BlogPostFile blogPostFile)
        {
            return InsertAsync<BlogPostFile>(blogPostFile);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="blogPostFile">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T blogPostFile) where T : BlogPostFile
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(String.Format("{0}/link", client.GetApiUrl(ModuleRelativePath)), blogPostFile);
            }
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <param name="blogPostFiles">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="BlogPostFile" /> .</returns>
        public virtual Task<BatchResult<BlogPostFile>[]> InsertAsync(BlogPostFile[] blogPostFiles)
        {
            return InsertAsync<BlogPostFile>(blogPostFiles);
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="blogPostFiles">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        public virtual Task<BatchResult<T>[]> InsertAsync<T>(T[] blogPostFiles) where T : BlogPostFile
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T[], BatchResult<T>[]>(client.GetApiUrl(String.Format("{0}/batch/link", ModuleRelativePath)), blogPostFiles);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="BlogPostFile" /> in the system.
        /// </summary>
        /// <param name="blogPostFile">Resource instance.</param>
        /// <returns>True if <see cref="BlogPostFile" /> is successfully updated, false otherwise.</returns>
        public virtual Task<bool> UpdateAsync(BlogPostFile blogPostFile)
        {
            return UpdateAsync<BlogPostFile>(blogPostFile);
        }

        /// <summary>
        /// Asynchronously update the <see cref="BlogPostFile" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="blogPostFile">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync<T>(T blogPostFile) where T : BlogPostFile
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    var result = await client.PutAsync<BlogPostFile, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, blogPostFile.Id)), blogPostFile);
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
        /// Asynchronously updates the collection of <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <param name="blogPostFiles">Resource instance.</param>
        /// <returns>Collection of updated <see cref="BlogPostFile" /> .</returns>
        public virtual Task<BatchResult<BlogPostFile>[]> UpdateAsync(BlogPostFile[] blogPostFiles)
        {
            return UpdateAsync<BlogPostFile>(blogPostFiles);
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="blogPostFiles">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task<BatchResult<T>[]> UpdateAsync<T>(T[] blogPostFiles) where T : BlogPostFile
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T[], BatchResult<T>[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), blogPostFiles);
            }
        }

        #endregion Methods
    }
}