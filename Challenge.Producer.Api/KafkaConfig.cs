using Confluent.Kafka;

namespace Challenge.Producer.Api
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; }
        public Acks Acks { get; set; }
        public bool EnableIdempotence { get; set; }
        public int LingerMs { get; set; }
        public int BatchSizeKB { get; set; }
        public string Topic { get; set; }

    }
}
