using BinlistApi.Interfaces.Managers;
using BinlistApi.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistApi.Managers
{
    public class RabbitMqManager : IRabbitMqManager
    {
        private readonly RabbitMqConfig rabbitMqConfig;

        public RabbitMqManager(IOptions<RabbitMqConfig> rabbitMqConfig)
        {
            this.rabbitMqConfig = rabbitMqConfig.Value;
        }

        public IModel Connect()
        {
            var factory = new ConnectionFactory
            {
                HostName = rabbitMqConfig.Hostname,
                Port = rabbitMqConfig.Port,
                UserName = rabbitMqConfig.Username,
                Password = rabbitMqConfig.Password
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            return channel;
        }
    }
}
