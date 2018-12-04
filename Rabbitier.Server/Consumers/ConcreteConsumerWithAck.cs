using Rabbitier.Configuration;
using Rabbitier.Consumer;
using Sample.Domain;

namespace SampleServer
{
    [ConsumerSettings("SampleQueue1")]
    public class ConcreteConsumerWithAck : RabbitierConsumer<Product>
    {
        public override void Consume(MessageReceived<Product> message)
        {
        }
    }
}
