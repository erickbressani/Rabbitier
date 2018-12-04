using SampleServer.Entities;
using System;
using System.Threading;

namespace SampleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumer = new ConcreteConsumer();
            consumer.Start();
        }
    }
}
