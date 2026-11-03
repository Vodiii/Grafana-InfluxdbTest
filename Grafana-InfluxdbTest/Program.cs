using Grafana_InfluxdbTest.Configuration;
using Grafana_InfluxdbTest.Models;
using Grafana_InfluxdbTest.Service;
using InfluxDB.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Grafana_InfluxdbTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.Configure<InfluxdbOptions>(context.Configuration.GetSection("InfluxDB"));

                    services.AddSingleton<InfluxDBClient>(sp =>
                    {
                        var options = sp.GetRequiredService<IOptions<InfluxdbOptions>>().Value;
                        return new InfluxDBClient(options.Url, options.Token);
                    });

                    services.AddTransient<IInfluxService, InfluxService>();
                })
                .Build();

            var influxService = host.Services.GetRequiredService<IInfluxService>();

            Console.WriteLine("Приложение запущено. Отправка данных... (Нажмите Ctrl+C для выхода)");

            var random = new Random();
            while (true)
            {
                var newData = new SensorData
                {
                    SensorId = "sensor-01",
                    Temperature = random.Next(20, 30) + Math.Round(random.NextDouble(), 2),
                    Humidity = random.Next(40, 60) + Math.Round(random.NextDouble(), 2)
                };

                await influxService.SendDataAsync(newData);

                await Task.Delay(2000);
            }
        }
    }
}