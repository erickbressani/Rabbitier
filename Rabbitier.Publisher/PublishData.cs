namespace Rabbitier.Publisher
{
    internal class PublishData
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public string ReplyTo { get; set; }
        public string CorrelationId { get; set; }
        public bool Mandatory { get; set; }
        public bool IsPersistent { get; set; }
        public byte[] Body { get; set; }

        public PublishData()
        {
            Exchange = string.Empty;
            RoutingKey = string.Empty;
            ReplyTo = string.Empty;
            CorrelationId = string.Empty;
        }
    }
}
