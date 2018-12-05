using Rabbitier.Configuration;
using Rabbitier.Subscriber;
using Sample.Domain;
using System;

namespace SampleServer
{
    [SubscriberSettings("SampleQueue1", noAck: true)]
    public class ConcreteSubscriberWithoutAck : RabbitierSubscriber<Product>
    {
        public override void Consume(MessageReceived<Product> message)
        {
            Console.WriteLine();
            Console.WriteLine("Message Received");
            Console.WriteLine($"Id: {message.ParsedBody.Id}");
            Console.WriteLine($"Description: {message.ParsedBody.Description}");
            Console.WriteLine($"Redelivered: {message.Redelivered}");
            Console.WriteLine($"RoutingKey: {message.RoutingKey}");
            Console.WriteLine($"Exchange: {message.Exchange}");
            Console.WriteLine($"DeliveryTag: {message.DeliveryTag}");
            Console.WriteLine($"ConsumerTag: {message.ConsumerTag}");
        }
    }
}
