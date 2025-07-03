using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {
        public static void Consumer(IModel channel)
        {
            // Declare a fanout exchange
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);
            
            // Declare a queue
            channel.QueueDeclare("demo-fanout-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            
            // Bind the queue to the fanout exchange
            channel.QueueBind("demo-fanout-queue", "demo-fanout-exchange", string.Empty);
            
            // Create a consumer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message from fanout exchange: {message}");
            };
            
            // Start consuming messages
            channel.BasicConsume(queue: "demo-fanout-queue", autoAck: true, consumer: consumer);
            Console.WriteLine("Listening for messages on fanout exchange. Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
