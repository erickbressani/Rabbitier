using Rabbitier.Configuration;
using Rabbitier.Consumer;
using Sample.Domain;

namespace SampleServer
{
    [ConsumerSettings("SampleQueue1", noAck: true)]
    public class ConcreteConsumer2 : RabbitierConsumer<Product>
    {
        public override void Consume(MessageReceived<Product> message)
        {
        }
    }
}
