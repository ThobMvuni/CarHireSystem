using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CarHireSystem.Data;
using CarHireSystem.Models;
using CarHireSystem.Services;

namespace CarHireSystem
{
    static class Program
    {
        private static readonly CarHireContext _context = new CarHireContext(
            new DbContextOptionsBuilder<CarHireContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarHireDb;Trusted_Connection=True;")
                .Options);
        
        static void Main(string[] args)
        {
            
            InitializeDatabase();
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Car Hire System ===");
                Console.WriteLine("1. Manage Cars");
                Console.WriteLine("2. Manage Customers");
                Console.WriteLine("3. Manage Rentals");
                Console.WriteLine("4. Exit");
                Console.Write("\nSelect an option: ");
                
                var choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        ManageCars();
                        break;
                    case "2":
                        ManageCustomers();
                        break;
                    case "3":
                        ManageRentals();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void InitializeDatabase()
        {
            _context.Database.EnsureCreated();
        }

        private static void ManageCars()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Car Management ===");
                Console.WriteLine("1. Add New Car");
                Console.WriteLine("2. View All Cars");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("\nSelect an option: ");
                
                var choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        AddCar();
                        break;
                    case "2":
                        ViewCars();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void AddCar()
        {
            var make = ValidationService.ReadValidString("Make", 2, 50, "Make must be between 2 and 50 characters");
            var model = ValidationService.ReadValidString("Model", 2, 50, "Model must be between 2 and 50 characters");
            var year = ValidationService.ReadValidInteger("Year", 1900, 2025, "Year must be between 1900 and 2025");
            var licensePlate = ValidationService.ReadValidString("License Plate", 6, 8, "License plate must be 6-8 alphanumeric characters");
            var dailyRate = ValidationService.ReadValidDecimal("Daily Rate", 0, 10000, "Daily rate must be between 0 and 10000");
            var color = ValidationService.ReadValidString("Color", 3, 30, "Color must be between 3 and 30 characters");
            var mileage = ValidationService.ReadValidInteger("Mileage", 0, 1000000, "Mileage must be between 0 and 1000000");
            var vehicleType = ValidationService.ReadValidString("Vehicle Type", 2, 50, "Vehicle type must be between 2 and 50 characters");

            var car = new Car
            {
                Make = make,
                Model = model,
                Year = year,
                LicensePlate = licensePlate,
                DailyRate = dailyRate,
                Color = color,
                Mileage = mileage,
                VehicleType = vehicleType,
                IsAvailable = true
            };
            
            if (!ValidationService.ValidateCarInput(car, out var errorMessage))
            {
                Console.WriteLine($"\nError: {errorMessage}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }
            
            try
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
                Console.WriteLine("\nCar added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError saving car: {ex.Message}");
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void ViewCars()
        {
            try
            {
                var cars = _context.Cars.ToList();
                Console.WriteLine("\n=== Available Cars ===");
                
                if (!cars.Any())
                {
                    Console.WriteLine("No cars found in the database.");
                }
                else
                {
                    foreach (var car in cars)
                    {
                        Console.WriteLine($"\nID: {car.Id}");
                        Console.WriteLine($"Make: {car.Make}");
                        Console.WriteLine($"Model: {car.Model}");
                        Console.WriteLine($"Year: {car.Year}");
                        Console.WriteLine($"License Plate: {car.LicensePlate}");
                        Console.WriteLine($"Daily Rate: ${car.DailyRate}");
                        Console.WriteLine($"Color: {car.Color}");
                        Console.WriteLine($"Mileage: {car.Mileage} km");
                        Console.WriteLine($"Vehicle Type: {car.VehicleType}");
                        Console.WriteLine($"Status: {(car.IsAvailable ? "Available" : "Rented")}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError retrieving cars: {ex.Message}");
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void ManageCustomers()
        {
            // TODO: Implement customer management
            Console.WriteLine("Customer management coming soon!");
            Console.ReadKey();
        }

        private static void ManageRentals()
        {
            // TODO: Implement rental management
            Console.WriteLine("Rental management coming soon!");
            Console.ReadKey();
        }
    }
}
