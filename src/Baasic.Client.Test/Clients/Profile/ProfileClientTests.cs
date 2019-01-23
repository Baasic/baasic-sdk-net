using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Formatters;
using Baasic.Client.Infrastructure.DependencyInjection;
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
using Baasic.Client.Common.Configuration;

namespace Baasic.Client.ProfileModule.Tests
{
    [Trait("Module", "Profile")]
    public class ProfileClientTests
    {
        ProfileClientFixture fixture;

        public ProfileClientTests()
        {
            this.fixture = new ProfileClientFixture();
        }

        [Fact]
        public void ProfileClient_ctor_test()
        {
            this.fixture.Target.Should().NotBeNull();
            this.fixture.Target.Configuration.Should().NotBeNull();            
        }

        public class ProfileClientFixture
        {
            public Mock<IBaasicClientFactory> BaasicClientFactory;
            public Mock<Baasic.Client.Core.HttpClientFactory> HttpClientFactory;
            public Mock<IClientConfiguration> ClientConfiguration;
            public ProfileClient Target;

            public ProfileClientFixture()
            {
                HttpClientFactory = new Mock<Baasic.Client.Core.HttpClientFactory>(new Mock<IDependencyResolver>().Object);
                HttpClientFactory.Setup(p => p.Create()).Returns(() =>
                {
                    return new HttpClient();
                });

                BaasicClientFactory = new Mock<IBaasicClientFactory>();
                BaasicClientFactory.Setup(f => f.Create(It.IsAny<IClientConfiguration>())).Returns((IClientConfiguration config) => new BaasicClient(config, HttpClientFactory.Object, new JsonFormatter()));

                ClientConfiguration = new Mock<IClientConfiguration>();
                ClientConfiguration.Setup(p => p.DefaultMediaType).Returns(Baasic.Client.Configuration.ClientConfiguration.HalJsonMediaType);
                ClientConfiguration.Setup(p => p.DefaultTimeout).Returns(TimeSpan.FromSeconds(1));
                ClientConfiguration.Setup(p => p.ApplicationIdentifier).Returns("Test");
                ClientConfiguration.Setup(p => p.SecureBaseAddress).Returns("https://api.baasic.com/v1");
                ClientConfiguration.Setup(p => p.BaseAddress).Returns("http://api.baasic.com/v1");

                Target = new ProfileClient(this.ClientConfiguration.Object, this.BaasicClientFactory.Object);

            }
        }
    }
}
