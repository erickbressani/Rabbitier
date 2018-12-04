using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace Rabbitier.Configuration
{
    public class RabbitierConnector
    {
        private RabbitierConfiguration _rabbitierConfiguration;

        public RabbitierConnector()
        {
            _rabbitierConfiguration = new RabbitierConfiguration();
        }

        public Subscription CreateSubscription(ConsumerSettings consumerSettings)
        {
            IModel model = CreateModel(consumerSettings);
            return new Subscription(model, consumerSettings.QueueName, consumerSettings.NoAck);
        }

        private IModel CreateModel(ConsumerSettings consumerSettings)
        {
            var connection = CreateConnection();
            IModel model = connection.CreateModel();
            model.BasicQos(consumerSettings.PrefetchSize, consumerSettings.PrefetchCount, consumerSettings.Global);
            return model;
        }

        private IConnection CreateConnection()
        {
            var connectionFactory = CreateConnectionFactory();
            return connectionFactory.CreateConnection();
        }

        private ConnectionFactory CreateConnectionFactory()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitierConfiguration.HostName,
                UserName = _rabbitierConfiguration.UserName,
                Password = _rabbitierConfiguration.Password
            };

            if (!string.IsNullOrEmpty(_rabbitierConfiguration.VirtualHost))
                connectionFactory.VirtualHost = _rabbitierConfiguration.VirtualHost;

            if (_rabbitierConfiguration.Port > 0)
                connectionFactory.Port = _rabbitierConfiguration.Port;

            return connectionFactory;
        }
    }
}
