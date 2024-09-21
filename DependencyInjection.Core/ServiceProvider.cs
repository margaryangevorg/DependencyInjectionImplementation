namespace DependencyInjection.Core
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, ServiceDescriptor> _serviceDescriptors = new Dictionary<Type, ServiceDescriptor>();
        private readonly Dictionary<Type, object> _serviceInstances = new Dictionary<Type, object>();

        public ServiceProvider(List<ServiceDescriptor> serviceDescriptors)
        {
            foreach (var descriptor in serviceDescriptors)
            {
                _serviceDescriptors[descriptor.ServiceType] = descriptor;
            }
        }

        public object GetService(Type serviceType)
        {
            if (!_serviceDescriptors.TryGetValue(serviceType, out var descriptor)) return null;

            if (descriptor.Lifetime == ServiceLifetime.Singleton && _serviceInstances.ContainsKey(serviceType))
            {
                return _serviceInstances[serviceType];
            }

            if (descriptor.Lifetime == ServiceLifetime.Singleton)
            {
                var instance = CreateInstance(descriptor.ImplementationType);
                _serviceInstances[serviceType] = instance;
                return instance;
            }

            if (descriptor.Lifetime == ServiceLifetime.Transient)
            {
                return CreateInstance(descriptor.ImplementationType);
            }

            return null;
        }

        private object CreateInstance(Type implementationType)
        {
            var constructors = implementationType.GetConstructors();

            if (constructors.Length == 0)
            {
                return Activator.CreateInstance(implementationType);
            }

            var constructor = constructors[0];
            var parameters = constructor.GetParameters();
            var parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameterValues[i] = GetService(parameters[i].ParameterType);

                if (parameterValues[i] == null)
                {
                    throw new InvalidOperationException();
                }
            }

            return constructor.Invoke(parameterValues);
        }
    }
}
