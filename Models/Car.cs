using System.ComponentModel.DataAnnotations;

namespace CarHireSystem.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Make is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Make must be between 2 and 50 characters")]
        public required string Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Model must be between 2 and 50 characters")]
        public required string Model { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2025, ErrorMessage = "Year must be between 1900 and 2025")]
        public int Year { get; set; }

        [Required(ErrorMessage = "License Plate is required")]
        [RegularExpression("^[A-Z0-9]{6,8}$", ErrorMessage = "License plate must be 6-8 alphanumeric characters")]
        public required string LicensePlate { get; set; }

        [Required(ErrorMessage = "Daily Rate is required")]
        [Range(0, 10000, ErrorMessage = "Daily rate must be between 0 and 10000")]
        public decimal DailyRate { get; set; }

        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Color is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Color must be between 3 and 30 characters")]
        public required string Color { get; set; }

        [Required(ErrorMessage = "Mileage is required")]
        [Range(0, 1000000, ErrorMessage = "Mileage must be between 0 and 1000000")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "Vehicle Type is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Vehicle type must be between 2 and 50 characters")]
        public required string VehicleType { get; set; }
    }
}
