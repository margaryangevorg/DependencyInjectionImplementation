using DependencyInjection.Core;
using FluentAssertions;
using Xunit;

namespace DependencyInjection.Test
{
    public class GeneralTests
    {
        [Fact]
        public void AddServices_Build_Works()
        {
            var collection = new ServiceCollection();

            collection.Add(typeof(Service1), typeof(Service1), ServiceLifetime.Transient);
            collection.Add(typeof(Service2), typeof(Service2), ServiceLifetime.Singleton);

            var provider = collection.Build();

            provider.Should().NotBeNull();
        }

        [Fact]
        public void AddServicesOnlyRoot_GetBack_Throws()
        {
            var collection = new ServiceCollection();

            collection.Add(typeof(Service1), typeof(Service1), ServiceLifetime.Transient);

            var provider = collection.Build();

            var getService = () => provider.GetService(typeof(Service1));
            getService.Should().Throw<Exception>();
        }

        [Fact]
        public void AddServices_GetBackUnregistered_ReturnsNull()
        {
            var collection = new ServiceCollection();

            collection.Add(typeof(Service1), typeof(Service1), ServiceLifetime.Transient);

            var provider = collection.Build();

            var service = provider.GetService(typeof(Service2));
            service.Should().BeNull();
        }

        [Fact]
        public void AddServicesAll_GetBack_Works()
        {
            var collection = new ServiceCollection();

            collection.Add(typeof(Service1), typeof(Service1), ServiceLifetime.Transient);
            collection.Add(typeof(Service2), typeof(Service2), ServiceLifetime.Transient);
            collection.Add(typeof(Service3), typeof(Service3), ServiceLifetime.Transient);

            var provider = collection.Build();

            var service = provider.GetService(typeof(Service1));
            service.Should().NotBeNull();
        }

        [Fact]
        public void AddServicesAll_GetBack_TransientWorks()
        {
            var collection = new ServiceCollection();

            collection.Add(typeof(Service1), typeof(Service1), ServiceLifetime.Transient);
            collection.Add(typeof(Service2), typeof(Service2), ServiceLifetime.Transient);
            collection.Add(typeof(Service3), typeof(Service3), ServiceLifetime.Transient);

            var provider = collection.Build();

            var service1 = provider.GetService(typeof(Service1));
            var service2 = provider.GetService(typeof(Service1));
            ReferenceEquals(service1, service2).Should().BeFalse();
        }

        [Fact]
        public void AddServicesAll_GetBack_SingletonWorks()
        {
            var collection = new ServiceCollection();

            collection.Add(typeof(Service1), typeof(Service1), ServiceLifetime.Singleton);
            collection.Add(typeof(Service2), typeof(Service2), ServiceLifetime.Transient);
            collection.Add(typeof(Service3), typeof(Service3), ServiceLifetime.Transient);

            var provider = collection.Build();

            var service1 = provider.GetService(typeof(Service1));
            var service2 = provider.GetService(typeof(Service1));
            ReferenceEquals(service1, service2).Should().BeTrue();
        }
    }
}
