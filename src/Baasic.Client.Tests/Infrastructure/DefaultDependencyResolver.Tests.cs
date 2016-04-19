using FluentAssertions;
using Moq;
using Xunit;
using Baasic.Client.Infrastructure.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baasic.Client.Core;
using Baasic.Client.Security.Token;
using Baasic.Client.Configuration;
using Baasic.Client.Modules.DynamicResource;
using Baasic.Client.Model;

namespace Baasic.Client.Tests.Infrastructure
{
    [Trait("Infrastructure", "Dependency-Injection")]
    public class DefaultDependencyResolverTests
    {
        private DefaultDependencyResolverFixture fixture;

        public DefaultDependencyResolverTests()
        {
            this.fixture = new DefaultDependencyResolverFixture();
        }

        [Fact]
        public void baasic_client_infrastructure_dependency_injection_ctor_test()
        {
            this.fixture.Target.Should().NotBeNull();
        }

        [Fact]
        public void baasic_client_infrastructure_dependency_injection_register_test()
        {
            this.fixture.Target.Should().NotBeNull();

            Action execute = () => { 
                var module = new DIModule();
                module.Load(this.fixture.Target);
            };
            execute.ShouldNotThrow();
        }

        [Fact]
        public void baasic_client_infrastructure_dependency_injection_get_service_simple_test()
        {
            this.fixture.Target.Should().NotBeNull();

            Action execute = () =>
            {
                var module = new DIModule();
                module.Load(this.fixture.Target);
            };
            execute.ShouldNotThrow();

            var expected = this.fixture.Target.GetService(typeof(IHttpClientFactory));
            expected.Should().NotBeNull();
        }

        [Fact]
        public void baasic_client_infrastructure_dependency_injection_get_service_complex_error_test()
        {
            this.fixture.Target.Should().NotBeNull();

            Action execute = () =>
            {
                var module = new DIModule();
                module.Load(this.fixture.Target);
            };
            execute.ShouldNotThrow();

            execute = () =>
            {
                var expected = this.fixture.Target.GetService(typeof(ITokenClient));
            };
            execute.ShouldThrow<Exception>();
        }

        [Fact]
        public void baasic_client_infrastructure_dependency_injection_get_service_complex_success_test()
        {
            this.fixture.Target.Should().NotBeNull();

            Action execute = () =>
            {
                var module = new DIModule();
                module.Load(this.fixture.Target);
            };
            execute.ShouldNotThrow();

            execute = () =>
            {
                this.fixture.Target.Register<IDependencyResolver>(() => this.fixture.Target);
                this.fixture.Target.Register<IClientConfiguration>(() => this.fixture.ClientConfiguration.Object);
                var expected = this.fixture.Target.GetService(typeof(ITokenClient));
            };
            execute.ShouldNotThrow();
        }

        [Fact]
        public void baasic_client_infrastructure_dependency_injection_get_service_complex_generic_success_test()
        {
            this.fixture.Target.Should().NotBeNull();

            Action execute = () =>
            {
                var module = new DIModule();
                module.Load(this.fixture.Target);
            };
            execute.ShouldNotThrow();

            execute = () =>
            {
                this.fixture.Target.Register<IDependencyResolver>(() => this.fixture.Target);
                this.fixture.Target.Register<IClientConfiguration>(() => this.fixture.ClientConfiguration.Object);
                var expected = this.fixture.Target.GetService(typeof(IDynamicResourceClient<DynamicModel>));
            };
            execute.ShouldNotThrow();
        }


        private class DefaultDependencyResolverFixture
        {
            public Mock<IClientConfiguration> ClientConfiguration = new Mock<IClientConfiguration>();
            public DefaultDependencyResolver Target;

            public DefaultDependencyResolverFixture()
            {
                Target = new DefaultDependencyResolver();
            }
        }


        private class DynamicModel : IModel
        {

            public DateTime DateCreated
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public DateTime DateUpdated
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public SGuid Id
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
