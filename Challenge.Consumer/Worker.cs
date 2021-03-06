using KafkaNet;
using KafkaNet.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Consumer.Kafka
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private KafkaOptions _kafkaOptions;
        private BrokerRouter _brokerRouter;
        private KafkaNet.Consumer _consumer;


        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {

            var workerOptions = new WorkerOptions();
            configuration.Bind("Kafka", workerOptions);

            _logger = logger;
            _kafkaOptions = new KafkaOptions(new Uri(workerOptions.BootstrapServers));
            _brokerRouter = new BrokerRouter(_kafkaOptions);
            _consumer = new KafkaNet.Consumer(new ConsumerOptions(workerOptions.Topic, _brokerRouter));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);


                stoppingToken.Register(() => _logger.LogDebug($"{DateTime.Now} | Servi�o parado..."));

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogDebug($"{DateTime.Now} | Servi�o em execu��o... ");
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
            {
                using (HttpClient client = new HttpClient())
                {
                    await client.PostAsync("https://localhost:44332/api/operations", ConvertObjectToByteArrayContent(Encoding.UTF8.GetString(msg.Value)));
                }
            }

        }
    }
}
