using System;
using System.Collections.Generic;
using System.Text;

namespace Grafana_InfluxdbTest.Models
{
    public class SensorData
    {
        public string SensorId { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
