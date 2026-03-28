namespace ConsoleApp1.Repositories;

public interface IRepository<T> where T : class
{
    public void AddItems(params T[] items);
    public List<T> GetAllItems();
    public T? GetById(int id);
}