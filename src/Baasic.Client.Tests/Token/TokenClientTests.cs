using Baasic.Client.Configuration;
using Baasic.Client.Formatters;
using Baasic.Client.TokenHandler;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Baasic.Client.Token.Tests
{
    public class TokenClientTests
    {
        [Fact()]
        public void TokenClient_constructor_test()
        {
            var conf = new Mock<IClientConfiguration>();
            var baasicClientFactory = new Mock<IBaasicClientFactory>();
            var jsonFormatter = new JsonFormatter();

            var target = new TokenClient(conf.Object, baasicClientFactory.Object, jsonFormatter);

            target.Should().NotBeNull();
        }

        [Fact()]
        public async Task TokenClient_create_new_token_test()
        {
            var tokenEndPoint = "https://api.baasic.com/v1/Test/Login";

            var username = "user";
            var password = "pass";

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns(async (HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                request.RequestUri.AbsoluteUri.Should().Be(tokenEndPoint);
                request.Content.Headers.ContentType.MediaType.Should().Be("application/x-www-form-urlencoded");
                request.Method.Should().Be(HttpMethod.Post);

                var content = await request.Content.ReadAsStringAsync();
                var values = content.Split('&').ToDictionary(k => k.Split('=')[0], v => v.Split('=')[1]);

                values["grant_type"].Should().Be("password");
                values["username"].Should().Be(username);
                values["password"].Should().Be(password);

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(@"{
                        token_type: ""bearer"",
                        access_token: ""accessToken"",
                        expires_in: 7200
                    }", System.Text.UTF8Encoding.UTF8, "application/json")
                };
            });

            Mock<HttpClientFactory> httpClientFactory = new Mock<HttpClientFactory>(new Mock<IDependencyResolver>().Object);
            httpClientFactory.Setup(p => p.Create()).Returns(() =>
            {
                return new HttpClient(handler.Object);
            });

            Mock<IBaasicClientFactory> baasicClientFactory = new Mock<IBaasicClientFactory>();
            baasicClientFactory.Setup(f => f.Create(It.IsAny<IClientConfiguration>())).Returns((IClientConfiguration config) => new BaasicClient(config, httpClientFactory.Object, new JsonFormatter()));

            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            clientConfiguration.Setup(p => p.DefaultMediaType).Returns(ClientConfiguration.HalJsonMediaType);
            clientConfiguration.Setup(p => p.DefaultTimeout).Returns(TimeSpan.FromSeconds(1));
            clientConfiguration.Setup(p => p.ApplicationIdentifier).Returns("Test");
            clientConfiguration.Setup(p => p.SecureBaseAddress).Returns("https://api.baasic.com/v1");
            clientConfiguration.Setup(p => p.BaseAddress).Returns("http://api.baasic.com/v1");

            var target = new TokenClient(clientConfiguration.Object, baasicClientFactory.Object, new JsonFormatter());

            var token = await target.CreateAsync(username, password);

            token.IsValid.Should().BeTrue();
            token.Scheme.Should().Be("bearer");
            token.Token.Should().Be("accessToken");

            var mockTokenHandler = new Mock<ITokenHandler>();
            mockTokenHandler.Setup(h => h.Save(It.IsAny<IAuthenticationToken>())).Returns((IAuthenticationToken tokenToSave) =>
            {
                tokenToSave.IsValid.Should().BeTrue();
                tokenToSave.Scheme.Should().Be("bearer");
                tokenToSave.Token.Should().Be("accessToken");

                return true;
            });

            clientConfiguration.Setup(p => p.TokenHandler).Returns(mockTokenHandler.Object);

            target = new TokenClient(clientConfiguration.Object, baasicClientFactory.Object, new JsonFormatter());

            await target.CreateAsync(username, password);
        }

        [Fact()]
        public async Task TokenClient_destroy_token_test()
        {
            var tokenEndPoint = "https://api.baasic.com/v1/Test/Login";

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns(async (HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                request.RequestUri.AbsoluteUri.Should().Be(tokenEndPoint);
                request.Content.Should().NotBeNull();
                request.Content.Headers.ContentType.MediaType.Should().Be("application/json");
                request.Method.Should().Be(HttpMethod.Delete);

                var content = await request.Content.ReadAsStringAsync();

                var deleteContent = JsonConvert.DeserializeObject<JObject>(content);

                var tokenProp = deleteContent.Property("token");
                tokenProp.Should().NotBeNull();
                tokenProp.Value.ToObject<string>().Should().Be("testToken");

                var typeProp = deleteContent.Property("type");
                typeProp.Should().NotBeNull();
                typeProp.Value.ToObject<string>().Should().Be("bearer");

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            });

            Mock<HttpClientFactory> httpClientFactory = new Mock<HttpClientFactory>(new Mock<IDependencyResolver>().Object);
            httpClientFactory.Setup(p => p.Create()).Returns(() =>
            {
                return new HttpClient(handler.Object);
            });

            Mock<IBaasicClientFactory> baasicClientFactory = new Mock<IBaasicClientFactory>();
            baasicClientFactory.Setup(f => f.Create(It.IsAny<IClientConfiguration>())).Returns((IClientConfiguration config) => new BaasicClient(config, httpClientFactory.Object, new JsonFormatter()));

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
            var tokenCleared = false;
            mockTokenHandler.Setup(h => h.Clear()).Returns(() =>
            {
                tokenCleared = true;

                return true;
            });

            clientConfiguration.Setup(p => p.TokenHandler).Returns(mockTokenHandler.Object);

            var target = new TokenClient(clientConfiguration.Object, baasicClientFactory.Object, new JsonFormatter());

            var isDeleted = await target.DestroyAsync();
            isDeleted.Should().BeTrue();

            tokenCleared.Should().BeTrue();
        }

        [Fact()]
        public async Task TokenClient_refresh_token_test()
        {
            var tokenEndPoint = "https://api.baasic.com/v1/Test/Login";

            var username = "user";
            var password = "pass";

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns(async (HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                request.RequestUri.AbsoluteUri.Should().Be(tokenEndPoint);
                request.Content.Headers.ContentType.MediaType.Should().Be("application/json");
                request.Method.Should().Be(HttpMethod.Put);

                var content = await request.Content.ReadAsStringAsync();

                var refreshContent = JsonConvert.DeserializeObject<JObject>(content);

                var tokenProp = refreshContent.Property("token");
                tokenProp.Should().NotBeNull();
                tokenProp.Value.ToObject<string>().Should().Be("testToken");

                var typeProp = refreshContent.Property("type");
                typeProp.Should().NotBeNull();
                typeProp.Value.ToObject<string>().Should().Be("bearer");

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(@"{
                        token_type: ""bearer"",
                        access_token: ""newAccessToken"",
                        expires_in: 7200
                    }", System.Text.UTF8Encoding.UTF8, "application/json")
                };
            });

            Mock<HttpClientFactory> httpClientFactory = new Mock<HttpClientFactory>(new Mock<IDependencyResolver>().Object);
            httpClientFactory.Setup(p => p.Create()).Returns(() =>
            {
                return new HttpClient(handler.Object);
            });

            Mock<IBaasicClientFactory> baasicClientFactory = new Mock<IBaasicClientFactory>();
            baasicClientFactory.Setup(f => f.Create(It.IsAny<IClientConfiguration>())).Returns((IClientConfiguration config) => new BaasicClient(config, httpClientFactory.Object, new JsonFormatter()));

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
            mockTokenHandler.Setup(h => h.Save(It.IsAny<IAuthenticationToken>())).Returns((IAuthenticationToken tokenToSave) =>
            {
                tokenToSave.IsValid.Should().BeTrue();
                tokenToSave.Scheme.Should().Be("bearer");
                tokenToSave.Token.Should().Be("newAccessToken");

                return true;
            });

            clientConfiguration.Setup(p => p.TokenHandler).Returns(mockTokenHandler.Object);

            var target = new TokenClient(clientConfiguration.Object, baasicClientFactory.Object, new JsonFormatter());

            var token = await target.RefreshAsync();

            token.IsValid.Should().BeTrue();
            token.Scheme.Should().Be("bearer");
            token.Token.Should().Be("newAccessToken");
        }
    }
}