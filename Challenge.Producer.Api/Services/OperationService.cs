using Challenge.Shared;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Challenge.Producer.Api.Services
{
    public class OperationService : IOperationService
    {
        private readonly KafkaConfig _config;

        public OperationService(IOptions<KafkaConfig> options)
        {
            _config = options.Value;
        }


        public async Task SendOperation(OperationCreatedEvent @event)
        {

            var config = new ProducerConfig { BootstrapServers = _config.BootstrapServers };

            //producer mais seguro
            config.Acks = _config.Acks;
            config.EnableIdempotence = _config.EnableIdempotence;

            //melhorar taxa de transferencia
            config.LingerMs = _config.LingerMs;
            config.BatchSize = _config.BatchSizeKB * 1024;

            using var producer = new ProducerBuilder<int, string>(config).Build();
            try
            {
                var value = JsonConvert.SerializeObject(@event);

                await producer.ProduceAsync(
                    _config.Topic,
                    new Message<int, string> { Key = new Random().Next(0, 2), Value = value });
            }
            catch (ProduceException<int, string> e)
            {
                Console.WriteLine($"Falha ao entregar a mensagem: {e.Message} [{e.Error.Code}]");
            }
        }
    }
}
