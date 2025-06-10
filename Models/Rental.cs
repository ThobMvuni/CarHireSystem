using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarHireSystem.Models
{
    public partial class Rental : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.Property(r => r.RentalDuration).HasComputedColumnSql("DATEDIFF(DAY, StartDate, EndDate)");
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Car is required")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [EndDateValidation(ErrorMessage = "End Date must be after Start Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Total Amount is required")]
        [Range(0, 100000, ErrorMessage = "Total amount must be between 0 and 100000")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("^(Active|Completed|Cancelled)$", ErrorMessage = "Status must be Active, Completed, or Cancelled")]
        public required string Status { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public required string Notes { get; set; }

        public required virtual Car Car { get; set; }
        public required virtual Customer Customer { get; set; }

        public int RentalDuration
        {
            get { return (EndDate - StartDate).Days; }
        }

        public class EndDateValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var rental = (Rental)validationContext.ObjectInstance;
                if (rental.StartDate >= rental.EndDate)
                {
                    return new ValidationResult("End Date must be after Start Date");
                }
                return ValidationResult.Success;
            }
        }
    }
}
