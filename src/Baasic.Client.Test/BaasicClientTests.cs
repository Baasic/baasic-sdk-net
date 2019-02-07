using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Formatters;
using Baasic.Client.Infrastructure.DependencyInjection;
using Baasic.Client.Infrastructure.Security;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Common.Infrastructure.Security;

namespace Baasic.Client.Tests
{
    [Trait("Core", "Core")]
    public class BaasicClientTests
    {
        #region Methods

        [Fact()]
        public async Task BaasicClient_attach_authentication_token()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                var authorizationHeader = request.Headers.Authorization;
                authorizationHeader.Should().NotBeNull();
                authorizationHeader.Scheme.Should().Be("bearer");
                authorizationHeader.Parameter.Should().Be("testToken");

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });

            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
            httpClientFactory.Setup(p => p.Create()).Returns(() =>
            {
                return new HttpClient(handler.Object);
            });

            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            clientConfiguration.Setup(p => p.DefaultMediaType).Returns(ClientConfiguration.HalJsonMediaType);
            clientConfiguration.Setup(p => p.DefaultTimeout).Returns(TimeSpan.FromSeconds(1));
            clientConfiguration.Setup(p => p.ApplicationIdentifier).Returns("Test");
            clientConfiguration.Setup(p => p.SecureBaseAddress).Returns("https://api.baasic.com/v1");
            clientConfiguration.Setup(p => p.BaseAddress).Returns("http://api.baasic.com/v1");

            var mockTokenHandler = new Mock<ITokenHandler>();
            mockTokenHandler.Setup(h => h.Get()).Returns(new AuthenticationToken()
            {
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                Scheme = "bearer",
                Token = "testToken"
            });

            clientConfiguration.Setup(p => p.TokenHandler).Returns(mockTokenHandler.Object);

            BaasicClient target = new BaasicClient(clientConfiguration.Object, httpClientFactory.Object, new JsonFormatter());

            var requestUrl = target.GetApiUrl("anyRequest");

            await target.GetAsync<object>(requestUrl);
            await target.PostAsync<object>(requestUrl, new object());
            await target.PutAsync<object>(requestUrl, new object());
            await target.DeleteAsync(requestUrl);
            await target.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUrl));
        }

        [Fact()]
        public void BaasicClient_constructor_test()
        {
            Mock<IDependencyResolver> dependencyResolver = new Mock<IDependencyResolver>();
            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(dependencyResolver.Object);
            var jsonFormatter = new JsonFormatter();

            BaasicClient target = new BaasicClient(clientConfiguration.Object, httpClientFactory.Object, jsonFormatter);
            target.Should().NotBeNull();
        }

        [Fact()]
        public async Task BaasicClient_DeleteAsync_RequestUriOnly_test()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("/module/0"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                else if (request.RequestUri.ToString().EndsWith("/module/1"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(httpResponseMessage);
            });

            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
            httpClientFactory.Setup(p => p.Create()).Returns(() =>
            {
                return new HttpClient(handler.Object);
            });

            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            clientConfiguration.Setup(p => p.DefaultMediaType).Returns(ClientConfiguration.HalJsonMediaType);
            clientConfiguration.Setup(p => p.DefaultTimeout).Returns(TimeSpan.FromSeconds(1));
            clientConfiguration.Setup(p => p.ApplicationIdentifier).Returns("Test");
            clientConfiguration.Setup(p => p.SecureBaseAddress).Returns("https://api.baasic.com/v1");
            clientConfiguration.Setup(p => p.BaseAddress).Returns("http://api.baasic.com/v1");

            var jsonFormatter = new JsonFormatter();

            BaasicClient target = new BaasicClient(clientConfiguration.Object, httpClientFactory.Object, jsonFormatter);
            target.Should().NotBeNull();

            Action execute = () => { if (target.DeleteAsync(target.GetApiUrl("/module/{0}", 0)).Result) { }; };
            execute.Should().Throw<HttpRequestException>();

            var expected = await target.DeleteAsync(target.GetApiUrl("/module/{0}", 1));
            expected.Should().BeTrue();
        }

        [Fact()]
        public async Task BaasicClient_DeleteAsync_test()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("/module/0"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                else if (request.RequestUri.ToString().EndsWith("/module/1"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(httpResponseMessage);
            });

            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
            httpClientFactory.Setup(p => p.Create()).Returns(() =>
                {
                    return new HttpClient(handler.Object);
                });

            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            clientConfiguration.Setup(p => p.DefaultMediaType).Returns(ClientConfiguration.HalJsonMediaType);
            clientConfiguration.Setup(p => p.DefaultTimeout).Returns(TimeSpan.FromSeconds(1));
            clientConfiguration.Setup(p => p.ApplicationIdentifier).Returns("Test");
            clientConfiguration.Setup(p => p.SecureBaseAddress).Returns("https://api.baasic.com/v1");
            clientConfiguration.Setup(p => p.BaseAddress).Returns("http://api.baasic.com/v1");

            var jsonFormatter = new JsonFormatter();

            BaasicClient target = new BaasicClient(clientConfiguration.Object, httpClientFactory.Object, jsonFormatter);
            target.Should().NotBeNull();

            Action execute = () => { if (target.DeleteAsync(target.GetApiUrl("/module/{0}", 0), new CancellationToken()).Result) { }; };
            execute.Should().Throw<HttpRequestException>();

            var expected = await target.DeleteAsync(target.GetApiUrl("/module/{0}", 1), new CancellationToken());
            expected.Should().BeTrue();
        }

        [Fact()]
        public void BaasicClient_Dispose_test()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(httpResponseMessage);
            });

            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
            httpClientFactory.Setup(p => p.Create()).Returns(() =>
            {
                return new HttpClient(handler.Object);
            });

            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            clientConfiguration.Setup(p => p.DefaultMediaType).Returns(ClientConfiguration.HalJsonMediaType);
            clientConfiguration.Setup(p => p.DefaultTimeout).Returns(TimeSpan.FromSeconds(1));
            clientConfiguration.Setup(p => p.ApplicationIdentifier).Returns("Test");
            clientConfiguration.Setup(p => p.SecureBaseAddress).Returns("https://api.baasic.com/v1");
            clientConfiguration.Setup(p => p.BaseAddress).Returns("http://api.baasic.com/v1");

            var jsonFormatter = new JsonFormatter();

            BaasicClient target = new BaasicClient(clientConfiguration.Object, httpClientFactory.Object, jsonFormatter);
            target.Should().NotBeNull();

            Action execute = () => target.Dispose();
            execute.Should().NotThrow<Exception>();
        }

        [Fact()]
        public void BaasicClient_GetApiUrlTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BaasicClient_GetApiUrlTest1()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BaasicClient_GetAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BaasicClient_GetAsyncTest1()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BaasicClient_GetSecureApiUrlTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BaasicClient_PostAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BaasicClient_PostAsyncTest1()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BaasicClient_PutAsyncTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BaasicClient_PutAsyncTest1()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}