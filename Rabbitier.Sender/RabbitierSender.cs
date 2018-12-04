using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace Rabbitier.Publisher
{
    public class RabbitierPublisher
    {
        private string _exchange;
        private string _routingKey;
        private bool _mandatory;
        private bool _immediate;
        private byte[] _body;
        private IBasicProperties _basicProperties;
        private IModel _model;

        private RabbitierPublisher()
        {

        }

        public static RabbitierPublisher CreateWith()
            => new RabbitierPublisher();

        public RabbitierPublisher Exchange(string exchange)
        {
            _exchange = exchange;
            return this;
        }

        public RabbitierPublisher RoutingKey(object routingKey)
            => RoutingKey(routingKey.ToString());

        public RabbitierPublisher RoutingKey(string routingKey)
        {
            _routingKey = routingKey;
            return this;
        }

        public RabbitierPublisher RoutingKey(params object[] routingKey)
        {
            _routingKey = string.Join('.', routingKey);
            return this;
        }

        public RabbitierPublisher IsMandatory()
        {
            _mandatory = true;
            return this;
        }

        public RabbitierPublisher IsImmediate()
        {
            _immediate = true;
            return this;
        }

        public RabbitierPublisher BasicProperties(IBasicProperties basicProperties)
        {
            _basicProperties = basicProperties;
            return this;
        }

        public RabbitierPublisher Body(string body)
        {
            _body = Encoding.Default.GetBytes(body);
            return this;
        }

        public RabbitierPublisher Body(byte[] body)
        {
            _body = body;
            return this;
        }

        public void Publish()
        {
            _model.BasicPublish(_exchange, _routingKey, _basicProperties, _body);
        }
    }
}
