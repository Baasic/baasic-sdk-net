using Baasic.Client.Configuration;
using Baasic.Client.Internals;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Baasic.Client.Tests
{
    public class BaasicClientTests
    {
        [Fact()]
        public void BaasicClient_BaasicClientTest()
        {
            Mock<IFactory> factory = new Mock<IFactory>();
            BaasicClient target = new BaasicClient(factory.Object);
            target.Should().NotBeNull();
        }

        [Fact()]
        public void BaasicClient_CreateStringContentTest()
        {
            Mock<IFactory> factory = new Mock<IFactory>();
            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            clientConfiguration.Setup(p => p.DefaultEncoding).Returns(System.Text.Encoding.UTF8);
            factory.Setup(p => p.CreateClientConfiguration()).Returns(clientConfiguration.Object);
            BaasicClient target = new BaasicClient(factory.Object);
            target.Should().NotBeNull();

            var expected = target.CreateStringContent("Data", ClientConfiguration.HalJsonMediaType);
            expected.Should().NotBeNull();
        }

        [Fact()]
        public async Task BaasicClient_DeleteAsyncTest()
        {
            Mock<IFactory> factory = new Mock<IFactory>();
            Mock<IClientConfiguration> clientConfiguration = new Mock<IClientConfiguration>();
            clientConfiguration.Setup(p => p.DefaultMediaType).Returns(ClientConfiguration.HalJsonMediaType);
            clientConfiguration.Setup(p => p.DefaultTimeout).Returns(TimeSpan.FromSeconds(1));
            clientConfiguration.Setup(p => p.ApplicationIdentifier).Returns("Test");
            clientConfiguration.Setup(p => p.SecureBaseAddress).Returns("https://api.baasic.com/v1");
            clientConfiguration.Setup(p => p.BaseAddress).Returns("http://api.baasic.com/v1");
            factory.Setup(p => p.CreateClientConfiguration()).Returns(clientConfiguration.Object);
            BaasicClient target = new BaasicClient(factory.Object);
            target.Should().NotBeNull();

            var expected = await target.DeleteAsync(target.GetApiUrl("/module/{0}", 12));
            expected.Should().BeFalse();
        }

        [Fact()]
        public void BaasicClient_DeleteAsyncTest1()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BaasicClient_DisposeTest()
        {
            throw new NotImplementedException();
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