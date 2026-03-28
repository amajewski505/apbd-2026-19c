namespace ConsoleApp1.Core;

public enum UserType {
    Student,
    Employee
}


public abstract class User
{
    private static int _id { get; set; }

    public int Id { get; private set; }
    public UserType UserType { get; private set; }

    private string _name { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            if(string.IsNullOrEmpty(value)) 
                throw new ArgumentException("Name cannot be empty");
            _name = value;
        }
    }
    
    private string _surname { get; set; }
    public string Surname {
        get => _surname;
        set
        {
            if(string.IsNullOrEmpty(value)) 
                throw new ArgumentException("Surname cannot be empty");
            _surname = value;
        } 
    }

    protected User(string name, string surname, UserType userType)
    {
        Id = ++_id;
        Name = name;
        Surname = surname;
        UserType = userType;
    }
    
}