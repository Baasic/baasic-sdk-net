using Baasic.Client.Common;
using Baasic.Client.Model.Blogs;
using System.IO;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.Blogs
{
    /// <summary>
    /// The blog post file stream client contract.
    /// </summary>
    public interface IBlogPostFileStreamClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="width">The file width.</param>
        /// <param name="height">The file height.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<Stream> GetFileAsync(object id, string fileName, int? width = null, int? height = null);

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="width">The file width.</param>
        /// <param name="height">The file height.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<Stream> GetFileAsync(object id, int? width = null, int? height = null);

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPostFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPostFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="width">The file width.</param>
        /// <param name="height">The file height.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<string> GetFileUrlAsync(object id, int? width = null, int? height = null);

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <param name="blogPostFile">Resource instance.</param>
        /// <returns>Newly created <see cref="BlogPostFile" /> .</returns>
        Task<BlogPostFile> InsertAsync(string fileName, Stream file, SGuid blogPostId);

        /// <summary>
        /// Asynchronously updates the <see cref="BlogPostFile" /> into the system.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="file">The file.</param>
        Task<bool> UpdateAsync(object id, string fileName, Stream file);

        #endregion Methods
    }
}