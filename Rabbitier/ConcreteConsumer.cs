using Rabbitier.Configuration;
using Rabbitier.Consumer;
using RabbitMQ.Client.Events;

namespace SampleServer
{
    [ConsumerSettings("SampleQueue1")]
    public class ConcreteConsumer : RabbitierConsumer
    {
        public override void Consume(BasicDeliverEventArgs args)
        {
        }
    }
}
