using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class DirectExchangeConsumer
    {
        public static void Consumer(IModel channel)
        {
            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            channel.QueueDeclare("demo-direct-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("demo-direct-queue", "demo-direct-exchange", "demo-routing-key");
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message from direct exchange: {message}");
            };
            channel.BasicConsume(queue: "demo-direct-queue", autoAck: true, consumer: consumer);
            Console.WriteLine("Listening for messages on direct exchange. Press [enter] to exit.");
            Console.ReadLine();
            
        }
    }
}
