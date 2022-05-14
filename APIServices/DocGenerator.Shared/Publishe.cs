using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace DocGenerator.Shared
{
    public class Publishe<T>
    {
        /// <summary>
        /// Create Publishe
        /// </summary>
        public Publishe(T obj)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "pdfGenerato",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

               var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj)) ;

                channel.BasicPublish(exchange: "",
                                     routingKey: "pdfGenerato",
                                     basicProperties: null,
                                     body: body);
            }
        }

    }
}
