using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Models
{
    public class AdmissionDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Course> Courses { get; set; }
        /*public DbSet<StudentCourse> StudentCourses { get; set; }*/

        public DbSet<StudentReport> StudentReports { get; set; }
        /* public DbSet<Admission> Admissions { get; set; }*/

        public AdmissionDbContext(DbContextOptions<AdmissionDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {




                
                base.OnModelCreating(modelBuilder);


                modelBuilder.Entity<Student>()
                .HasOne(s => s.College)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.collegeUniqueId)
                .OnDelete(DeleteBehavior.Restrict);

               
                modelBuilder.Entity<Student>()
                    .HasMany(s => s.Courses)
                    .WithMany(c => c.Students)
                    .UsingEntity(j => j.ToTable("StudentCourses")); 

                
                modelBuilder.Entity<Course>()
                    .HasOne(c => c.College)
                    .WithMany(co => co.Courses)
                    .HasForeignKey(c => c.collegeUniqueId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<StudentReport>()
                .HasOne(sr => sr.Student)
                .WithMany(s => s.StudentReports)
                .HasForeignKey(sr => sr.AdmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during model configuration: {ex.Message}");
            }
        }
    }
}
