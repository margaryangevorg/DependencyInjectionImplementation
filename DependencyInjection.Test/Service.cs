namespace DependencyInjection.Test
{
    public class Service1
    {
        public Service1(Service2 someServiceL2)
        {

        }
    }

    public class Service2
    {
        public Service2(Service3 someServiceL3)
        {

        }
    }

    public class Service3
    {
    }
}
