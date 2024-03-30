using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Models
{
    public class AdmissionDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Admission> Admissions { get; set; }

        public AdmissionDbContext(DbContextOptions<AdmissionDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<College>()
                    .HasMany(c => c.Courses)
                    .WithOne(c => c.College)
                    .HasForeignKey(c => c.CollegeId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Course>()
                    .HasOne(c => c.College)
                    .WithMany(c => c.Courses)
                    .HasForeignKey(c => c.CollegeId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Student>()
                    .HasMany(s => s.Admissions)
                    .WithOne(a => a.Student)
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Admission>()
                    .HasOne(a => a.College)
                    .WithMany(c => c.Admissions)
                    .HasForeignKey(a => a.CollegeId) // Explicitly specify the foreign key property
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Admission>()
                    .HasOne(a => a.Student)
                    .WithMany(s => s.Admissions)
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure other entities and their relationships here...

                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                // Log or throw the exception as appropriate
                Console.WriteLine($"An error occurred during model configuration: {ex.Message}");
            }
        }
    }
}
