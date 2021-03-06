﻿using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Modules.Articles
{
    /// <summary>
    /// Article Tag Module Client.
    /// </summary>
    public interface IArticleTagClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously removes the <see cref="ArticleTag" /> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <returns>True if <see cref="ArticleTag" /> is removed, otherwise false.</returns>
        Task<bool> DeleteAsync(object key);

        /// <summary>
        /// Asynchronously find <see cref="ArticleTag" /> entries.
        /// </summary>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="ArticleTag" /> .</returns>
        Task<CollectionModelBase<ArticleTag>> FindAsync(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously find <see cref="ArticleTag" /> entries.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleTag" />.</typeparam>
        /// <param name="searchQuery">Search phrase or query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <typeparamref name="T" /> .</returns>
        Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = ClientBase.DefaultSearchQuery,
            int page = ClientBase.DefaultPage, int rpp = ClientBase.DefaultMaxNumberOfResults,
            string sort = ClientBase.DefaultSorting, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields) where T : ArticleTag;

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleTag" /> from the system.
        /// </summary>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="ArticleTag" /> is returned, otherwise null.</returns>
        Task<ArticleTag> GetAsync(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleTag" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleTag" />.</typeparam>
        /// <param name="key">Key (Id or Slug).</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> GetAsync<T>(object key, string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously adds the <see cref="ArticleTag" /> .
        /// </summary>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <see cref="ArticleTag" /> is returned, otherwise null.</returns>
        Task<ArticleTag> InsertAsync(ArticleTag entry);

        /// <summary>
        /// Asynchronously adds the <see cref="ArticleTag" /> .
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleTag" />.</typeparam>
        /// <param name="entry">The tag entry.</param>
        /// <returns>If tag is added <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> InsertAsync<T>(T entry) where T : ArticleTag;

        /// <summary>
        /// Asynchronously update the <see cref="ArticleTag" /> .
        /// </summary>
        /// <param name="tag">The new or existing <see cref="ArticleTag" /> .</param>
        /// <returns>If tag is updated <see cref="ArticleTag" /> is returned, otherwise null.</returns>
        Task<ArticleTag> UpdateAsync(ArticleTag tag);

        /// <summary>
        /// Asynchronously update the <see cref="ArticleTag" /> .
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="ArticleTag" />.</typeparam>
        /// <param name="tag">The new or existing <see cref="ArticleTag" /> .</param>
        /// <returns>If tag is updated <typeparamref name="T" /> is returned, otherwise null.</returns>
        Task<T> UpdateAsync<T>(T tag) where T : ArticleTag;

        #endregion Methods
    }
}