using Baasic.Client.ArticleModule;
using Baasic.Client.DynamicResource;
using Baasic.Client.Formatters;
using Baasic.Client.KeyValueModule;
using Baasic.Client.Token;
using Baasic.Client.TokenHandler;
using Ninject.Modules;

using System;

namespace Baasic.Client.Ninject
{
    /// <summary>
    /// Dependency Injection Module containing Baasic Client bindings.
    /// </summary>
    public partial class DIModule : NinjectModule
    {
        #region Methods

        /// <summary>
        /// Load dependency injection bindings.
        /// </summary>
        public override void Load()
        {
            Bind<IDependencyResolver>().To<BaasicClientDependencyResolver>();
            Bind<IHttpClientFactory>().To<HttpClientFactory>();
            Bind<IBaasicClientFactory>().To<BaasicClientFactory>();
            Bind<IBaasicClient>().To<BaasicClient>();
            Bind<IAuthenticationToken>().To<AuthenticationToken>();
            Bind<ITokenHandler>().To<DefaultTokenHandler>();
            Bind<IJsonFormatter>().To<JsonFormatter>();
            Bind<ITokenClient>().To<TokenClient>();
            Bind<IKeyValueClient>().To<KeyValueClient>();
            Bind<IArticleClient>().To<ArticleClient>();
            Bind<IArticleTagClient>().To<ArticleTagClient>();
            Bind(typeof(IDynamicResourceClient<>)).To(typeof(DynamicResourceClient<>));
        }

        #endregion Methods
    }
}