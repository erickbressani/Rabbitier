using Rabbitier.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rabbitier.Consumer
{
    public abstract class RabbitierConsumer : IRabbitierConsumer
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

        public void Stop()
            => _enabled = false;

        public void Start()
        {
            _enabled = true;
            Task.Run(() => StartDequeuing());
        }

        private void StartDequeuing()
        {
            while (_enabled)
            {
                var deliveryArgs = _subscription.Next();
                Consume(deliveryArgs);

                if (!_consumerSettings.NoAck)
                    _subscription.Ack(deliveryArgs);
            }
        }

        private ConsumerSettings GetConsumerSettings()
            => GetType().GetCustomAttributes(typeof(ConsumerSettings), true)
                        .FirstOrDefault() as ConsumerSettings;

        public abstract void Consume(BasicDeliverEventArgs args);
    }
}
