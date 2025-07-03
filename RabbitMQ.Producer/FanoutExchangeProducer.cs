using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class FanoutExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            // Declare a fanout exchange
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);
            
            // Declare a queue and bind it to the fanout exchange
            channel.QueueDeclare("demo-fanout-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("demo-fanout-queue", "demo-fanout-exchange", "", null);
            var count = 0;
            while (true)
            {
                var message = new { name = "Test", Message = $"Hello Count: {count++} " };
                var body = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(message);
                
                // Publish the message to the fanout exchange
                channel.BasicPublish(exchange: "demo-fanout-exchange", routingKey: "", basicProperties: null, body: body);
                
                Thread.Sleep(1000); // Sleep for 1 second before sending the next message
            }
        }   
    }
}
