using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Models
{
    public class AdmissionDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<StudentReport> StudentReports { get; set; }
        /* public DbSet<Admission> Admissions { get; set; }*/

        public AdmissionDbContext(DbContextOptions<AdmissionDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {




                // Configure the one-to-many relationship between College and Student
                base.OnModelCreating(modelBuilder);

                // Configure the primary key for the join entity
                modelBuilder.Entity<StudentCourse>()
                    .HasKey(sc => new { sc.AdmissionId, sc.courseUniqueId });

                // Configure the many-to-many relationship between Student and Course
                modelBuilder.Entity<StudentCourse>()
                    .HasOne(sc => sc.Student)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.AdmissionId)
                    .OnDelete(DeleteBehavior.Cascade); 

                modelBuilder.Entity<StudentCourse>()
                    .HasOne(sc => sc.Course)
                    .WithMany(c => c.StudentCourses)
                    .HasForeignKey(sc => sc.courseUniqueId)
                    .OnDelete(DeleteBehavior.Restrict); 

                
                modelBuilder.Entity<College>()
                    .HasMany(c => c.Students)
                    .WithOne(s => s.College)
                    .HasForeignKey(s => s.collegeUniqueId)
                    .OnDelete(DeleteBehavior.Cascade); 

               
                modelBuilder.Entity<College>()
                    .HasMany(c => c.Courses)
                    .WithOne(c => c.College)
                    .HasForeignKey(c => c.collegeUniqueId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<StudentReport>()
                    .HasOne(s => s.Student)
                    .WithMany()
                    .HasForeignKey(a => a.AdmissionId);
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during model configuration: {ex.Message}");
            }
        }
    }
}
