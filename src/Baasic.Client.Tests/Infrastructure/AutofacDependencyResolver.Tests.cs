using Baasic.Client.AutoFac;
using Baasic.Client.Configuration;
using Baasic.Client.Core;
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
    [Trait("Infrastructure", "Dependency-Injection-Autofac")]
    public class AutofacDependencyResolverTests
    {
        private AutofacDependencyResolverFixture fixture;

        public AutofacDependencyResolverTests()
        {
            fixture = new AutofacDependencyResolverFixture();
        }

        [Fact]
        public void baasic_client_infrastructure_autofac_dependency_injection_ctor_test()
        {
            fixture.Target.Should().NotBeNull();
        }

        [Fact]
        public void baasic_client_infrastructure_autofac_dependency_injection_register_test()
        {
            Action execute = () =>
            {
                fixture.Target.Initialize();
                fixture.Settings.Build();
            };
            execute.ShouldNotThrow();
        }

        [Fact]
        public void baasic_client_infrastructure__autofac_dependency_injection_get_service_simple_test()
        {
            Action execute = () =>
            {
                fixture.Target.Initialize();
                fixture.Settings.Build();
            };
            execute.ShouldNotThrow();

            var factory1 = (IHttpClientFactory)fixture.Target.GetService(typeof(IHttpClientFactory));
            factory1.Should().NotBeNull();

            var created1 = factory1.Create();
            created1.Should().NotBeNull();

            var factory2 = fixture.Target.GetService<IHttpClientFactory>();
            factory2.Should().NotBeNull();

            var created2 = factory2.Create();
            created2.Should().NotBeNull();
        }

        [Fact]
        public void baasic_client_infrastructure_autofac_dependency_injection_get_service_complex_error_test()
        {
            Action execute = () =>
            {
                fixture.Target.Initialize();
                fixture.Settings.Build();
            };
            execute.ShouldNotThrow();

            execute = () =>
            {
                var expected = fixture.Target.GetService(typeof(ITokenClient));
            };
            execute.ShouldThrow<Exception>();
        }

        [Fact]
        public void baasic_client_infrastructure_autofac_dependency_injection_get_service_complex_success_test()
        {
            Action execute = () =>
            {
                fixture.Target.Initialize();
                fixture.Target.Register(() => fixture.ClientConfiguration.Object);

                fixture.Settings.Build();
            };
            execute.ShouldNotThrow();

            execute = () =>
            {
                var expected = fixture.Target.GetService(typeof(ITokenClient));
                expected.Should().NotBeNull();
            };
            execute.ShouldNotThrow();
        }

        [Fact]
        public void baasic_client_infrastructure_autofac_dependency_injection_get_service_complex_generic_success_test()
        {
            Action execute = () =>
            {
                fixture.Target.Initialize();
                fixture.Target.Register(() => this.fixture.ClientConfiguration.Object);

                fixture.Settings.Build();
            };
            execute.ShouldNotThrow();

            execute = () =>
            {
                var expected = this.fixture.Target.GetService(typeof(IDynamicResourceClient<DynamicModel>));
            };
            execute.ShouldNotThrow();
        }

        [Fact]
        public void baasic_client_infrastructure_autofac_dependency_injection_get_multiple_services_simple_test()
        {
            Action execute = () =>
            {
                fixture.Target.Register<IGenericInterface, GenericServiceA>();
                fixture.Target.Register<IGenericInterface, GenericServiceB>();

                fixture.Settings.Build();
            };
            execute.ShouldNotThrow();

            var services1 = fixture.Target.GetServices<IGenericInterface>();
            services1.Should().NotBeNullOrEmpty();
            services1.Should().HaveCount(2);

            var services2 = fixture.Target.GetServices(typeof(IGenericInterface));
            services2.Should().NotBeNullOrEmpty();
            services2.Should().HaveCount(2);
        }

        private class AutofacDependencyResolverFixture
        {
            public AutoFacSettings Settings;
            public AutofacDependencyResolver Target;
            public Mock<IClientConfiguration> ClientConfiguration = new Mock<IClientConfiguration>();

            public AutofacDependencyResolverFixture()
            {
                Settings = new AutoFacSettings();
                Target = new AutofacDependencyResolver(Settings);
            }
        }

        private interface IGenericInterface
        {
        }

        private class GenericServiceA : IGenericInterface
        {
        }

        private class GenericServiceB : IGenericInterface
        {
        }
    }
}