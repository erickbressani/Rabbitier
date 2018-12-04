namespace Rabbitier.Consumer
{
    public interface IRabbitierConsumer<TMessage> where TMessage : new()
    {
        void Start();
        void Stop();
        void Consume(MessageReceived<TMessage> message);
    }
}
