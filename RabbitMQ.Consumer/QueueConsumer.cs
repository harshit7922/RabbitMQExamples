using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class QueueConsumer
    {
        public static void Consumer(IModel channel)
        {
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
}
