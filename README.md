# Rabbitier
An easy to use abstraction for RabbitMQ Publishers and Subscribers for .Net Core

## RabbitierPublisher
Fluent RabbitMQ Publisher
  
### Usage

One way publisher, send the message direct to a queue:
```
RabbitierPublisher.CreateWith()
                  .RoutingKey("QueueName")
                  .Body(product) //Accept string, byte[], any object (this early version is only converting object to Json)
                  .Publish();
```

Topic publisher, send the message based on a topic route:
```
RabbitierPublisher.CreateWith()
                  .Exchange("ExchangeName")
                  .RoutingKey("Topic1", "Topic2", "Topic3") //Accept any object, concatenates with "."
                  .Body(product) //Accept string, byte[], any object (this early version is only converting object to Json)
                  .Publish();
```

Additional settings: 
```
 RabbitierPublisher.CreateWith()
                   .Exchange("ExchangeName")
                   .RoutingKey("RoutingKeyName")
                   .IsMandatory() //default is false
                   .IsPersistent() //default is false
                   .IsImmediate() //default is false
                   .Body(product)
                   .Publish();
```

## RabbitierSubscriber
Subscriber Abstraction for RabbitMQ
  
### Usage

In order to create any subscriber your subscriber class just have to inherite from RabbitierSubscriber<TMessage> and add the custom attribute [SubscriberSettings(string queueName)]
  
```
> RabbitierSubscriber<TMessage>: TMessage is the type you want to convert the incoming message (this early version is only converting object from Json)
	
> [SubscriberSettings(...)]: Contains all the default parameters from RabbitMQ,
                              string queueName, 
                              bool noAck = false, 
                              uint prefetchSize = 0, 
                              ushort prefetchCount = 1, 
                              bool global = false
```

Subscriber classes samples:
```
[SubscriberSettings("SampleQueue1")]
public class ConcreteSubscriberWithAck : RabbitierSubscriber<Product>
{
    public override void Consume(MessageReceived<Product> message) 
    {
    	...
    }
}

[SubscriberSettings("SampleQueue1", noAck: true)]
public class ConcreteSubscriberWithoutAck : RabbitierSubscriber<Product>
{
    public override void Consume(MessageReceived<Product> message)
    {
    	...
    }
}
```

Starting the Subscriber
```
var subscriber = new ConcreteSubscriberWithoutAck();
subscriber.Start();
```

Stoping the Subscriber
```
...
subscriber.Stop();
```

Method: Consume(MessageReceived<TMessage> message)
 - This method is inherited from RabbitierSubscriber;
 - It is called every time a message is sent to the bound queue, only if the subscriber is started;
  
Parameter: MessageReceived<TMessage>
 - Wraps the information send by RabbitMQ
 - Properties:  
	--TMessage JsonParsedBody 
        --byte[] Body
        --string Exchange
        --string ConsumerTag
        --string RoutingKey
        --ulong DeliveryTag
	--bool Redelivered
  
## Configuration
In order to work, your client needs to have an appsettings.json with the following settings:
```
...
"Rabbitier": {
    "HostName": "localhost", //default localhost
    "UserName": "guest", //default guest
    "Password": "guest", //default guest
	  "VirtualHost": "VirtualHost", //Optional
    "Port": "1234" //Optional, RabbitMQ will search for the default port if it is null
  }
...
```
  
