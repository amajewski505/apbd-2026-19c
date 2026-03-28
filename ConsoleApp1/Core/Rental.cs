namespace ConsoleApp1.Core;

public class Rental
{
    private static int _id { get; set; }

    public int Id { get; private set; }
    public DateTime RentDateTime { get; private set; }
    public DateTime ReturnDateTime { get; set; }
    public DateTime? FactualReturnDateTime { get; set; }
    public bool ReturnedInTimeRange { get; set; }
    public User Person { get; set; }
    public decimal PentaltyApplied { get; set; }
    
    private Equipment _rentalItem { get; set; }
    public Equipment RentalItem { get => _rentalItem;
        private set
        {
            if(value == null) throw new ArgumentNullException(nameof(value));
            _rentalItem = value;
        }
    }
    
    public Rental(Equipment rentalItem, User person, DateTime returnDateTime)
    {
        Id = ++_id;
        RentDateTime = DateTime.Now;
        RentalItem = rentalItem;
        Person = person;
        ReturnDateTime = returnDateTime;
    }
}