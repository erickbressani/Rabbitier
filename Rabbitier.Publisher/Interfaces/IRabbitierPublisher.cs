using System.Collections.Generic;

namespace Rabbitier.Publisher
{
    public interface IRabbitierPublisher
    {
        IRabbitierPublisher Exchange(string exchange);
        IRabbitierPublisher RoutingKey(object routingKey);
        IRabbitierPublisher RoutingKey(string routingKey);
        IRabbitierPublisher RoutingKey(params object[] routingKey);
        IRabbitierPublisher IsMandatory();
        IRabbitierPublisher IsPersistent();
        ISender Body(object body);
        ISender Body(string body);
        ISender Body(byte[] body);
    }
}
