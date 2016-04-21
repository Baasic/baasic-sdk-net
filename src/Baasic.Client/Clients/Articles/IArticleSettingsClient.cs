using Baasic.Client.Core;
using Baasic.Client.Model.Articles;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Modules.Articles
{
    /// <summary>
    /// Article Settings Module Client.
    /// </summary>
    public interface IArticleSettingsClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously gets the <see cref="ArticleSettings" /> from the system.
        /// </summary>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="ArticleSettings" /> is returned, otherwise null.</returns>
        Task<ArticleSettings> GetAsync(string embed = ClientBase.DefaultEmbed, string fields = ClientBase.DefaultFields);

        /// <summary>
        /// Asynchronously update the <see cref="ArticleSettings" /> .
        /// </summary>
        /// <param name="settings">The new or existing <see cref="ArticleSettings" /> .</param>
        /// <returns>True if successfully updated <see cref="ArticleSettings" />.</returns>
        Task<bool> UpdateAsync(ArticleSettings settings);

        #endregion Methods
    }
}