namespace DependencyInjection.Core
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly List<ServiceDescriptor> _serviceDescriptors = new List<ServiceDescriptor>();

        public IServiceCollection Add(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(serviceType, implementationType, lifetime));
            return this;
        }

        public IServiceProvider Build()
        {
            return new ServiceProvider(_serviceDescriptors);
        }
    }
}
