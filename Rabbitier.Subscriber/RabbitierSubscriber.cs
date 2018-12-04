using Rabbitier.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rabbitier.Subscriber
{
    public abstract class RabbitierSubscriber<TMessage> where TMessage : new()
    {
        private readonly Subscription _subscription;
        private readonly SubscriberSettings _subscriberSettings;
        private bool _enabled;

        public RabbitierSubscriber()
        {
            _subscriberSettings = GetSubscriberSettings();

            if (_subscriberSettings == null)
                throw new ArgumentException($"{GetType()} needs to have the ConsumerSettings attribute.");

            var rabbitierConnectionFactory = new RabbitierConnector();
            _subscription = rabbitierConnectionFactory.CreateSubscription(_subscriberSettings);
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
                AcknowledgeIfNecessary(args);
            }
        }

        private void Consume(BasicDeliverEventArgs args)
        {
            var messageReceived = new MessageReceived<TMessage>(args);
            Consume(messageReceived);
        }

        private void AcknowledgeIfNecessary(BasicDeliverEventArgs args)
        {
            if (!_subscriberSettings.NoAck)
                _subscription.Ack(args);
        }

        public void Stop()
            => _enabled = false;

        private SubscriberSettings GetSubscriberSettings()
            => GetType().GetCustomAttributes(typeof(SubscriberSettings), true)
                        .FirstOrDefault() as SubscriberSettings;

        public abstract void Consume(MessageReceived<TMessage> message);
    }
}
