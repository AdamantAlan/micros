using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.AsyncDataService
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;

        public IModel _channel { get; }

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory() { HostName = $"{configuration["RabbitMQHost"]}",Port=int.Parse(configuration["RabbitMQPort"]) };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _connection.ConnectionShutdown += RabbitMq_Connection_Shutdown;
        }
        public void PublishNewPlatform(PlatformPublishedDto platform)
        {
            var message = JsonSerializer.Serialize(platform);
            if(_connection.IsOpen)
            {
                Console.WriteLine("Sending message");
                SendMessage(message);
                Console.WriteLine("Message sent");
            }
            else
            {
                Console.WriteLine("Connection close");
            }
        }
        public void Dispose()
        {
            if(_connection.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
        private void SendMessage(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "trigger", routingKey:"", basicProperties: null, body: bytes);
        }

        private void RabbitMq_Connection_Shutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMq shutdown");
        }
    }
}
