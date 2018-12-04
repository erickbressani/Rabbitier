using Rabbitier.Configuration;
using Rabbitier.Subscriber;
using Sample.Domain;

namespace SampleServer
{
    [SubscriberSettings("SampleQueue1", noAck: true)]
    public class ConcreteSubscriberWithoutAck : RabbitierSubscriber<Product>
    {
        public override void Consume(MessageReceived<Product> message)
        {
        }
    }
}
