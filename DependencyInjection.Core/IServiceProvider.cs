namespace DependencyInjection.Core
{
    public interface IServiceProvider
    {
        object GetService(Type serviceType);
    }
}
