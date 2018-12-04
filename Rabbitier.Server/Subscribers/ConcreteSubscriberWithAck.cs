using Rabbitier.Configuration;
using Rabbitier.Subscriber;
using Sample.Domain;

namespace SampleServer
{
    [SubscriberSettings("SampleQueue1")]
    public class ConcreteSubscriberWithAck : RabbitierSubscriber<Product>
    {
        public override void Consume(MessageReceived<Product> message)
        {
        }
    }
}
