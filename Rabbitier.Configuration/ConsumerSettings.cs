using System;

namespace Rabbitier.Configuration
{
    public class ConsumerSettings : Attribute
    {
        public string QueueName { get; }
        public bool NoAck { get; }
        public uint PrefetchSize { get; }
        public ushort PrefetchCount { get; }
        public bool Global { get; }

        public ConsumerSettings(string queueName, bool noAck = false, uint prefetchSize = 0, ushort prefetchCount = 1, bool global = false)
        {
            QueueName = queueName;
            NoAck = noAck;
            PrefetchSize = prefetchSize;
            PrefetchCount = prefetchCount;
            Global = global;
        }
    }
}
