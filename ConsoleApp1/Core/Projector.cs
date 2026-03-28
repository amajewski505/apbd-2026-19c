namespace ConsoleApp1.Core;

public class Projector: Equipment
{
    public int BrightnessLumens { get; set; }
    public int LampHoursUsed { get; set; }
    

    public Projector(string name, string serialNumber, int brightnessLumens, int lampHoursUsed) : base(name, serialNumber)
    {
        BrightnessLumens = brightnessLumens;
        LampHoursUsed = lampHoursUsed;
    }
}