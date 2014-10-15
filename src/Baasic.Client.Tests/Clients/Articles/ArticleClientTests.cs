using Baasic.Client.Configuration;
using Baasic.Client.Formatters;
using Baasic.Client.Model;
using Baasic.Client.Model.Articles;
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

namespace Baasic.Client.ArticleModule.Tests
{
    public class ArticleClientTests
    {
        #region Methods

        #region Article

        [Fact()]
        public void ArticleClient_ArchiveAsync_test()
        {
            #region Setup

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("/archive/NA"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    httpResponseMessage.Content = new StringContent(false.ToString());
                }
                else if (request.RequestUri.ToString().EndsWith("/archive/Slug"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StringContent(true.ToString());
                }
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.ArchiveAsync("NA").Result.Should().BeFalse(); };
            execute.ShouldThrow<HttpRequestException>();

            target.ArchiveAsync("Slug").Result.Should().BeTrue();
        }

        [Fact()]
        public void ArticleClient_constructor_test()
        {
            Mock<HttpClientFactory> httpClientFactory = new Mock<HttpClientFactory>(new Mock<IDependencyResolver>().Object);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();
            target.Configuration.Should().NotBeNull();
        }

        [Fact()]
        public void ArticleClient_DeleteAsync_test()
        {
            #region Setup

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("/NA"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                else if (request.RequestUri.ToString().EndsWith("/Slug"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.DeleteAsync("NA").Result.Should().BeFalse(); };
            execute.ShouldThrow<HttpRequestException>();
            target.DeleteAsync("Slug").Result.Should().BeTrue();
        }

        [Fact()]
        public void ArticleClient_GetAsync_find_test()
        {
            #region Setup

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("NA"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new CollectionModelBase<Article>()));
                }
                else if (request.RequestUri.ToString().EndsWith("Slug"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new CollectionModelBase<Article>() { Item = new List<Article>() { new Article() { Slug = "Slug" } } }));
                }
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.GetAsync("NA", 1, 10, "", "").Result.Should().NotBeNull(); };
            execute.ShouldThrow<HttpRequestException>();

            var expected = target.GetAsync("Slug", 1, 10, "", "").Result;
            expected.Should().NotBeNull();
            expected.Item.Should().NotBeNull();
            expected.Item.First().Slug.Should().Be("Slug");
        }

        [Fact()]
        public void ArticleClient_GetAsync_test()
        {
            #region Setup

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("NA"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    httpResponseMessage.Content = new StringContent("");
                }
                else if (request.RequestUri.ToString().EndsWith("Slug"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new Article() { Slug = "Slug" }));
                }
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.GetAsync("NA", "").Result.Should().BeNull(); };
            execute.ShouldThrow<HttpRequestException>();

            var expected = target.GetAsync("Slug", "").Result;
            expected.Should().NotBeNull();
            expected.Slug.Should().Be("Slug");
        }

        [Fact()]
        public void ArticleClient_InsertAsync_test()
        {
            #region Setup

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("article"))
                {
                    Article article = new JsonFormatter().Deserialize<Article>(request.Content.ReadAsStreamAsync().Result);

                    if (article == null)
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(""));
                    }
                    else
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new Article() { Slug = "Slug" }));
                    }
                }
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.InsertAsync(null).Result.Should().NotBeNull(); };
            execute.ShouldThrow<HttpRequestException>();

            var expected = target.InsertAsync(new Article() { Id = Guid.NewGuid(), Slug = "Slug" }).Result;
            expected.Should().NotBeNull();
            expected.Slug.Should().Be("Slug");
        }

        [Fact()]
        public void ArticleClient_PublishAsync_test()
        {
            #region Setup

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("publish/NA"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                else if (request.RequestUri.ToString().EndsWith("publish/Slug"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.PublishAsync("NA").Result.Should().BeFalse(); };
            execute.ShouldThrow<HttpRequestException>();

            target.PublishAsync("Slug").Result.Should().BeTrue();
        }

        [Fact()]
        public void ArticleClient_RestoreAsync_test()
        {
            #region Setup

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("restore/NA"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                else if (request.RequestUri.ToString().EndsWith("restore/Slug"))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.RestoreAsync("NA").Result.Should().BeFalse(); };
            execute.ShouldThrow<HttpRequestException>();

            target.RestoreAsync("Slug").Result.Should().BeTrue();
        }

        [Fact()]
        public void ArticleClient_UpdateAsync_test()
        {
            #region Setup

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("article"))
                {
                    Article article = new JsonFormatter().Deserialize<Article>(request.Content.ReadAsStreamAsync().Result);

                    if (article == null)
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(""));
                    }
                    else
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new Article() { Slug = "Slug" }));
                    }
                }
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.UpdateAsync(null).Result.Should().NotBeNull(); };
            execute.ShouldThrow<HttpRequestException>();

            var expected = target.UpdateAsync(new Article() { Id = Guid.NewGuid(), Slug = "Slug" }).Result;
            expected.Should().NotBeNull();
            expected.Slug.Should().Be("Slug");
        }

        #endregion Article

        #region Tag

        [Fact()]
        public void ArticleClient_AddTagToArticleAsync_Tag_test()
        {
            #region Setup

            Guid articleId = Guid.NewGuid();
            string tag = "tag";

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                string url = articleId.ToString() + "/tag/" + tag;
                if (!request.RequestUri.ToString().Contains(url))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                else if (request.RequestUri.ToString().EndsWith(url))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new ArticleTagEntry() { Id = Guid.NewGuid(), ArticleId = articleId, TagId = Guid.NewGuid() }));
                }
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.AddTagToArticleAsync(Guid.Empty, "").Result.Should().BeNull(); };
            execute.ShouldThrow<HttpRequestException>();

            execute = () => { target.AddTagToArticleAsync(Guid.Empty, tag).Result.Should().BeNull(); };
            execute.ShouldThrow<HttpRequestException>();

            var expected = target.AddTagToArticleAsync(articleId, tag).Result;
            expected.Should().NotBeNull();
            expected.ArticleId.Should().Be(articleId);
        }

        [Fact()]
        public void ArticleClient_AddTagToArticleAsync_test()
        {
            #region Setup

            Guid articleId = Guid.NewGuid();
            Guid tagId = Guid.NewGuid();
            Guid tagEntryId = Guid.NewGuid();

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().Contains("/article/"))
                {
                    ArticleTagEntry tagEntry = new JsonFormatter().Deserialize<ArticleTagEntry>(request.Content.ReadAsStreamAsync().Result);

                    if (tagEntry == null)
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(""));
                    }
                    else if (tagEntry != null &&
                        (tagEntry.Id.Equals(Guid.Empty) || tagEntry.TagId.Equals(Guid.Empty) || tagEntry.ArticleId.Equals(Guid.Empty)))
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(""));
                    }
                    else
                    {
                        httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                        httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new ArticleTagEntry() { Id = tagEntryId, ArticleId = articleId, TagId = tagId }));
                    }
                }
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.AddTagToArticleAsync(null).Result.Should().BeNull(); };
            execute.ShouldThrow<HttpRequestException>();

            execute = () => { target.AddTagToArticleAsync(new ArticleTagEntry() { Id = tagEntryId, ArticleId = Guid.Empty, TagId = tagId }).Result.Should().BeNull(); };
            execute.ShouldThrow<HttpRequestException>();
            execute = () => { target.AddTagToArticleAsync(new ArticleTagEntry() { Id = tagEntryId, ArticleId = articleId, TagId = Guid.Empty }).Result.Should().BeNull(); };
            execute.ShouldThrow<HttpRequestException>();

            var expected = target.AddTagToArticleAsync(new ArticleTagEntry() { Id = tagEntryId, ArticleId = articleId, TagId = tagId }).Result;
            expected.Should().NotBeNull();
            expected.Id.ToString().Should().Be(tagEntryId.ToString());
            expected.ArticleId.Should().Be(articleId);
            expected.TagId.Should().Be(tagId);
        }

        [Fact()]
        public void ArticleClient_GetTagEntriesAsync_find_test()
        {
            #region Setup

            Guid articleId = Guid.NewGuid();
            Guid tagId = Guid.NewGuid();
            Guid tagEntryId = Guid.NewGuid();

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (request.RequestUri.ToString().EndsWith("NA"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new CollectionModelBase<ArticleTagEntry>()));
                }
                else if (request.RequestUri.ToString().EndsWith("Tag"))
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new CollectionModelBase<ArticleTagEntry>() { Item = new List<ArticleTagEntry>() { new ArticleTagEntry() { Id = tagEntryId, ArticleId = articleId, TagId = tagId } } }));
                }
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.GetTagEntriesAsync("NA", 1, 10, "", "").Result.Should().NotBeNull(); };
            execute.ShouldThrow<HttpRequestException>();

            var expected = target.GetTagEntriesAsync("Tag", 1, 10, "", "").Result;
            expected.Should().NotBeNull();
            expected.Item.Should().NotBeNull();
            expected.Item.First().TagId.Should().Be(tagId);
        }

        [Fact()]
        public void ArticleClient_GetTagEntryAsync_test()
        {
            #region Setup

            Guid articleId = Guid.NewGuid();
            Guid tagId = Guid.NewGuid();
            Guid tagEntryId = Guid.NewGuid();

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
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(new ArticleTagEntry() { Id = tagEntryId, ArticleId = articleId, TagId = tagId }));
                }
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);

            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.GetTagEntryAsync(Guid.Empty, "NA", "").Result.Should().BeNull(); };
            execute.ShouldThrow<HttpRequestException>();

            var expected = target.GetTagEntryAsync(articleId, "Tag", "").Result;
            expected.Should().NotBeNull();
            expected.TagId.Should().Be(tagId);
        }

        [Fact()]
        public void ArticleClient_RemoveAllTagsFromArticleAsync_test()
        {
            #region Setup

            Guid articleId = Guid.NewGuid();

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Returns((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                string url = articleId.ToString() + "/tag";
                if (!request.RequestUri.ToString().EndsWith(url))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                else if (request.RequestUri.ToString().EndsWith(url))
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                return Task.FromResult(httpResponseMessage);
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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);
            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.RemoveAllTagsFromArticleAsync(Guid.Empty).Result.Should().BeFalse(); };
            execute.ShouldThrow<HttpRequestException>();
            target.RemoveAllTagsFromArticleAsync(articleId).Result.Should().BeTrue();
        }

        [Fact()]
        public void ArticleClient_RemoveTagFromArticleAsync_test()
        {
            #region Setup

            Guid articleId = Guid.NewGuid();

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

            ArticleClient target = new ArticleClient(clientConfiguration.Object, baasicClientFactory.Object);
            target.Should().NotBeNull();

            #endregion Setup

            Action execute = () => { target.RemoveTagFromArticleAsync(Guid.Empty, "NA").Result.Should().BeFalse(); };
            execute.ShouldThrow<HttpRequestException>();

            execute = () => { target.RemoveTagFromArticleAsync(articleId, "NA").Result.Should().BeFalse(); };
            execute.ShouldThrow<HttpRequestException>();

            target.RemoveTagFromArticleAsync(articleId, "tag").Result.Should().BeTrue();
        }

        #endregion Tag

        #endregion Methods
    }
}