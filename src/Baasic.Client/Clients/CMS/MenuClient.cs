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
    public class MenuClient : ClientBase, IMenuClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        public MenuClient(IClientConfiguration configuration,
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
            get { return "cms/menus"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously deletes the collection <see cref="Menu" /> from the system.
        /// </summary>
        /// <param name="ids">The collection of identifiers.</param>
        /// <returns>True if the collection <see cref="Menu" /> is deleted, false otherwise.</returns>
        public virtual Task<bool> BulkDeleteAsync(object ids)
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    return client.DeleteAsync(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), ids);
                }
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return Task.FromResult(false);
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes the <see cref="Menu" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if <see cref="Menu" /> is deleted, false otherwise.</returns>
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
        /// Asynchronously find <see cref="Menu" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Menu" /> s.</returns>
        public virtual Task<CollectionModelBase<Menu>> FindAsync(string searchQuery = DefaultSearchQuery,
            int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync(searchQuery, null, null, null, null, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="Menu" /> s.
        /// </summary>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The from date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="positions">The menu positions.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>List of <see cref="Menu" /> s.</returns>
        public virtual Task<CollectionModelBase<Menu>> FindAsync(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            string positions = null, int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return FindAsync<Menu>(searchQuery, from, to, ids, positions, page, rpp, sort, embed, fields);
        }

        /// <summary>
        /// Asynchronously find <see cref="Menu" /> s.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="searchQuery">Search query.</param>
        /// <param name="from">The form date.</param>
        /// <param name="to">The to date.</param>
        /// <param name="ids">The file ids.</param>
        /// <param name="positions">The menu positions.</param>
        /// <param name="tags">The article tags.</param>
        /// <param name="page">Page number.</param>
        /// <param name="rpp">Records per page limit.</param>
        /// <param name="sort">Sort by field.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>Collection of <typeparamref name="T" /> s.</returns>
        public virtual async Task<CollectionModelBase<T>> FindAsync<T>(string searchQuery = DefaultSearchQuery,
            DateTime? from = null, DateTime? to = null, string ids = null,
            string positions = null, int page = DefaultPage, int rpp = DefaultMaxNumberOfResults,
            string sort = DefaultSorting, string embed = DefaultEmbed, string fields = DefaultFields)
            where T : Menu
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryString(uriBuilder, searchQuery, page, rpp, sort, embed, fields);
                InitializeQueryStringPair(uriBuilder, "from", from);
                InitializeQueryStringPair(uriBuilder, "to", to);
                InitializeQueryStringPair(uriBuilder, "positions", positions);
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
        /// Asynchronously gets the <see cref="Menu" /> from the system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <see cref="Menu" /> is returned, otherwise null.</returns>
        public virtual Task<Menu> GetAsync(object key, string embed = DefaultEmbed, string fields = DefaultFields)
        {
            return GetAsync<Menu>(key, embed, fields);
        }

        /// <summary>
        /// Asynchronously gets the <see cref="Menu" /> from the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="embed">Embed related resources.</param>
        /// <param name="fields">The fields to include in response.</param>
        /// <returns>If found <typeparamref name="T" /> is returned, otherwise null.</returns>
        public virtual Task<T> GetAsync<T>(object id, string embed = DefaultEmbed, string fields = DefaultFields) where T : Menu
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{{0}}", ModuleRelativePath), id));
                InitializeQueryString(uriBuilder, embed, fields);
                return client.GetAsync<T>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously insert the <see cref="Menu" /> into the system.
        /// </summary>
        /// <param name="navigations">Resource instance.</param>
        /// <param name="forcePositionsUpdate">True if menu needs to be saved on position no matter of existing menus.</param>
        /// <returns>Newly created <see cref="Menu" /> .</returns>
        public virtual Task<Menu> InsertAsync(Menu menu, bool? forcePositionsUpdate = null)
        {
            return InsertAsync<Menu>(menu, forcePositionsUpdate);
        }

        /// <summary>
        /// Asynchronously insert the <see cref="Menu" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="menu">Resource instance.</param>
        /// <param name="forcePositionsUpdate">True if menu needs to be saved on position no matter of existing menus.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> InsertAsync<T>(T menu, bool? forcePositionsUpdate = null) where T : Menu
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(ModuleRelativePath));
                InitializeQueryStringPair(uriBuilder, "forcePositionsUpdate", forcePositionsUpdate);
                return client.PostAsync<T>(uriBuilder.ToString(), menu);
            }
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Menu" /> into the system.
        /// </summary>
        /// <param name="menus">Resource instance.</param>
        /// <returns>Collection of newly created <see cref="Menu" /> .</returns>
        public virtual Task<Menu[]> InsertAsync(Menu[] menus)
        {
            return InsertAsync<Menu>(menus);
        }

        /// <summary>
        /// Asynchronously insert the collection of <see cref="Menu" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="menus">Resource instance.</param>
        /// <returns>Collection of newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> InsertAsync<T>(T[] menus) where T : Menu
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PostAsync<T[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), menus);
            }
        }

        /// <summary>
        /// Asynchronously update the <see cref="Menu" /> in the system.
        /// </summary>
        /// <param name="page">Resource instance.</param>
        /// <param name="forcePositionsUpdate">True if menu needs to be saved on position no matter of existing menus.</param>
        /// <returns>True if <see cref="Menu" /> is successfully updated, false otherwise.</returns>
        public virtual Task<bool> UpdateAsync(Menu menu, bool? forcePositionsUpdate = null)
        {
            return UpdateAsync<Menu>(menu, forcePositionsUpdate);
        }

        /// <summary>
        /// Asynchronously update the <see cref="Menu" /> in the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="menu">Resource instance.</param>
        /// <param name="forcePositionsUpdate">True if menu needs to be saved on position no matter of existing menus.</param>
        /// <returns>True if <typeparamref name="T" /> is successfully updated, false otherwise.</returns>
        public virtual async Task<bool> UpdateAsync<T>(T menu, bool? forcePositionsUpdate = null) where T : Menu
        {
            try
            {
                using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
                {
                    UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(String.Format("{0}/{1}", ModuleRelativePath, menu.Id)));
                    InitializeQueryStringPair(uriBuilder, "forcePositionsUpdate", forcePositionsUpdate);
                    var result = await client.PutAsync<Menu, HttpStatusCode>(uriBuilder.ToString(), menu);
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
        /// Asynchronously updates the collection of <see cref="Menu" /> into the system.
        /// </summary>
        /// <param name="menus">Resource instance.</param>
        /// <returns>Collection of updated <see cref="Menu" /> .</returns>
        public virtual Task<Menu[]> UpdateAsync(Menu[] menus)
        {
            return UpdateAsync<Menu>(menus);
        }

        /// <summary>
        /// Asynchronously updates the collection of <see cref="Menu" /> into the system.
        /// </summary>
        /// <typeparam name="T">Type of extended <see cref="Menu" />.</typeparam>
        /// <param name="menus">Resource instance.</param>
        /// <returns>Collection of updated <typeparamref name="T" /> .</returns>
        public virtual Task<T[]> UpdateAsync<T>(T[] menus) where T : Menu
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                return client.PutAsync<T[]>(client.GetApiUrl(String.Format("{0}/batch", ModuleRelativePath)), menus);
            }
        }

        #endregion Methods
    }
}