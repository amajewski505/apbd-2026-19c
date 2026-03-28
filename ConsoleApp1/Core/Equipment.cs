namespace ConsoleApp1.Core;

public abstract class Equipment
{
    private static int _id { get; set; }

    public int Id { get; private set; }

    private string _name;

    public string Name
    {
        get => _name;
        protected set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name cannot be empty");
            }
            _name = value;
        }
    }

    public bool IsAvailable { get; set; }

    
    private string _serialNumber;
    public string SerialNumber
    {
        get =>  _serialNumber;
        protected set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Serial Number can't be empty");
            }
            _serialNumber = value;
        }
    }


    protected Equipment(string  name, string serialNumber)
    {
        Id = ++_id;
        Name = name;
        IsAvailable = true;
        SerialNumber = serialNumber;
    }
}