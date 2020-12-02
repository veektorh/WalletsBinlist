using BinlistApi.Interfaces.Managers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinlistApi.Managers
{
    public class BinMessagePublisher : IBinMessagePublisher
    {
        private readonly IRabbitMqManager rabbitMqManager;
        private readonly ILogger<BinMessagePublisher> logger;

        public BinMessagePublisher(IRabbitMqManager rabbitMqManager, ILogger<BinMessagePublisher> logger)
        {
            this.rabbitMqManager = rabbitMqManager;
            this.logger = logger;
        }

        public void PublishBin(Bin bin)
        {

            if (bin  == null)
            {
                throw new Exception("Invalid bin object");
            }

            try
            {
                var channel = rabbitMqManager.Connect();

                channel.QueueDeclare("binlist", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var message = new { CardIin = bin.id, Scheme = bin.scheme, BankName = bin.bank.name };
                var messageString = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(messageString);

                channel.BasicPublish("", "binlist", null, body);

            }
            catch (Exception ex)
            {
                logger.LogError($"Unable to connect to rabbitmq ");
                logger.LogError(ex.Message);
            }
        }
    }
}
