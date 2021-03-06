using Baasic.Client.Clients.Blogs;
using Baasic.Client.Clients.CMS;
using Baasic.Client.Clients.MediaVault;
using Baasic.Client.Clients.Profile;
using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Common.Infrastructure.Security;
using Baasic.Client.Core;
using Baasic.Client.Formatters;
using Baasic.Client.Infrastructure.Security;
using Baasic.Client.Membership;
using Baasic.Client.Modules.Articles;
using Baasic.Client.Modules.DynamicResource;
using Baasic.Client.Modules.KeyValues;
using Baasic.Client.Modules.Notifications;
using Baasic.Client.Modules.Profile;
using Baasic.Client.Security.Token;
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
            dependencyResolver.Register<IBaasicPlatformClient, BaasicPlatformClient>();
            dependencyResolver.Register<IBaasicPlatformClientFactory, BaasicPlatformClientFactory>();
            dependencyResolver.Register<IJsonFormatter, JsonFormatter>();

            dependencyResolver.Register<ITokenHandler, DefaultTokenHandler>();
            dependencyResolver.Register<IPlatformTokenHandler, DefaultPlatformTokenHandler>();
            dependencyResolver.Register<IAuthenticationToken, AuthenticationToken>();
            dependencyResolver.Register<ITokenClient, TokenClient>();
            dependencyResolver.Register<IPlatformTokenClient, PlatformTokenClient>();

            dependencyResolver.Register<IKeyValueClient, KeyValueClient>();
            dependencyResolver.Register<IArticleSettingsClient, ArticleSettingsClient>();
            dependencyResolver.Register<IArticleClient, ArticleClient>();
            dependencyResolver.Register<IArticleCommentsClient, ArticleCommentsClient>();
            dependencyResolver.Register<IArticleCommentRepliesClient, ArticleCommentRepliesClient>();
            dependencyResolver.Register<IArticleTagClient, ArticleTagClient>();
            dependencyResolver.Register(typeof(IDynamicResourceClient<>), typeof(DynamicResourceClient<>));

            dependencyResolver.Register<IRoleClient, RoleClient>();
            dependencyResolver.Register<IUserClient, UserClient>();
            dependencyResolver.Register<IUserRegistrationClient, UserRegistrationClient>();
            dependencyResolver.Register<IUserPasswordRecoveryClient, UserPasswordRecoveryClient>();

            dependencyResolver.Register<IProfileClient, ProfileClient>();
            dependencyResolver.Register<ILanguageClient, LanguageClient>();

            dependencyResolver.Register<INotificationClient, NotificationClient>();

            dependencyResolver.Register<IPageClient, PageClient>();
            dependencyResolver.Register<IPageStatusClient, PageStatusClient>();
            dependencyResolver.Register<IPageFileClient, PageFileClient>();
            dependencyResolver.Register<IPageFileStreamClient, PageFileStreamClient>();
            dependencyResolver.Register<IMenuClient, MenuClient>();
            dependencyResolver.Register<INavigationClient, NavigationClient>();

            dependencyResolver.Register<IBlogClient, BlogClient>();
            dependencyResolver.Register<IBlogPostClient, BlogPostClient>();
            dependencyResolver.Register<IBlogPostStatusClient, BlogPostStatusClient>();
            dependencyResolver.Register<IBlogPostFileClient, BlogPostFileClient>();
            dependencyResolver.Register<IBlogTagClient, BlogTagClient>();
            dependencyResolver.Register<IBlogPostFileStreamClient, BlogPostFileStreamClient>();

            dependencyResolver.Register<IFileClient, FileClient>();
            dependencyResolver.Register<IFileStreamClient, FileStreamClient>();
        }

        #endregion Methods
    }
}