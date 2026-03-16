using GarageApp.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GarageApp.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<JobCard> JobCards { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<LookupMaster> LookupMasters { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Car>()
                .HasOne(c => c.Customer)
                .WithMany(cu => cu.Cars)
                .HasForeignKey(c => c.CustomerId);

            builder.Entity<Car>()
                .HasIndex(c => c.RegistrationNumber)
                .IsUnique();

            builder.Entity<JobCard>()
                .HasOne(j => j.Customer)
                .WithMany(c => c.JobCards)
                .HasForeignKey(j => j.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<JobCard>()
                .HasOne(j => j.Car)
                .WithMany(c => c.JobCards)
                .HasForeignKey(j => j.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Invoice>()
                .HasOne(i => i.JobCard)
                .WithOne(j => j.Invoice)
                .HasForeignKey<Invoice>(i => i.JobCardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Invoice>()
                .HasIndex(i => i.JobCardId)
                .IsUnique();

            builder.Entity<Invoice>()
                .Property(i => i.TotalAmount)
                .HasPrecision(18, 2);

            builder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Invoice)
                .WithMany(i => i.Items)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InvoiceItem>()
                .Property(ii => ii.Amount)
                .HasPrecision(18, 2);

            builder.Entity<LookupMaster>()
                .HasIndex(x => x.Code)
                .IsUnique();

            builder.Entity<LookupItem>()
                .HasOne(x => x.LookupMaster)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.LookupMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<LookupItem>()
                .HasIndex(x => new { x.LookupMasterId, x.Code })
                .IsUnique();
        }
    }
}