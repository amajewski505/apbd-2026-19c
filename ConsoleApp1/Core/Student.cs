namespace ConsoleApp1.Core;

public class Student: User
{
    private double _gpa;
    public double Gpa
    {
        get => _gpa;
        set
        {
            if (value < 1 || value > 100) throw new ArgumentOutOfRangeException("Gpa must be in range of 1-100");
            _gpa = value;
        }
    }
    
    private string _major { get; set; }

    public string Major
    {
        get => _major;
        set
        {
            if(string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Major must be a non-empty string");
            _major = value;
        }
    }

    public Student(string name, string surname, double gpa, string major): base(name, surname, UserType.Student)
    {
        Gpa = gpa;
        Major = major;
    }
}