# Rabbitier
An easy to use abstraction for RabbitMQ Publishers and Subscribers for .Net Core

## RabbitierPublisher
Fluent RabbitMQ Publisher
  
### Usage

One way based publisher:
```csharp
RabbitierPublisher
	.CreateWith()
  	.RoutingKey("QueueName")
  	.Body(product) //Accept string, byte[], any object (this early version is only converting object to Json)
  	.Publish();
```

Topic based publisher:
```csharp
RabbitierPublisher
	.CreateWith()
	.Exchange("ExchangeName")
	.RoutingKey("Topic1", "Topic2", "Topic3") //Accept any object, concatenates with "."
	.Body(product) //Accept string, byte[], any object (this early version is only converting object to Json)
	.Publish();
```

Header based publisher:
```csharp
RabbitierPublisher
	.CreateWith()
	.Exchange("ExchangeName")
	.Body(product) //Accept string, byte[], any object (this early version is only converting object to Json)
	.AddHeader("Key1", "Value1")
	.AddHeader("Key2", "Value2")
	.AddHeader("Key3", "Value3")
	.Publish();
```

Additional settings: 
```csharp
 RabbitierPublisher
	.CreateWith()
	...
	.IsMandatory() //default is false
	.IsPersistent() //default is false
	.ReplyTo("ResponseQueueName")
	.CorrelationId("CorrelationId")
	...
	.Publish();
```

## RabbitierSubscriber
Subscriber Abstraction for RabbitMQ
  
### Usage

In order to create any subscriber your subscriber class just have to inherite from RabbitierSubscriber<TMessage> and add the custom attribute [SubscriberSettings(string queueName)]
  
```csharp
> RabbitierSubscriber<TMessage>: TMessage is the type you want to convert the incoming message (this early version is only converting object from Json)
	
> [SubscriberSettings(...)]: Contains all the default parameters from RabbitMQ,
                              string queueName, 
                              bool noAck = false, 
                              uint prefetchSize = 0, 
                              ushort prefetchCount = 1, 
                              bool global = false
```

Subscriber classes samples:
```csharp
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
```csharp
var subscriber = new ConcreteSubscriberWithoutAck();
subscriber.Start();
```

Stoping the Subscriber
```csharp
...
subscriber.Stop();
```

Method: Consume(MessageReceived<TMessage> message)
 - This method is inherited from RabbitierSubscriber;
 - If the subscriber is started and running it is called every time a message is send to the bound queue;
  
Parameter: MessageReceived<TMessage>
 - Wraps the information send by RabbitMQ
 - Properties:  
	- ParsedBody: TMessage 
	- Body: byte[] 
	- Exchange: string 
	- ConsumerTag: string 
	- RoutingKey: string 
	- ReplyTo: string 
	- DeliveryTag: ulong 
	- Redelivered: bool 
  
## Configuration
In order to work, your client needs to have an appsettings.json with the following settings:
```csharp
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
  
