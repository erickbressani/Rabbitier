namespace SampleServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var subscriber = new ConcreteSubscriberWithoutAck();
            subscriber.Start();
        }
    }
}
