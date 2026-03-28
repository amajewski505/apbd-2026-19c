using ConsoleApp1.Core;
using ConsoleApp1.Repositories;

namespace ConsoleApp1.Services;

public class UserService
{
    private UserRespository _userRepository;
    
    public UserService(UserRespository userRepository)
    {
        _userRepository = userRepository;
    }
    
    
    public User GetUserById(int id)
    {
        var foundUser = _userRepository.GetById(id);
        
        if(foundUser == null) throw new KeyNotFoundException($"User with id {id} not found");
        
        return foundUser;
    }
}