namespace DependencyInjection.Core
{
    public interface IServiceCollection
    {
        IServiceCollection Add(Type serviceType, Type implementationType, ServiceLifetime lifetime);

        IServiceProvider Build();
    }
}
