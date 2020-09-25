using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Consumer.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Challenge.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //IConfiguration configuration = hostContext.Configuration;

                    //WorkerOptions options = configuration.GetSection("Kafka").Get<WorkerOptions>();

                    //services.AddSingleton(options);

                    services.AddHostedService<Worker>();
                });
    }
}
