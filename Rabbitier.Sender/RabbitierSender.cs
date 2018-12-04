using Rabbitier.Configuration;
using Rabbitier.Configuration.Parsers;
using RabbitMQ.Client;
using System.Text;

namespace Rabbitier.Publisher
{
    public class RabbitierPublisher
    {
        private PublishData _publishData;

        private RabbitierPublisher()
        {
            _publishData = new PublishData();
        }

        public static RabbitierPublisher CreateWith()
            => new RabbitierPublisher();

        public RabbitierPublisher Exchange(string exchange)
        {
            _publishData.Exchange = exchange;
            return this;
        }

        public RabbitierPublisher RoutingKey(object routingKey)
            => RoutingKey(routingKey.ToString());

        public RabbitierPublisher RoutingKey(string routingKey)
        {
            _publishData.RoutingKey = routingKey;
            return this;
        }

        public RabbitierPublisher RoutingKey(params object[] routingKey)
        {
            _publishData.RoutingKey = string.Join('.', routingKey);
            return this;
        }

        public RabbitierPublisher IsMandatory()
        {
            _publishData.Mandatory = true;
            return this;
        }

        public RabbitierPublisher IsImmediate()
        {
            _publishData.Immediate = true;
            return this;
        }

        public RabbitierPublisher IsPersistent()
        {
            _publishData.IsPersistent = true;
            return this;
        }

        public Publisher Body(object body)
        {
            var parsedBody = Json.ParseToJson(body);
            return Body(parsedBody);
        }

        public Publisher Body(string body)
        {
            var parsedBody = Encoding.Default.GetBytes(body);
            return Body(parsedBody);
        }

        public Publisher Body(byte[] body)
        {
            _publishData.Body = body;
            return new Publisher(_publishData);
        }

        public class Publisher
        {
            private PublishData _publishData;
            private IModel _model;
            private IBasicProperties _properties;

            internal Publisher(PublishData publishData)
            {
                var rabbitierConnector = new RabbitierConnector();
                _model = rabbitierConnector.CreateModel();
                _publishData = publishData;
            }

            private void SetProperties()
            {
                _properties = _model.CreateBasicProperties();
                _properties.Persistent = _publishData.IsPersistent;
            }

            public void Publish()
                => _model.BasicPublish(_publishData.Exchange,
                                       _publishData.RoutingKey,
                                       _properties,
                                       _publishData.Body);
        }
    }
}
