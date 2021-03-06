﻿using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Blogs;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.Blogs
{
    /// <summary>
    /// The blog post client contract.
    /// </summary>
    public interface IBlogPostClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="BlogPost" /> from the system.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        /// <returns>True if the collection <see cref="BlogPost" /> is deleted, false otherwise.</returns>
        Task<bool> BulkDeleteAsync(object ids);

        /// <summary>
        /// Asynchronously deletes the <see cref="BlogPost" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="BlogPost" /> is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(object id);

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
        Task<CollectionModelBase<BlogPost>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

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
        /// <param name="blogPostStatusIds">The blog post status ids.</param>
        /// <param name="languageIds">The language ids.</param>
        /// <param name="blogSlugs">The blog slugs.</param>
        /// <param name="tagSlugs">The tag slugs.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="BlogPost" /> s.</returns>
        Task<CollectionModelBase<BlogPost>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, DateTime? publishedFrom = null, DateTime? publishedTo = null,
            string ids = null, string url = null, string template = null, string blogPostStatusIds = null, string languageIds = null,
            string blogSlugs = null, string tagSlugs = null,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

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
        /// <param name="blogPostStatusIds">The blog post status ids.</param>
        /// <param name="languageIds">The language ids.</param>
        /// <param name="blogSlugs">The blog slugs.</param>
        /// <param name="tagSlugs">The tag slugs.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = ClientBase.DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, DateTime? publishedFrom = null, DateTime? publishedTo = null, string ids = null,
            string url = null, string template = null, string blogPostStatusIds = null, string languageIds = null,
            string blogSlugs = null, string tagSlugs = null,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields)
            where T : BlogPost;

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPost" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="BlogPost" /> is returned, otherwise null.</returns>
        Task<BlogPost> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="BlogPost" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object id, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : BlogPost;

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <param name="blogPost">The blog post.</param>
        /// <returns>Newly created <see cref="BlogPost" /> .</returns>
        Task<BlogPost> InsertAsync(BlogPost blogPost);

        /// <summary>
        /// Asynchronously insert the <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="blogPost">The blog post.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> InsertAsync<T>(T blogPost) where T : BlogPost;

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <param name="blogPost">The blog post.</param>
        /// <returns>Collection of newly created <see cref="BlogPost" /> .</returns>
        Task<BatchResult<BlogPost>[]> InsertAsync(BlogPost[] blogPost);

        /// <summary>
        /// Asynchronously insert the collection of <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="blogPost">The blog post.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        Task<BatchResult<T>[]> InsertAsync<T>(T[] blogPost) where T : BlogPost;

        /// <summary>
        /// Asynchronously update the <see cref="BlogPost" /> in the system.
        /// </summary>
        /// <param name="blogPost">The blog post.</param>
        /// <returns>True if <see cref="BlogPost" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync(BlogPost blogPost);

        /// <summary>
        /// Asynchronously update the <see cref="BlogPost" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="blogPost">The blog post.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        Task<bool> UpdateAsync<T>(T blogPost) where T : BlogPost;

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <param name="blogPost">The blog post.</param>
        /// <returns>Collection of updated <see cref="BlogPost" /> .</returns>
        Task<BatchResult<BlogPost>[]> UpdateAsync(BlogPost[] blogPost);

        /// <summary>
        /// Asynchronously updates the collection of <see cref="BlogPost" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="BlogPost" />.</typeparam>
        /// <param name="blogPost">The blog post.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        Task<BatchResult<T>[]> UpdateAsync<T>(T[] blogPost) where T : BlogPost;

        #endregion Methods
    }
}