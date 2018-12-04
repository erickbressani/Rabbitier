using Rabbitier.Configuration;
using Rabbitier.Consumer;
using Sample.Domain;

namespace SampleServer
{
    [ConsumerSettings("SampleQueue1")]
    public class ConcreteConsumer1 : RabbitierConsumer<Product>
    {
        public override void Consume(MessageReceived<Product> message)
        {
        }
    }
}
