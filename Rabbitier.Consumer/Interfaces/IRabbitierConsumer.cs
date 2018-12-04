using RabbitMQ.Client.Events;

namespace Rabbitier.Consumer
{
    public interface IRabbitierConsumer
    {
        void Stop();
        void Start();
        void Consume(BasicDeliverEventArgs args);
    }
}
