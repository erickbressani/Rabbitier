using Rabbitier.Publisher;
using Sample.Domain;
using System;

namespace SampleClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Make sure to have a Queue named: \"SampleQueue1\" on RabbitMQ");

            while (true)
            {
                var product = new Product()
                {
                    Id = new Random().Next(),
                    Description = "TestFooBar"
                };

                Console.WriteLine("Press any key to send message");
                Console.ReadKey();

                RabbitierPublisher.CreateWith()
                                  .RoutingKey("SampleQueue1")
                                  .Body(product)
                                  .Publish();

                Console.WriteLine();
                Console.WriteLine($"Message sent: ProductId: {product.Id}");
                Console.WriteLine();
            }
        }
    }
}
