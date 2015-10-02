using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpClient
{
    public class SensorClientData
    {
        public string SensorId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public double Humidity { get; set; }
        public double Temperature { get; set; }
        public SensorClientData()
        {

        }        
    }

    public class SensorInfo
    {
        public Guid SensorId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
    }

    public class SensorData
    {
        public double Humidity { get; set; }
        public double Temperature { get; set; }
    }
}
