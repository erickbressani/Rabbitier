using Rabbitier.Publisher;
using Sample.Domain;

namespace SampleClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            var product = new Product()
            {
                Id = 1,
                Description = "TestFooBar"
            };

            RabbitierPublisher.CreateWith()
                              .RoutingKey("SampleQueue1")
                              .Body(product)
                              .Publish();
        }
    }
}
