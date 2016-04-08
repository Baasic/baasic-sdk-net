using Baasic.Client.Core;
using Baasic.Client.Formatters;
using Baasic.Client.Infrastructure.Security;
using Baasic.Client.Membership;
using Baasic.Client.Modules.Articles;
using Baasic.Client.Modules.DynamicResource;
using Baasic.Client.Modules.KeyValues;
using Baasic.Client.Modules.Profile;
using Baasic.Client.Security.Token;
using System;
using System.Net.Http;

namespace Baasic.Client.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Dependency Injection Module containing Baasic Client bindings.
    /// </summary>
    public partial class DIModule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DIModule" /> class.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        public DIModule(IDependencyResolver dependencyResolver)
        {
            DependencyResolver = dependencyResolver;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the dependency resolver.
        /// </summary>
        /// <value>The dependency resolver.</value>
        protected IDependencyResolver DependencyResolver { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Load dependency injection bindings.
        /// </summary>
        public virtual void Load()
        {
            #region Core

            DependencyResolver.Register<HttpClient>(() => new HttpClient());
            DependencyResolver.Register<IHttpClientFactory, Baasic.Client.Core.HttpClientFactory>();
            DependencyResolver.Register<IBaasicClientFactory, BaasicClientFactory>();
            DependencyResolver.Register<IJsonFormatter, JsonFormatter>();

            #endregion Core

            #region Security

            DependencyResolver.Register<ITokenHandler, DefaultTokenHandler>();
            DependencyResolver.Register<IAuthenticationToken, AuthenticationToken>();
            DependencyResolver.Register<ITokenClient, TokenClient>();

            #endregion Security

            #region Clients

            DependencyResolver.Register<IKeyValueClient, KeyValueClient>();
            DependencyResolver.Register<IArticleClient, ArticleClient>();
            DependencyResolver.Register<IArticleTagClient, ArticleTagClient>();
            DependencyResolver.Register(typeof(IDynamicResourceClient<>), typeof(DynamicResourceClient<>));
            DependencyResolver.Register<IRoleClient, RoleClient>();
            DependencyResolver.Register<IUserClient, UserClient>();
            DependencyResolver.Register<IProfileClient, ProfileClient>();

            #endregion Clients
        }

        #endregion Methods
    }
}