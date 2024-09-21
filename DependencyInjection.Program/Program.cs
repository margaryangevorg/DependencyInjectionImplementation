using DependencyInjection.Core;

namespace DependencyInjection.Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();

            collection.Add(typeof(SomeService), typeof(SomeService), ServiceLifetime.Transient);
            collection.Add(typeof(SomeServiceL2), typeof(SomeServiceL2), ServiceLifetime.Singleton);
            collection.Add(typeof(SomeServiceL3), typeof(SomeServiceL3), ServiceLifetime.Singleton);

            var provider = collection.Build();
            var obj = provider.GetService(typeof(SomeService));
        }
    }

    public class SomeService
    {
        public SomeService(SomeServiceL2 someServiceL2)
        {

        }
    }

    public class SomeServiceL2
    {
        public SomeServiceL2(SomeServiceL3 someServiceL3)
        {

        }
    }

    public class SomeServiceL3
    {
    }
}
