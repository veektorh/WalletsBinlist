using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Configuration;
using System.Text;

namespace RabbitMqConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = ConfigurationManager.AppSettings["hostname"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]),
                    UserName = ConfigurationManager.AppSettings["username"],
                    Password = ConfigurationManager.AppSettings["password"]
                };

                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                channel.QueueDeclare("binlist", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Message Received - {message}");
                };

                channel.BasicConsume("binlist", autoAck: true, consumer);

                Console.WriteLine("Consumer up and running");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured, Make sure rabbit mq is running");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

    }
}
