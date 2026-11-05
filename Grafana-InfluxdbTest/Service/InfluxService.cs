using Grafana_InfluxdbTest.Configuration;
using Grafana_InfluxdbTest.Models;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Grafana_InfluxdbTest.Service
{
    public class InfluxService : IInfluxService
    {
        private readonly InfluxDBClient _client;
        private readonly InfluxdbOptions _options;

        public InfluxService(InfluxDBClient client, IOptions<InfluxdbOptions> options)
        {
            _client = client;
            _options = options.Value;
        }

        public async Task SendDataAsync(SensorData data)
        {
            try
            {


                var point = PointData
                    .Measurement("sensor_data")
                    .Tag("sensor_id", data.SensorId)
                    .Field("temperature", data.Temperature)
                    .Field("humidity", data.Humidity);

                var writeApi = _client.GetWriteApiAsync();

                await writeApi.WritePointAsync(point, _options.Bucket, _options.Org);

                Console.WriteLine($"Отправлено: {data.SensorId} | T: {data.Temperature:F1} | H: {data.Humidity:F1}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ОШИБКА ОТПРАВКИ", ex.Message);    
            }
        }
    }
}
