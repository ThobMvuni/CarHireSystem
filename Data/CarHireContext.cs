using Microsoft.EntityFrameworkCore;
using CarHireSystem.Models;

namespace CarHireSystem.Data
{
    public class CarHireContext : DbContext
    {
        public CarHireContext(DbContextOptions<CarHireContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany()
                .HasForeignKey(r => r.CarId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId);
        }
    }
}
