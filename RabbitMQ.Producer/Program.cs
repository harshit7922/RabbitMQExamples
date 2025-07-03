// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Producer;

static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World! Producer");
        var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        //QueueProducer.Publish(channel);
        //Direct_ExchangePublisher.Publish(channel);
        //TopicExchangePublisher.Publish(channel);
        //HeaderExchangePublisher.Publish(channel);
        FanoutExchangeProducer.Publish(channel);
    }
}