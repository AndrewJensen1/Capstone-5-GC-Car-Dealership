using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Capstone_5_GC_Car_Dealership.Models
{
    public partial class CarDbContext : DbContext
    {
        public CarDbContext()
        {
        }

        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cars> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CarDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Cars>(entity =>
            {
                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(30);
            });
        }
    }
}
