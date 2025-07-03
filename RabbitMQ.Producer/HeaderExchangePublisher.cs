using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class HeaderExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers, durable: true, autoDelete: false, arguments: null);
            channel.QueueDeclare("demo-header-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var headers = new Dictionary<string, object>
            {
                { "x-match", "all" }, // Use "all" for AND logic, "any" for OR logic
                { "header1", "value1" },
                { "header2", "value2" }
            };
            channel.QueueBind("demo-header-queue", "demo-header-exchange", "", headers);
            var count = 0;
            while (true)
            {
                var message = new { name = "Test", Message = $"Hello Count: {count++} " };
                var body = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(message);
                channel.BasicPublish(exchange: "demo-header-exchange", routingKey: "", basicProperties: null, body: body);
                Thread.Sleep(1000); // Sleep for 1 second before sending the next message
            }
        }
    }
}
