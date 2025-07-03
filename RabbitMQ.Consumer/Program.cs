using RabbitMQ.Client;
using RabbitMQ.Client.Events;

static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World! Consumer");
        Console.WriteLine("Hello, World!");
        var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received message: {message}");
        };
        channel.BasicConsume(queue: "demo-queue", autoAck: true, consumer: consumer);
        Console.ReadLine();
    }
}