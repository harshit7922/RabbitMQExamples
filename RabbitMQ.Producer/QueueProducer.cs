using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var count = 0;
            while(true)
            { 
                var message = new { name = "Test", Message=$"Hello Count: {count++} " };
                var body = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(message);
                channel.BasicPublish(exchange: "", routingKey: "demo-queue", basicProperties: null, body: body);
                Thread.Sleep(1000); // Sleep for 1 second before sending the next message
            }
        }
    }
}
