using Baasic.Client.Configuration;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Baasic.Client.Tests
{
    public class BaasicClientTests
    {
        [Fact()]
        public void BaasicClient_BaasicClientTest()
        {
            Mock<IDependencyResolver> dependencyResolver = new Mock<IDependencyResolver>();
            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            Mock<HttpClientFactory> httpClientFactory = new Mock<HttpClientFactory>(dependencyResolver.Object);

            BaasicClient target = new BaasicClient(clientConfiguration.Object, httpClientFactory.Object);
            target.Should().NotBeNull();
        }

        [Fact()]
        public void BaasicClient_CreateStringContentTest()
        {
            Mock<IDependencyResolver> dependencyResolver = new Mock<IDependencyResolver>();
            Mock<HttpClientFactory> httpClientFactory = new Mock<HttpClientFactory>(dependencyResolver.Object);
            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            clientConfiguration.Setup(p => p.DefaultEncoding).Returns(System.Text.Encoding.UTF8);

            BaasicClient target = new BaasicClient(clientConfiguration.Object, httpClientFactory.Object);
            target.Should().NotBeNull();

            var expected = target.CreateStringContent("Data", ClientConfiguration.HalJsonMediaType);
            expected.Should().NotBeNull();
        }

        [Fact()]
        public async Task BaasicClient_DeleteAsync_RequestUriOnly_Test()
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

            Mock<HttpClientFactory> httpClientFactory = new Mock<HttpClientFactory>(new Mock<IDependencyResolver>().Object);
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

            BaasicClient target = new BaasicClient(clientConfiguration.Object, httpClientFactory.Object);
            target.Should().NotBeNull();

            var expected = await target.DeleteAsync(target.GetApiUrl("/module/{0}", 0));
            expected.Should().BeFalse();

            expected = await target.DeleteAsync(target.GetApiUrl("/module/{0}", 1));
            expected.Should().BeTrue();
        }

        [Fact()]
        public async Task BaasicClient_DeleteAsync_Test()
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

            Mock<HttpClientFactory> httpClientFactory = new Mock<HttpClientFactory>(new Mock<IDependencyResolver>().Object);
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

            BaasicClient target = new BaasicClient(clientConfiguration.Object, httpClientFactory.Object);
            target.Should().NotBeNull();

            var expected = await target.DeleteAsync(target.GetApiUrl("/module/{0}", 0), new CancellationToken());
            expected.Should().BeFalse();

            expected = await target.DeleteAsync(target.GetApiUrl("/module/{0}", 1), new CancellationToken());
            expected.Should().BeTrue();
        }

        [Fact()]
        public void BaasicClient_Dispose_Test()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(httpResponseMessage);
            });

            Mock<HttpClientFactory> httpClientFactory = new Mock<HttpClientFactory>(new Mock<IDependencyResolver>().Object);
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

            BaasicClient target = new BaasicClient(clientConfiguration.Object, httpClientFactory.Object);
            target.Should().NotBeNull();

            Action execute = () => target.Dispose();
            execute.ShouldNotThrow<Exception>();
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
    }
}