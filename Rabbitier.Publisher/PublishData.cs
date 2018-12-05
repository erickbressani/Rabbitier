using RabbitMQ.Client;
using System.Collections.Generic;

namespace Rabbitier.Publisher
{
    internal class PublishData
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public bool Mandatory { get; set; }
        public byte[] Body { get; set; }
        public bool IsPersistent { get; set; }
        public List<KeyValuePair<string, object>> Headers { get; }

        public PublishData()
        {
            Exchange = string.Empty;
            RoutingKey = string.Empty;
            Headers = new List<KeyValuePair<string, object>>();
        }
    }
}
