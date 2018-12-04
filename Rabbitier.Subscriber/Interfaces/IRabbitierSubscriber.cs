namespace Rabbitier.Subscriber
{
    public interface IRabbitierSubscriber<TMessage> where TMessage : new()
    {
        void Start();
        void Stop();
        void Consume(MessageReceived<TMessage> message);
    }
}
