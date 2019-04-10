using Baasic.Client.Common;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model.Blogs;
using Baasic.Client.Utility;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.Blogs
{
    /// <summary>
    /// The blog post file stream client class. <seealso cref="ClientBase" /><seealso cref="IBlogPostFileStreamClient" />
    /// </summary>
    public class BlogPostFileStreamClient : ClientBase, IBlogPostFileStreamClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogPostFileStreamClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public BlogPostFileStreamClient(
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
            get { return "blog/blog-post-file-streams"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="width">The file width.</param>
        /// <param name="height">The file height.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<Stream> GetFileAsync(object id, int? width = null, int? height = null)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryStringPair(uriBuilder, "width", width);
                InitializeQueryStringPair(uriBuilder, "height", height);
                return client.GetAsync(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="width">The file width.</param>
        /// <param name="height">The file height.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<string> GetFileUrlAsync(object id, int? width = null, int? height = null)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryStringPair(uriBuilder, "width", width);
                InitializeQueryStringPair(uriBuilder, "height", height);
                return Task.FromResult(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <param name="blogPostFile">Resource instance.</param>
        /// <returns>Newly created <see cref="BlogPostFile" /> .</returns>
        public virtual Task<BlogPostFile> InsertAsync(string fileName, Stream file, SGuid blogPostId)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, fileName)));
                InitializeQueryStringPair(uriBuilder, "blogPostId", blogPostId);
                return client.PostFileAsync<BlogPostFile>(uriBuilder.ToString(), file, fileName);
            }
        }

        /// <summary>
        /// Asynchronously updates the <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="file">The file.</param>
        public virtual async Task<bool> UpdateAsync(object id, string fileName, Stream file)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, id)));
                var result = await client.PutFileAsync<HttpStatusCode>(uriBuilder.ToString(), file, fileName);
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

        #endregion Methods
    }
}