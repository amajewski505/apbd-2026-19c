using ConsoleApp1.Core;
using ConsoleApp1.Repositories;

namespace ConsoleApp1.Services;

public class EquipmentService
{
    private readonly EquipmentRepository _equipmentRepository;

    public EquipmentService(EquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public void ShowAllItems()
    {
        foreach (var item in _equipmentRepository.GetAllItems())
        {
            var status = item.IsAvailable ? "Available" : "Unavailable";
            Console.WriteLine($"Equipment: {item.Name} | Status: '{status}'");
        }
    }

    public void ShowAvailableItems()
    {
        foreach (var item in _equipmentRepository.GetAllItems())
        {
            if(item.IsAvailable) Console.WriteLine($"Equipment: {item.Name}");
        }
    }

    public void AddEquipmentItems(params Equipment[] items)
    {
        foreach (var item in items)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
        }
        _equipmentRepository.AddItems(items);
    }

    public void MakeEquipmentItemUnavailable(int id)
    {
        var item = _equipmentRepository.GetById(id);
        if(item == null)
            throw new NullReferenceException($"Equipment item with id {id} not found");

        item.IsAvailable = false;
    }

    public Equipment GetEquipmentById(int id)
    {
        var foundEquipment = _equipmentRepository.GetById(id);
        
        if(foundEquipment == null) throw new KeyNotFoundException($"Equipment with id {id} not found");
        
        return foundEquipment;
    }
    
    
}