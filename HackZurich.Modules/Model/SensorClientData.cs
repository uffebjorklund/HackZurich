namespace HackZurich.Modules.Model
{
    /// <summary>
    /// Combination of client info and sensor data
    /// Easier to store in AzureStorge if we have a flat structure.
    /// </summary>
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

        public SensorClientData(SensorInfo ci, SensorData sd)
        {
            this.SensorId = ci.SensorId.ToString();
            this.Lat = ci.Lat;
            this.Lng = ci.Lng;
            this.Name = ci.Name;
            this.Organization = ci.Organization;
            this.Humidity = sd.Humidity;
            this.Temperature = sd.Temperature;
        }
    }
}
