using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Infrastructure.DependencyInjection;
using Baasic.Client.Modules.DynamicResource;
using Baasic.Client.Security.Token;
using Baasic.Client.Tests.Infrastructure.Common;
using FluentAssertions;
using Moq;
using System;
using Xunit;
using Baasic.Client.Common.Configuration;

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

            Action execute = () =>
            {
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

            this.fixture.Target.Register<IDependencyResolver>(() => this.fixture.Target);
            this.fixture.Target.Register<IClientConfiguration>(() => this.fixture.ClientConfiguration.Object);
            var expected = this.fixture.Target.GetService(typeof(IHttpClientFactory));
            expected.Should().NotBeNull();
        }

        [Fact]
        public void baasic_client_infrastructure_dependency_injection_get_service_error_test()
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
                var expected = this.fixture.Target.GetService(typeof(IHttpClientFactory));
            };
            execute.ShouldThrow<Exception>();
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
    }
}