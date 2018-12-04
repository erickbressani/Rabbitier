using RabbitMQ.Client;

namespace Rabbitier.Publisher
{
    internal class PublishData
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public bool Mandatory { get; set; }
        public bool Immediate { get; set; }
        public byte[] Body { get; set; }
        public bool IsPersistent { get; set; }

        public PublishData()
        {
            Exchange = string.Empty;
            RoutingKey = string.Empty;
        }
    }
}
