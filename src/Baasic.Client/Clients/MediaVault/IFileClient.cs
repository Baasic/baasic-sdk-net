using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.MediaVault;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.MediaVault
{
    /// <summary>
    /// The file client contract.
    /// </summary>
    public interface IFileClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="FileEntry" /> from the system.
        /// </summary>
        /// <param name="deleteRequests">The collection of delete request.</param>
        /// <returns>True if the collection <see cref="FileEntry" /> is deleted, false otherwise.</returns>
        Task<bool> BulkDeleteAsync(FileEntryDeleteRequest[] deleteRequests);

        /// <summary>
        /// Asynchronously deletes the <see cref="FileEntry" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="fileFormat">The file format.</param>
        /// <returns>True if <see cref="FileEntry" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id, object fileFormat = null);

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
        Task<CollectionModelBase<FileEntry>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

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
        /// <param name="fileExtensions">The file extensions.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="FileEntry" /> s.</returns>
        Task<CollectionModelBase<FileEntry>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            string fileName = null,
            DateTime? from = null, DateTime? to = null, string ids = null, int? minFileSize = null, int? maxFileSize = null,
            string fileExtensions = null, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

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
        /// <param name="fileExtensions">The file extensions.</param>
        /// <param name="page">The page number.</param>
        /// <param name="rpp">Records per blogPost limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = ClientBase.DefaultSearchQuery,
            string fileName = null,
            DateTime? from = null, DateTime? to = null, string ids = null, int? minFileSize = null, int? maxFileSize = null,
            string fileExtensions = null, int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields)
            where T : FileEntry;

        /// <summary>
        /// Asynchronously gets the <see cref="FileEntry" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="FileEntry" /> is returned, otherwise null.</returns>
        Task<FileEntry> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="FileEntry" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : FileEntry;

        /// <summary>
        /// Asynchronously insert the <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <param name="fileEntry">Resource instance.</param>
        /// <returns>Newly created <see cref="FileEntry" /> .</returns>
        Task<FileEntry> InsertAsync(FileEntry fileEntry);

        /// <summary>
        /// Asynchronously insert the <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="fileEntry">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T fileEntry) where T : FileEntry;

        /// <summary>
        /// Asynchronously insert the collection of <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <param name="fileEntries">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="FileEntry" /> .</returns>
        Task<FileEntry[]> InsertAsync(FileEntry[] fileEntries);

        /// <summary>
        /// Asynchronously insert the collection of <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="fileEntries">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        Task<T[]> InsertAsync<T>(T[] fileEntries) where T : FileEntry;

        /// <summary>
        /// Asynchronously update the <see cref="FileEntry" /> in the system.
        /// </summary>
        /// <param name="fileEntry">Resource instance.</param>
        /// <returns>True if <see cref="FileEntry" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync(FileEntry fileEntry);

        /// <summary>
        /// Asynchronously update the <see cref="FileEntry" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="fileEntry">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync<T>(T fileEntry) where T : FileEntry;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <param name="fileEntries">Resource instance.</param>
        /// <returns>Collection of updated <see cref="FileEntry" /> .</returns>
        Task<FileEntry[]> UpdateAsync(FileEntry[] fileEntries);

        /// <summary>
        /// Asynchronously updates the collection of <see cref="FileEntry" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="FileEntry" />.</typeparam>
        /// <param name="fileEntries">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task<T[]> UpdateAsync<T>(T[] fileEntries) where T : FileEntry;

        #endregion Methods
    }
}