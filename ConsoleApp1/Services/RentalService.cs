using ConsoleApp1.Core;
using ConsoleApp1.Repositories;
using ConsoleApp1.Utils;

namespace ConsoleApp1.Services;

public class RentalService
{
    private readonly RentalRepository _rentalRepository;
    private readonly UserService _userService;
    private readonly EquipmentService _equipmentService;

    public RentalService(UserService userService, EquipmentService equipmentService, RentalRepository rentalRepository)
    {
        _rentalRepository = rentalRepository;
        _userService = userService;
        _equipmentService = equipmentService;
    }
    

    public void ShowExpiredRentals()
    {
        foreach (var item in _rentalRepository.GetAllItems())
        {
            if (item.ReturnDateTime < DateTime.Now)
            {
                Console.WriteLine($"Item: {item.RentalItem.Name} has been overdue for ${DateTime.Now - item.ReturnDateTime}");
            }
        }
    }

    public void ShowRentalsForUser(int userId)
    {
        _rentalRepository.GetAllItems().FindAll(rentalItem => rentalItem.Person.Id == userId).ForEach(rentalItem => Console.WriteLine(
            $"{rentalItem.Person.Name} rented {rentalItem.RentalItem.Name}"));
    }

    public void RentItem(Equipment equipment, User user, DateTime returnDateTime)
    {
        if (!CanUserRentMore(user.Id)) throw new Exception("User can't rent more items");
        if (!IsItemAvailableForRent(equipment.Id)) throw new Exception("Item is not available");

        var rental = new Rental(equipment, user, returnDateTime);
        
        _rentalRepository.AddItems(rental);
    }
    
    public void ReturnItem(int rentalId)
    {
        var rental = GetRentalItem(rentalId);

        if (rental.FactualReturnDateTime.HasValue)
        {
            throw new Exception("Item has already been returned");
        }
        
        rental.FactualReturnDateTime = DateTime.Now;
        
        rental.RentalItem.IsAvailable = true;
        
        if (rental.FactualReturnDateTime.Value > rental.ReturnDateTime)
        {
            rental.ReturnedInTimeRange = false;
            
            var timeLate = rental.FactualReturnDateTime.Value - rental.ReturnDateTime;
            int daysLate = timeLate.Days;
            
            if (daysLate == 0) daysLate = 1; 
            
            rental.PentaltyApplied = daysLate * RentalConstants.PenaltyForEachDayDelay;
            
            Console.WriteLine($"Item returned late. Penalty applied: {rental.PentaltyApplied} PLN.");
        }
        else
        {
            rental.ReturnedInTimeRange = true;
            rental.PentaltyApplied = 0;
            Console.WriteLine("Item returned on time.");
        }
    }

    private bool CanUserRentMore(int userId)
    {
        var rentalCountForUser = _rentalRepository.GetAllItems()
            .FindAll(rentalItem => rentalItem.Person.Id == userId && !rentalItem.FactualReturnDateTime.HasValue).Count;
        var user = _userService.GetUserById(userId);

        switch (user.UserType)
        {
            case UserType.Student:
                return rentalCountForUser < RentalConstants.MaxStudentRentals;
            case UserType.Employee:
                return rentalCountForUser < RentalConstants.MaxEmployeeRentals;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private bool IsItemAvailableForRent(int itemId)
    {
        var isAvailable = _equipmentService.GetEquipmentById(itemId).IsAvailable;
        var isNotRented = !IsEquipmentInActiveRent(itemId);
        
        return isAvailable && isNotRented;
    }



    private bool IsEquipmentInActiveRent(int itemId)
    {
        var foundEquipment = _rentalRepository.GetAllItems()
            .Find(rentalItem => rentalItem.RentalItem.Id == itemId && !rentalItem.FactualReturnDateTime.HasValue);
        if (foundEquipment == null) return false;
        
        return true;
    }
    
    private Rental GetRentalItem(int id)
    {
        var foundRental = _rentalRepository.GetById(id);
        
        if(foundRental == null) throw new KeyNotFoundException($"Rental with id {id} not found");

        return foundRental;
    }

    public void GenerateSummaryReport()
    {
        var allRentalItems = _rentalRepository.GetAllItems();
        
        var activeRentalItems = allRentalItems.FindAll(rentalItem => !rentalItem.FactualReturnDateTime.HasValue);
        var overdueRentalItems = allRentalItems.FindAll(rentalItem => rentalItem.ReturnDateTime < DateTime.Now);
        var completedRentalItems = allRentalItems.FindAll(rentalItem => rentalItem.FactualReturnDateTime.HasValue);

        decimal totalPenalties = 0;
        foreach (var rentalItem in allRentalItems)
        {
            totalPenalties += rentalItem.PentaltyApplied;
        }
        
        Console.WriteLine("===SUMMARY REPORT===");
        Console.WriteLine($"Total rentals: {allRentalItems.Count}");
        Console.WriteLine($"Active rentals: {activeRentalItems.Count}");
        Console.WriteLine($"Overdue rentals: {overdueRentalItems.Count}");
        Console.WriteLine($"Completed rentals: {completedRentalItems.Count}");
        Console.WriteLine($"Total penalties applied: {totalPenalties} PLN");
        Console.WriteLine("===SUMMARY REPORT===");
        
    }
    
    
    
}