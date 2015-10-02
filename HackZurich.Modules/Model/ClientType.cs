namespace HackZurich.Modules.Model
{
    /// <summary>
    /// To be able to target clients based on type.
    /// For example sensordata is only sent to monitoring clients
    /// </summary>
    public enum ClientType
    {
        Monitor,
        Kinect    
    }
}
