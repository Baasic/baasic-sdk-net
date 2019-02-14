using Baasic.Client.Common;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Model;
using Baasic.Client.Model.CMS;
using Baasic.Client.Utility;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Baasic.Client.Clients.CMS
{
    /// <summary>
    /// The navigation client class.
    /// </summary>
    public class NavigationClient : ClientBase, INavigationClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public NavigationClient(IClientConfiguration configuration,
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
            get { return "cms/navigations"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously deletes the <see cref="Navigation" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="Navigation" /> is deleted, false otherwise.</returns>
        public virtual async Task<bool> DeleteAsync(object id)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return await client.DeleteAsync(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
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
        /// Asynchronously find <see cref="Navigation" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Navigation" /> s.</returns>
        public virtual Task<CollectionModelBase<Navigation>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync(searchQuery, null, null, null, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="Navigation" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Navigation" /> s.</returns>
        public virtual Task<CollectionModelBase<Navigation>> FindAsync(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<Navigation>(searchQuery, from, to, ids, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="Navigation" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="tags">The article tags.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        public virtual async Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : Navigation
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "from", from);
                InitializeQueryStringPair(uriBuilder, "to", to);
                InitializeQueryStringPair(uriBuilder, "ids", ids);
                var result = await client.GetAsync<CollectionModelBase<T>>(uriBuilder.ToString());
                if (result == null)
                {
                    result = new CollectionModelBase<T>();
                }
                return result;
            }
        }

        /// <summary>
        /// Asynchronously gets the <see cref="Navigation" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="Navigation" /> is returned, otherwise null.</returns>
        public virtual Task<Navigation> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetAsync<Navigation>(key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="Navigation" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetAsync<T>(object id, string embed = DefaultEmbed, string fields = DefaultFields) where T : Navigation
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="Navigation" /> into the system.
        /// </summary>
        /// <param name="navigation">Resource instance.</param>
        /// <returns>Newly created <see cref="Navigation" /> .</returns>
        public virtual Task<Navigation> InsertAsync(Navigation navigation)
        {
            return InsertAsync<Navigation>(navigation);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="Navigation" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigation">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T navigation) where T : Navigation
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T>(client.GetApiUrl(ModuleRelativePath), navigation);
            }
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="Navigation" /> .</returns>
        public virtual Task<Navigation[]> InsertAsync(Navigation[] navigations)
        {
            return InsertAsync<Navigation>(navigations);
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> InsertAsync<T>(T[] navigations) where T : Navigation
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), navigations);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="Navigation" /> in the system.
        /// </summary>
        /// <param name="page">Resource instance.</param>
        /// <returns>True if <see cref="Navigation" /> is successfully updated, false otherwise.</returns>
        public virtual Task<bool> UpdateAsync(Navigation navigation)
        {
            return UpdateAsync<Navigation>(navigation);
        }

        /// <summary>
        /// Asynchronously update the <see cref="Navigation" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigation">Resource instance.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync<T>(T navigation) where T : Navigation
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    var result = await client.PutAsync<Navigation, HttpStatusCode>(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, navigation.Id)), navigation);
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
        /// Asynchronously updates the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of updated <see cref="Navigation" /> .</returns>
        public virtual Task<Navigation[]> UpdateAsync(Navigation[] navigations)
        {
            return UpdateAsync<Navigation>(navigations);
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> UpdateAsync<T>(T[] navigations) where T : Navigation
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), navigations);
            }
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task UpdateMenuNavigationAsync(Guid menuId, Navigation[] navigations)
        {
            return UpdateMenuNavigationAsync<Navigation>(menuId, navigations);
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Navigation" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Navigation" />.</typeparam>
        /// <param name="navigations">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task UpdateMenuNavigationAsync<T>(Guid menuId, T[] navigations) where T : Navigation
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync(client.GetApiUrl(String.Format("{0}/batch/menu/{1}", ModuleRelativePath, menuId)), navigations);
            }
        }

        #endregion Methods
    }
}