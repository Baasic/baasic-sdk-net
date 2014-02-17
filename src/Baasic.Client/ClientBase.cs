using Baasic.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client
{
    /// <summary>
    /// Base Module Client.
    /// </summary>
    public abstract class ClientBase
    {
        #region Fields
        /// <summary>
        /// Maximum number of results.
        /// <para>
        /// Default value is 100.
        /// </para>
        /// </summary>
        public const int MaxNumberOfResults = 10;

        /// <summary>
        /// Default page.
        /// <para>
        /// Default value is 1.
        /// </para>
        /// </summary>
        public const int DefaultPage = 1;

        /// <summary>
        /// Default sorting.
        /// <para>
        /// Default value is <see cref="String.Empty"/>.
        /// </para>
        /// </summary>
        public const string DefaultSorting = "";

        /// <summary>
        /// Default embed.
        /// <para>
        /// Default value is <see cref="String.Empty"/>.
        /// </para>
        /// </summary>
        public const string DefaultEmbed = "";

        /// <summary>
        /// Default search query.
        /// <para>
        /// Default value is <see cref="String.Empty"/>.
        /// </para>
        /// </summary>
        public const string DefaultSearchQuery = "";
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets client configuration.
        /// </summary>
        public IClientConfiguration Configuration { get; set; }

        protected abstract string ModuleRelativePath { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Client configuration.</param>
        public ClientBase(IClientConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Methods
        protected virtual void InitializeQueryString(UrlBuilder uriBuilder, 
            string searchQuery,
            int page, int rpp,
            string sort, string embed)
        {
            if (!String.IsNullOrWhiteSpace(searchQuery))
                uriBuilder.QueryString.Add("searchQuery", searchQuery);
            if (!DefaultPage.Equals(page))
                uriBuilder.QueryString.Add("page", page.ToString());
            if (!MaxNumberOfResults.Equals(rpp))
                uriBuilder.QueryString.Add("rpp", rpp.ToString());
            if (!String.IsNullOrWhiteSpace(sort))
                uriBuilder.QueryString.Add("sort", sort);
            if (!String.IsNullOrWhiteSpace(embed))
                uriBuilder.QueryString.Add("embed", embed);
        }
        #endregion
    }
}
