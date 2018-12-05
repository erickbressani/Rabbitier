using Rabbitier.Subscriber.Parsers;
using RabbitMQ.Client.Events;

namespace Rabbitier.Subscriber
{
    public class MessageReceived<TMessage> where TMessage : new()
    {
        public TMessage ParsedBody { get; }
        public byte[] Body { get; }
        public string Exchange { get; }
        public string ConsumerTag { get; }
        public string RoutingKey { get; }
        public ulong DeliveryTag { get; }
        public bool Redelivered { get; }

        internal MessageReceived(BasicDeliverEventArgs args)
        {
            Body = args.Body;
            ParsedBody = Json.Parse<TMessage>(Body);
            Exchange = args.Exchange;
            ConsumerTag = args.ConsumerTag;
            RoutingKey = args.RoutingKey;
            DeliveryTag = args.DeliveryTag;
            Redelivered = args.Redelivered;
        }
    }
}
