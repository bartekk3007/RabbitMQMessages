using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wysylanie");
            Console.WriteLine("Nacisnij");
            Console.ReadKey();

            string queueName = "queue";
            var factory = new ConnectionFactory();
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.HostName = "localhost";
            factory.VirtualHost = "/";

            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            for (int i = 1; i <= 10; i++)
            {
                string message = $"Wiadomosc numer {i}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"Wyslano: '{message}'");
            }

            Console.WriteLine("Nacisnij przycisk");
            Console.ReadKey();

            channel.Close();
            conn.Close();
        }
    }
}
