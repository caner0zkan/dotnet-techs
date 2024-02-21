using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new("amqps://jpdcfjkp:IW7jASj-pN11AA8KrZQ3yldqdWBYBt_o@sparrow.rmq.cloudamqp.com/jpdcfjkp");

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "example-queue", exclusive: false);

var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);
consumer.Received += (sender, e) => {
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();