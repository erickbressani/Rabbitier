using Rabbitier.Configuration;
using Rabbitier.Consumer;
using SampleServer.Entities;

namespace SampleServer
{
    [ConsumerSettings("SampleQueue1")]
    public class ConcreteConsumer : RabbitierConsumer<Product>
    {
        public override void Consume(MessageReceived<Product> message)
        {
        }
    }
}
