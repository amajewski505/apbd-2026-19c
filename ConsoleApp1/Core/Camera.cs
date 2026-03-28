namespace ConsoleApp1.Core;

public enum SensorType
{
    FullFrame,
    Apsc
}


public class Camera: Equipment
{
    public double Megapixels { get; set; }
    public SensorType SensorType { get; set; }
    
    public Camera(string name, string serialNumber, double megapixels, SensorType sensorType) : base(name, serialNumber)
    {
        Megapixels = megapixels;
        SensorType = sensorType;
    }
}