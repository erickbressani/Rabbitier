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
        private Subscription _subscription;
        private SubscriberSettings _subscriberSettings;
        private bool _enabled;

        public RabbitierSubscriber()
        {
            SetupSubscriberSettings();
            CreateSubscription();
        }

        private void SetupSubscriberSettings()
        {
            _subscriberSettings = GetSubscriberSettings();

            if (_subscriberSettings == null)
                throw new ArgumentException($"{GetType()} needs to have the ConsumerSettings attribute.");
        }

        private SubscriberSettings GetSubscriberSettings()
            => GetType().GetCustomAttributes(typeof(SubscriberSettings), true)
                        .FirstOrDefault() as SubscriberSettings;

        private void CreateSubscription()
        {
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
                var messageReceived = new MessageReceived<TMessage>(args);
                Consume(messageReceived);
                AcknowledgeIfNecessary(args);
            }
        }

        private void AcknowledgeIfNecessary(BasicDeliverEventArgs args)
        {
            if (!_subscriberSettings.NoAck)
                _subscription.Ack(args);
        }

        public void Stop()
            => _enabled = false;

        public abstract void Consume(MessageReceived<TMessage> message);
    }
}
