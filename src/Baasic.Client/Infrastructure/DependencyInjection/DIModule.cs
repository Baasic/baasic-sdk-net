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
    public partial class DIModule : IDIModule
    {
        #region Methods

        /// <summary>
        /// Load dependency injection bindings.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        public virtual void Load(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.Register<HttpClient, HttpClient>();
            dependencyResolver.Register<IHttpClientFactory, Baasic.Client.Core.HttpClientFactory>();
            dependencyResolver.Register<IBaasicClient, BaasicClient>();
            dependencyResolver.Register<IBaasicClientFactory, BaasicClientFactory>();
            dependencyResolver.Register<IJsonFormatter, JsonFormatter>();

            dependencyResolver.Register<ITokenHandler, DefaultTokenHandler>();
            dependencyResolver.Register<IAuthenticationToken, AuthenticationToken>();
            dependencyResolver.Register<ITokenClient, TokenClient>();

            dependencyResolver.Register<IKeyValueClient, KeyValueClient>();
            dependencyResolver.Register<IArticleClient, ArticleClient>();
            dependencyResolver.Register<IArticleTagClient, ArticleTagClient>();
            dependencyResolver.Register(typeof(IDynamicResourceClient<>), typeof(DynamicResourceClient<>));

            dependencyResolver.Register<IRoleClient, RoleClient>();
            dependencyResolver.Register<IUserClient, UserClient>();
            dependencyResolver.Register<IUserRegistrationClient, UserRegistrationClient>();
            dependencyResolver.Register<IUserPasswordRecoveryClient, UserPasswordRecoveryClient>();

            dependencyResolver.Register<IProfileClient, ProfileClient>();
        }

        #endregion Methods
    }
}