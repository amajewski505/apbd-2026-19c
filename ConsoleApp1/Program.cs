using ConsoleApp1.Core;
using ConsoleApp1.Repositories;
using ConsoleApp1.Services;

namespace ConsoleApp1;

internal class Program
{
    public static void Main(string[] args)
    {
        // SCENARIUSZ DEMONSTRACYJNY
        
        // For dependency injection
        var userRepository = new UserRespository(); 
        var equipmentRepository = new EquipmentRepository();
        var rentalRepository = new RentalRepository();
        
        // For dependency injection    
        var userService = new UserService(userRepository);
        var equipmentService = new EquipmentService(equipmentRepository);
        var rentalService = new RentalService(userService, equipmentService, rentalRepository);

        Console.WriteLine("--- Dodanie kilku egzemplarzy sprzętu różnych typów. ---");
        var laptop = new Laptop("Dell XPS 15", "SN-1001", 16, 15.6); 
        var camera = new Camera("Sony A7 III", "SN-2001", 24.2, SensorType.FullFrame);
        var projector = new Projector("Epson Home Cinema", "SN-3001", 3000, 150);

        equipmentRepository.AddItems(laptop, camera, projector);
        Console.WriteLine("Sprzet dodany do systemu.\n");

        Console.WriteLine("--- Dodanie kilku użytkowników różnych typów. ---");
        var student = new Student("John", "Doe", 85.5, "Computer Science");
        var employee = new Employee("Jane", "Smith", 5000m, "Professor");

        userRepository.AddItems(student, employee);
        Console.WriteLine("Uzytkownicy dodani do systemu.\n");

        Console.WriteLine("--- Poprawne wypożyczenie sprzętu. ---");
        rentalService.RentItem(laptop, student, DateTime.Now.AddDays(7));
        Console.WriteLine("Pomyslnie wypozyczono laptop studentowi na 7 dni.\n");

        Console.WriteLine("--- Próbę wykonania niepoprawnej operacji, np. wypożyczenia sprzętu niedostępnego albo przekroczenia limitu. ---");
        try
        {
            Console.WriteLine("Proba wypozyczenia juz wypozyczonego sprzetu");
            rentalService.RentItem(laptop, employee, DateTime.Now.AddDays(3));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}\n");
        }

        Console.WriteLine("--- Zwrot sprzętu w terminie. ---");
        var laptopRental = rentalRepository.GetAllItems().Find(r => r.RentalItem.Id == laptop.Id && !r.FactualReturnDateTime.HasValue);
        if (laptopRental != null)
        {
            rentalService.ReturnItem(laptopRental.Id);
        }
        Console.WriteLine();

        Console.WriteLine("--- Zwrot opóźniony skutkujący naliczeniem kary. ---");
        rentalService.RentItem(camera, employee, DateTime.Now.AddDays(-3));
        Console.WriteLine("Kamera zwrocona 3 dni po terminie");

        var cameraRental = rentalRepository.GetAllItems().Find(r => r.RentalItem.Id == camera.Id && !r.FactualReturnDateTime.HasValue);
        if (cameraRental != null)
        {
            rentalService.ReturnItem(cameraRental.Id);
        }
        Console.WriteLine();

        Console.WriteLine("--- Wyświetlenie raportu końcowego o stanie systemu. ---");
        rentalService.GenerateSummaryReport();
    }
}
