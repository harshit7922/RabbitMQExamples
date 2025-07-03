using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class HeaderExchangeCnsumer
    {
        public static void Consumer(IModel channel)
        {
            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers, durable: true, autoDelete: false, arguments: null);
            channel.QueueDeclare("demo-header-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var headers = new Dictionary<string, object>
            {
                { "x-match", "all" }, // or "any" for any match
                { "header1", "value1" },
                { "header2", "value2" }
            };
            channel.QueueBind("demo-header-queue", "demo-header-exchange", string.Empty, headers);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message from header exchange: {message}");
            };
            
            channel.BasicConsume(queue: "demo-header-queue", autoAck: true, consumer: consumer);
            Console.WriteLine("Listening for messages on header exchange. Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
