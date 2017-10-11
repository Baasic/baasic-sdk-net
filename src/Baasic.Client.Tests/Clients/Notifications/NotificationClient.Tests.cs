using Baasic.Client.Configuration;
using Moq.Protected;
using Baasic.Client.Core;
using Baasic.Client.Formatters;
using Baasic.Client.Infrastructure.DependencyInjection;
using Baasic.Client.Model;
using Baasic.Client.Modules.Notifications;
using Baasic.Client.Modules.Profile;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using System.Net;
using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Common.Configuration;

namespace Baasic.Client.Tests.Notifications
{
    [Trait("Module", "Notification")]
    public class NotificationClientTests
    {
        NotificationClientFixture fixture;

        public NotificationClientTests()
        {
            this.fixture = new NotificationClientFixture();
        }

        [Fact]
        public void NotificationClient_ctor_test()
        {
            this.fixture.Target.Should().NotBeNull();
            this.fixture.Target.Configuration.Should().NotBeNull();            
        }

        [Fact]
        public async Task NotificationClient_single_publish_valid_test()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("/notifications/publish/"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Created);
                    httpResponseMessage.Content = new StringContent(true.ToString());
                }                
                return Task.FromResult(httpResponseMessage);
            });
            this.fixture.Handler = handler;

            this.fixture.Target.Should().NotBeNull();
            
            Notification notification = new Notification();
            notification.Channels = new string[] { "Channel1" };
            notification.TemplateId = "T1";
            notification.TemplateContext = new Newtonsoft.Json.Linq.JObject();

            var expected = await this.fixture.Target.PublishAsync(notification);            
            expected.Should().BeTrue();
        }

        [Fact]
        public void NotificationClient_single_publish_missing_args_test()
        {
            this.fixture.Target.Should().NotBeNull();

            Notification notification = new Notification();
            notification.TemplateId = "T1";
            
            Action execute = () => { if (this.fixture.Target.PublishAsync(notification).Result); };
            execute.ShouldThrow<ArgumentNullException>();

            notification.TemplateId = "";
            notification.Channels = new string[] { "Channel1" };
            execute.ShouldThrow<ArgumentNullException>();

            notification.Channels = new string[] { "Channel1" };
            notification.TemplateId = "T1";
            execute.ShouldNotThrow < ArgumentNullException>();
        }

        public class NotificationClientFixture
        {
            public Mock<IBaasicClientFactory> BaasicClientFactory;
            public Mock<HttpClientFactory> HttpClientFactory;
            public Mock<IClientConfiguration> ClientConfiguration;
            public Mock<HttpMessageHandler> Handler;
            public NotificationClient Target;

            public NotificationClientFixture()
            {
                Handler = new Mock<HttpMessageHandler>();
                HttpClientFactory = new Mock<HttpClientFactory>(new Mock<IDependencyResolver>().Object);
                HttpClientFactory.Setup(p => p.Create()).Returns(() =>
                {
                    return new HttpClient(Handler.Object);
                });

                BaasicClientFactory = new Mock<IBaasicClientFactory>();
                BaasicClientFactory.Setup(f => f.Create(It.IsAny<IClientConfiguration>())).Returns((IClientConfiguration config) => new BaasicClient(config, HttpClientFactory.Object, new JsonFormatter()));

                ClientConfiguration = new Mock<IClientConfiguration>();
                ClientConfiguration.Setup(p => p.DefaultMediaType).Returns(Baasic.Client.Configuration.ClientConfiguration.HalJsonMediaType);
                ClientConfiguration.Setup(p => p.DefaultTimeout).Returns(TimeSpan.FromSeconds(1));
                ClientConfiguration.Setup(p => p.ApplicationIdentifier).Returns("Test");
                ClientConfiguration.Setup(p => p.SecureBaseAddress).Returns("https://api.baasic.com/v1");
                ClientConfiguration.Setup(p => p.BaseAddress).Returns("http://api.baasic.com/v1");

                InitializeTarget();
            }

            public void InitializeTarget()
            {
                Target = new NotificationClient(this.ClientConfiguration.Object, this.BaasicClientFactory.Object);
            }
        }
    }
}
