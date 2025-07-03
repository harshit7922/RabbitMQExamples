// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;

static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World! Producer");
        var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        var message =  new {name = "Test", value = 123};
        var body = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(message);
        channel.BasicPublish(exchange: "", routingKey: "demo-queue", null, body: body);
    }
}