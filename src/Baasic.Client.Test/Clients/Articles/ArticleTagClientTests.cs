using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Formatters;
using Baasic.Client.Infrastructure.DependencyInjection;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
using Baasic.Client.Modules.Articles;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Baasic.Client.Common.Configuration;

namespace Baasic.Client.ArticleModule.Tests
{
    [Trait("Module", "Article")]
    public class ArticleTagClientTests
    {
        #region Methods

        [Fact()]
        public void ArticleTagClient_ArticleTagClient_Test()
        {
            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
            httpClientFactory.Setup(p => p.Create()).Returns(() =>
            {
                return new HttpClient();
            });

            Mock<IBaasicClientFactory> baasicClientFactory = new Mock<IBaasicClientFactory>();
            baasicClientFactory.Setup(f => f.Create(It.IsAny<IClientConfiguration>())).Returns((IClientConfiguration config) => new BaasicClient(config, httpClientFactory.Object, new JsonFormatter()));

            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            clientConfiguration.Setup(p => p.DefaultMediaType).Returns(ClientConfiguration.HalJsonMediaType);
            clientConfiguration.Setup(p => p.DefaultTimeout).Returns(TimeSpan.FromSeconds(1));
            clientConfiguration.Setup(p => p.ApplicationIdentifier).Returns("Test");
            clientConfiguration.Setup(p => p.SecureBaseAddress).Returns("https://api.baasic.com/v1");
            clientConfiguration.Setup(p => p.BaseAddress).Returns("http://api.baasic.com/v1");

            ArticleTagClient target = new ArticleTagClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();
            target.Configuration.Should().NotBeNull();
        }

        [Fact()]
        public void ArticleTagClient_DeleteAsync_Test()
        {
            #region Setup

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (!request.RequestUri.ToString().EndsWith("tag"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                else if (request.RequestUri.ToString().EndsWith("tag"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(httpResponseMessage);
            });

            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
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

            ArticleTagClient target = new ArticleTagClient(clientConfiguration.Object, baasicClientFactory.Object);
            target.Should().NotBeNull();

            #endregion Setup

            target.DeleteAsync("NA").Result.Should().BeFalse();
            target.DeleteAsync("NA").Result.Should().BeFalse();
            target.DeleteAsync("tag").Result.Should().BeTrue();
        }

        [Fact()]
        public void ArticleTagClient_GetAsync_Find_Test()
        {
            #region Setup

            Guid tagId = Guid.NewGuid();

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("NA"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new CollectionModelBase<ArticleTag>()));
                }
                else if (request.RequestUri.ToString().EndsWith("Tag"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new CollectionModelBase<ArticleTag>() { Item = new List<ArticleTag>() { new ArticleTag() { Id = tagId, Slug = "Tag", Tag = "Tag" } } }));
                }
                return Task.FromResult(httpResponseMessage);
            });

            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
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

            ArticleTagClient target = new ArticleTagClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            var expected = target.FindAsync("NA", 1, 10, "", "").Result;
            expected.Should().NotBeNull();
            expected.Item.Should().NotBeNull();
            expected.Item.Count.Should().Be(0);

            expected = target.FindAsync("Tag", 1, 10, "", "").Result;
            expected.Should().NotBeNull();
            expected.Item.Should().NotBeNull();
            expected.Item.First().Tag.Should().Be("Tag");
        }

        [Fact()]
        public void ArticleTagClient_GetAsync_Test()
        {
            #region Setup

            Guid tagId = Guid.NewGuid();

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("NA"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    httpResponseMessage.Content = new StringContent("");
                }
                else if (request.RequestUri.ToString().EndsWith("Tag"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new ArticleTag() { Id = tagId, Slug = "Tag", Tag = "Tag" }));
                }
                return Task.FromResult(httpResponseMessage);
            });

            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
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

            ArticleTagClient target = new ArticleTagClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            target.GetAsync("NA", "").Result.Should().BeNull();
            var expected = target.GetAsync("Tag", "").Result;
            expected.Should().NotBeNull();
            expected.Tag.Should().Be("Tag");
        }

        [Fact()]
        public void ArticleTagClient_InsertAsync_Test()
        {
            #region Setup

            Guid tagId = Guid.NewGuid();

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().Contains("/articletag"))
                {
                    ArticleTag tag = new JsonFormatter().Deserialize<ArticleTag>(request.Content.ReadAsStreamAsync().Result);

                    if (tag == null)
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(""));
                    }
                    else if (tag != null &&
                        (tag.Id.Equals(Guid.Empty) || String.IsNullOrWhiteSpace(tag.Tag)))
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(""));
                    }
                    else
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new ArticleTag() { Id = tagId, Slug = "Tag", Tag = "Tag" }));
                    }
                }
                return Task.FromResult(httpResponseMessage);
            });

            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
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

            ArticleTagClient target = new ArticleTagClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            target.InsertAsync(null).Result.Should().BeNull();
            target.InsertAsync(new ArticleTag() { Id = tagId }).Result.Should().BeNull();
            target.InsertAsync(new ArticleTag() { Id = tagId, Slug = "Tag" }).Result.Should().BeNull();
            var expected = target.InsertAsync(new ArticleTag() { Id = tagId, Slug = "Tag", Tag = "Tag" }).Result;
            expected.Should().NotBeNull();
            expected.Id.ToString().Should().Be(tagId.ToString());
            expected.Tag.Should().Be("Tag");
            expected.Slug.Should().Be("Tag");
        }

        [Fact()]
        public void ArticleTagClient_UpdateAsync_Test()
        {
            #region Setup

            Guid tagId = Guid.NewGuid();

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().Contains("/articletag"))
                {
                    ArticleTag tag = new JsonFormatter().Deserialize<ArticleTag>(request.Content.ReadAsStreamAsync().Result);

                    if (tag == null)
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(""));
                    }
                    else if (tag != null &&
                        (tag.Id.Equals(Guid.Empty) || String.IsNullOrWhiteSpace(tag.Tag)))
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(""));
                    }
                    else
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new ArticleTag() { Id = tagId, Slug = tag.Slug, Tag = tag.Tag }));
                    }
                }
                return Task.FromResult(httpResponseMessage);
            });

            Mock<Core.HttpClientFactory> httpClientFactory = new Mock<Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
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

            ArticleTagClient target = new ArticleTagClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            target.UpdateAsync(null).Result.Should().BeNull();
            target.UpdateAsync(new ArticleTag() { Id = tagId }).Result.Should().BeNull();
            target.UpdateAsync(new ArticleTag() { Id = tagId, Slug = "Tag" }).Result.Should().BeNull();
            var expected = target.UpdateAsync(new ArticleTag() { Id = tagId, Slug = "Tag1", Tag = "Tag1" }).Result;
            expected.Should().NotBeNull();
            expected.Id.ToString().Should().Be(tagId.ToString());
            expected.Tag.Should().Be("Tag1");
            expected.Slug.Should().Be("Tag1");
        }

        #endregion Methods
    }
}