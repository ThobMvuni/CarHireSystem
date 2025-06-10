using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarHireSystem.Models
{
    public partial class Customer : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Age).HasComputedColumnSql("DATEDIFF(YEAR, DateOfBirth, GETDATE()) - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, DateOfBirth, GETDATE()), DateOfBirth) > GETDATE() THEN 1 ELSE 0 END");
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "First Name must contain only letters and spaces")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Last Name must contain only letters and spaces")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100 characters")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^[0-9\+\-\s]*$", ErrorMessage = "Phone number must contain only numbers, +, -, and spaces")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 20 digits")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Driver License Number is required")]
        [RegularExpression(@"^[A-Z0-9]{8}$", ErrorMessage = "Driver license number must be 8 alphanumeric characters")]
        public required string DriverLicenseNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateOfBirthValidation(ErrorMessage = "Age must be between 18 and 90 years")] // Custom validation attribute
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth > today.AddYears(-age)) age--;
                return age;
            }
        }
    }

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Age).HasComputedColumnSql("DATEDIFF(YEAR, DateOfBirth, GETDATE()) - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, DateOfBirth, GETDATE()), DateOfBirth) > GETDATE() THEN 1 ELSE 0 END");
        }
    }

    public class DateOfBirthValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Date of Birth is required");

            var dateOfBirth = (DateTime)value;
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;

            if (age < 18)
                return new ValidationResult("Customer must be at least 18 years old");
            if (age > 90)
                return new ValidationResult("Customer must be younger than 90 years old");

            return ValidationResult.Success;
        }
    }
}
