using RabbitMQ.Client;
using System;
using System.Text;

namespace NewTask
{
    public class NewTask
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            {
                channel.QueueDeclare(
                    queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                var message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "task_queue",
                    basicProperties: properties,
                    body: body
                    );

                Console.WriteLine($" [x] Sent {message}");
            }
            Console.WriteLine(" Press [enter] to exit");
        }
    }
}
