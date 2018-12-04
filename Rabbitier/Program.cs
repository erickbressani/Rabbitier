using System;
using System.Threading;

namespace SampleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var consumer = new ConcreteConsumer();
            consumer.Start();
        }
    }
}
