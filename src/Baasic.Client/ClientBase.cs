using Baasic.Client.Configuration;
using Baasic.Client.Utility;
using System;

namespace Baasic.Client
{
    /// <summary>
    /// Base Module Client.
    /// </summary>
    public abstract class ClientBase
    {
        #region Fields

        /// <summary>
        /// Default embed.
        /// <para>Default value is <see cref="String.Empty"/> .</para>
        /// </summary>
        public const string DefaultEmbed = "";

        /// <summary>
        /// Default page.
        /// <para>Default value is 1.</para>
        /// </summary>
        public const int DefaultPage = 1;

        /// <summary>
        /// Default search query.
        /// <para>Default value is <see cref="String.Empty"/> .</para>
        /// </summary>
        public const string DefaultSearchQuery = "";

        /// <summary>
        /// Default sorting.
        /// <para>Default value is <see cref="String.Empty"/> .</para>
        /// </summary>
        public const string DefaultSorting = "";

        /// <summary>
        /// Maximum number of results.
        /// <para>Default value is 100.</para>
        /// </summary>
        public const int MaxNumberOfResults = 10;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets client configuration.
        /// </summary>
        public IClientConfiguration Configuration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the module relative path.
        /// </summary>
        protected abstract string ModuleRelativePath { get; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientBase"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ClientBase(IClientConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Initialize query string.
        /// </summary>
        /// <param name="uriBuilder">Uri builder.</param>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page index.</param>
        /// <param name="rpp">Records per page.</param>
        /// <param name="sort">Sort expression.</param>
        /// <param name="embed">Embedded items.</param>
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

        #endregion Methods
    }
}