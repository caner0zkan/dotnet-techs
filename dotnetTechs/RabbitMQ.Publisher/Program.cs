
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new("amqps://jpdcfjkp:IW7jASj-pN11AA8KrZQ3yldqdWBYBt_o@sparrow.rmq.cloudamqp.com/jpdcfjkp");

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "example-queue", exclusive: false);

var message = Encoding.UTF8.GetBytes("Merhaba");
channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

Console.Read();