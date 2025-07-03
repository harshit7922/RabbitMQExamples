using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer;

static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World! Consumer");
        Console.WriteLine("Hello, World!");
        var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
       //QueueConsumer.Consumer(channel);
        DirectExchangeConsumer.Consumer(channel);
        Console.WriteLine("Press [enter] to exit.");
        Console.ReadLine();
    }
}