namespace ConsoleApp1.Core;

public class Employee: User
{
    private decimal _salary;

    public decimal Salary
    {
        get => _salary;
        set
        {
            if (value < 0) throw new ArgumentException("Salary cannot be negative");
            _salary = value;
        }
    }

    private string _jobTitle;

    public string JobTitle
    {
        get => _jobTitle;
        set
        {
            if(string.IsNullOrEmpty(value))  throw new ArgumentException("Job Title cannot be empty");
            _jobTitle = value;
        }
    }

    public Employee(string name, string surname, decimal salary, string jobTitle): base(name, surname, UserType.Employee)
    {
        _salary = salary;
        _jobTitle = jobTitle;
    }
}