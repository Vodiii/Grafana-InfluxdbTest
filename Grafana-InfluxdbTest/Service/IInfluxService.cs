using Grafana_InfluxdbTest.Models;
using System.Threading.Tasks;

namespace Grafana_InfluxdbTest.Service
{
    public interface IInfluxService
    {
        Task SendDataAsync(SensorData data);
    }
}

