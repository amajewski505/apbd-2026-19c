namespace ConsoleApp1.Core;

public class Laptop: Equipment
{
    public int RAMSizeGB { get; set; }

    // Screen size in inches
    public double ScreenSizeInch { get; set; }
    
    public Laptop(string name, string serialNumber, int ramSizeGb, double screenSizeInch): base(name, serialNumber)
    {
        RAMSizeGB = ramSizeGb;
        ScreenSizeInch = screenSizeInch;
    }
    
}