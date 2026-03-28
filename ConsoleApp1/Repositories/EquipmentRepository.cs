using ConsoleApp1.Core;

namespace ConsoleApp1.Repositories;

public class EquipmentRepository: IRepository<Equipment>
{
    private readonly List<Equipment> _equipmentItems = new();
    
    public void AddItems(params Equipment[] items)
    {
        _equipmentItems.AddRange(items);
    }

    public List<Equipment> GetAllItems()
    {
        return _equipmentItems;
    }

    public Equipment? GetById(int id)
    {
        return _equipmentItems.Find(equipmentItem => equipmentItem.Id == id);
    }
}