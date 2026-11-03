using System;
using System.Collections.Generic;
using System.Text;

namespace Grafana_InfluxdbTest.Configuration
{
    public class InfluxdbOptions
    {
        public string Url { get; set; } =string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Bucket { get; set; } = string.Empty;
        public string Org { get; set; } = string.Empty;
    }
}
