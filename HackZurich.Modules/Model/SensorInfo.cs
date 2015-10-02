using System;

namespace HackZurich.Modules.Model
{
    /// <summary>
    /// Info that you only have to set once on the controller.
    /// Then we use state (and even persist) so that it does not have to be set again at all
    /// </summary>
    public class SensorInfo
    {
        public Guid SensorId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
    }
}
