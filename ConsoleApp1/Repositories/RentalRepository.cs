using ConsoleApp1.Core;

namespace ConsoleApp1.Repositories;

public class RentalRepository: IRepository<Rental>
{
    private readonly List<Rental> _rentalItems = new();
    
    public void AddItems(params Rental[] items)
    {
        _rentalItems.AddRange(items);
    }
    
    public List<Rental> GetAllItems()
    {
        return _rentalItems;
    }
    
    public Rental? GetById(int id)
    {
        return _rentalItems.Find(rentalItem => rentalItem.Id == id);
    }
}