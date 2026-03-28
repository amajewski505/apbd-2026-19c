using ConsoleApp1.Core;

namespace ConsoleApp1.Repositories;

public class UserRespository: IRepository<User>
{
    private readonly List<User> _users = new();

    public void AddItems(params User[] users)
    {
        _users.AddRange(users);
    }

    public User? GetById(int id)
    {
        return _users.Find(user => user.Id == id);
    }
    
    public List<User> GetAllItems()
    {
        return _users;
    }
}