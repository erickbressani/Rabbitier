namespace SampleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumer = new ConcreteConsumer2();
            consumer.Start();
        }
    }
}
