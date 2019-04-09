using Baasic.Client.Common;
using Baasic.Client.Model.CMS;
using System.IO;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.CMS
{
    /// <summary>
    /// The page file stream client contract.
    /// </summary>
    public interface IPageFileStreamClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously gets the <see cref="PageFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="width">The file width.</param>
        /// <param name="height">The file height.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<Stream> GetFileAsync(object id, int? width = null, int? height = null);

        /// <summary>
        /// Asynchronously gets the <see cref="PageFile" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="PageFile" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="width">The file width.</param>
        /// <param name="height">The file height.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<string> GetFileUrlAsync(object id, int? width = null, int? height = null);

        /// <summary>
        /// Asynchronously insert the <see cref="PageFile" /> into the system.
        /// </summary>
        /// <param name="pageId">Resource instance.</param>
        /// <returns>Newly created <see cref="PageFile" /> .</returns>
        Task<PageFile> InsertAsync(string fileName, byte[] file, SGuid pageId);

        /// <summary>
        /// Asynchronously updates the <see cref="PageFile" /> into the system.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="file">The file.</param>
        Task<bool> UpdateAsync(object id, string fileName, byte[] file);

        #endregion Methods
    }
}