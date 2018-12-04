namespace SampleServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var consumer = new ConcreteConsumerWithoutAck();
            consumer.Start();
        }
    }
}
