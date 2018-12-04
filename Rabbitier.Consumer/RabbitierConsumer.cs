using Rabbitier.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rabbitier.Consumer
{
    public abstract class RabbitierConsumer<TMessage> where TMessage : new()
    {
        private readonly Subscription _subscription;
        private readonly ConsumerSettings _consumerSettings;
        private bool _enabled;

        public RabbitierConsumer()
        {
            _consumerSettings = GetConsumerSettings();

            if (_consumerSettings == null)
                throw new ArgumentException($"{GetType()} needs to have the ConsumerSettings attribute.");

            var rabbitierConnectionFactory = new RabbitierConnector();
            _subscription = rabbitierConnectionFactory.CreateSubscription(_consumerSettings);
        }

        public void Start()
        {
            _enabled = true;
            Task.Run(() => StartDequeuing());
        }

        private void StartDequeuing()
        {
            while (_enabled)
            {
                BasicDeliverEventArgs args = _subscription.Next();
                Consume(args);

                if (!_consumerSettings.NoAck)
                    _subscription.Ack(args);
            }
        }

        private void Consume(BasicDeliverEventArgs args)
        {
            var messageReceived = new MessageReceived<TMessage>(args);
            Consume(messageReceived);
        }

        public void Stop()
            => _enabled = false;

        private ConsumerSettings GetConsumerSettings()
            => GetType().GetCustomAttributes(typeof(ConsumerSettings), true)
                        .FirstOrDefault() as ConsumerSettings;

        public abstract void Consume(MessageReceived<TMessage> message);
    }
}
