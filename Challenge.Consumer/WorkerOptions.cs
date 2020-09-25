using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Consumer
{
    public class WorkerOptions
    {
        public string BootstrapServers { get; set; }
        public string Acks { get; set; }
        public string EnableIdempotence { get; set; }
        public string LingerMs { get; set; }
        public string BatchSizeKB { get; set; }
        public string Topic { get; set; }
    }
}
