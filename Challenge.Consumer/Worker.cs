using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Shared;
using Challenge.Shared.Enum;
using KafkaNet;
using KafkaNet.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Challenge.Consumer.Kafka
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private KafkaOptions _kafkaOptions;
        private BrokerRouter _brokerRouter;
        private KafkaNet.Consumer _consumer;


        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _kafkaOptions = new KafkaOptions(new Uri("http://omv.serveblog.net:29092"));
            _brokerRouter = new BrokerRouter(_kafkaOptions);
            _consumer = new KafkaNet.Consumer(new ConsumerOptions("operation-created-event", _brokerRouter));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);


                stoppingToken.Register(() => _logger.LogDebug($"{DateTime.Now} | Serviço parado..."));

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogDebug($"{DateTime.Now} | Serviço em execução... ");
                        await ObterAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogDebug($"{DateTime.Now} | Exception: {ex.Message}");
                    }

                }
            }
        }

        public ByteArrayContent ConvertObjectToByteArrayContent(string valor)
        {
            ByteArrayContent byteContent = new ByteArrayContent((Encoding.UTF8.GetBytes(valor)));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        private async Task ObterAsync()
        {
            foreach (var msg in _consumer.Consume())
                using (HttpClient client = new HttpClient())
                    await client.PostAsync("http://localhost:50648/api/operations", ConvertObjectToByteArrayContent(Encoding.UTF8.GetString(msg.Value)));
        }
    }
}
