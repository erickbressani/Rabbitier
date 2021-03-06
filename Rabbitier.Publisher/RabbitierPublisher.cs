﻿using Rabbitier.Configuration;
using Rabbitier.Publisher.Parsers;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;

namespace Rabbitier.Publisher
{
    public class RabbitierPublisher : IRabbitierPublisher
    {
        private PublishData _publishData;

        private RabbitierPublisher()
        {
            _publishData = new PublishData();
        }

        public static IRabbitierPublisher CreateWith()
            => new RabbitierPublisher();

        public IRabbitierPublisher Exchange(string exchange)
        {
            _publishData.Exchange = exchange;
            return this;
        }

        public IRabbitierPublisher RoutingKey(object routingKey)
            => RoutingKey(routingKey.ToString());

        public IRabbitierPublisher RoutingKey(string routingKey)
        {
            _publishData.RoutingKey = routingKey;
            return this;
        }

        public IRabbitierPublisher RoutingKey(params object[] routingKey)
        {
            _publishData.RoutingKey = string.Join('.', routingKey);
            return this;
        }

        public IRabbitierPublisher ReplyTo(string replyTo)
        {
            _publishData.ReplyTo = replyTo;
            return this;
        }

        public IRabbitierPublisher CorrelationId(object correlationId)
            => CorrelationId(correlationId.ToString());

        public IRabbitierPublisher CorrelationId(string correlationId)
        {
            _publishData.CorrelationId = correlationId;
            return this;
        }

        public IRabbitierPublisher IsMandatory()
        {
            _publishData.Mandatory = true;
            return this;
        }

        public IRabbitierPublisher IsPersistent()
        {
            _publishData.IsPersistent = true;
            return this;
        }

        public ISender Body(object body)
        {
            var parsedBody = Json.Parse(body);
            return Body(parsedBody);
        }

        public ISender Body(string body)
        {
            var parsedBody = Encoding.Default.GetBytes(body);
            return Body(parsedBody);
        }

        public ISender Body(byte[] body)
        {
            _publishData.Body = body;
            return new Sender(_publishData);
        }

        public class Sender : ISender
        {
            private PublishData _publishData;
            private IModel _model;
            private IBasicProperties _properties;

            internal Sender(PublishData publishData)
            {
                var rabbitierConnector = new RabbitierConnector();
                _model = rabbitierConnector.CreateModel();
                _publishData = publishData;
                SetProperties();
            }

            private void SetProperties()
            {
                _properties = _model.CreateBasicProperties();
                _properties.Persistent = _publishData.IsPersistent;
                _properties.ReplyTo = _publishData.ReplyTo;
                _properties.CorrelationId = _publishData.CorrelationId;
                _properties.Headers = new Dictionary<string, object>();
            }

            public ISender AddHeader(KeyValuePair<string, object> header)
            {
                _properties.Headers.Add(header);
                return this;
            }

            public ISender AddHeader(string key, object value)
            {
                var header = new KeyValuePair<string, object>(key, value);
                return AddHeader(header);
            }

            public void Publish()
            {
                _model.BasicPublish(_publishData.Exchange,
                                    _publishData.RoutingKey,
                                    basicProperties: _properties,
                                    body: _publishData.Body,
                                    mandatory: _publishData.Mandatory);
            }
        }
    }
}
