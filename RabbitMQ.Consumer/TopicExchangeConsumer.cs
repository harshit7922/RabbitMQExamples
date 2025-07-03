using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class TopicExchangeConsumer
    {
        public static void Consumer(IModel channel)
        {
            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic, durable: true, autoDelete: false, arguments: null);
            channel.QueueDeclare("demo-topic-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("demo-topic-queue", "demo-topic-exchange", "demo.topic.#");
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message from topic exchange: {message}");
            };
            channel.BasicConsume(queue: "demo-topic-queue", autoAck: true, consumer: consumer);
            Console.WriteLine("Listening for messages on topic exchange. Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
