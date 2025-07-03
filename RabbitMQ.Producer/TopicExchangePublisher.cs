using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class TopicExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic, durable: true, autoDelete: false, arguments: null);
            channel.QueueDeclare("demo-topic-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("demo-topic-queue", "demo-topic-exchange", "demo.topic.#", null);
            var count = 0;
            while (true)
            {
                var message = new { name = "Test", Message = $"Hello Count: {count++} " };
                var body = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(message);
                channel.BasicPublish(exchange: "demo-topic-exchange", routingKey: "demo.topic.test", basicProperties: null, body: body);
                Thread.Sleep(1000); // Sleep for 1 second before sending the next message
            }
        }
    }
}
