﻿// <auto-generated />
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(AdmissionDbContext))]
    partial class AdmissionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Backend.Models.College", b =>
                {
                    b.Property<int>("collegeUniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("collegeUniqueId"));

                    b.Property<int>("CollegeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("collegeUniqueId");

                    b.ToTable("Colleges");
                });

            modelBuilder.Entity("Backend.Models.Course", b =>
                {
                    b.Property<int>("courseUniqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("courseUniqueId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("collegeUniqueId")
                        .HasColumnType("int");

                    b.HasKey("courseUniqueId");

                    b.HasIndex("collegeUniqueId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Backend.Models.Student", b =>
                {
                    b.Property<int>("AdmissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdmissionId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("collegeUniqueId")
                        .HasColumnType("int");

                    b.HasKey("AdmissionId");

                    b.HasIndex("collegeUniqueId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Backend.Models.StudentCourse", b =>
                {
                    b.Property<int>("AdmissionId")
                        .HasColumnType("int");

                    b.Property<int>("courseUniqueId")
                        .HasColumnType("int");

                    b.HasKey("AdmissionId", "courseUniqueId");

                    b.HasIndex("courseUniqueId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("Backend.Models.Course", b =>
                {
                    b.HasOne("Backend.Models.College", "College")
                        .WithMany("Courses")
                        .HasForeignKey("collegeUniqueId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("College");
                });

            modelBuilder.Entity("Backend.Models.Student", b =>
                {
                    b.HasOne("Backend.Models.College", "College")
                        .WithMany("Students")
                        .HasForeignKey("collegeUniqueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("College");
                });

            modelBuilder.Entity("Backend.Models.StudentCourse", b =>
                {
                    b.HasOne("Backend.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("AdmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("courseUniqueId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Backend.Models.College", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Backend.Models.Course", b =>
                {
                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("Backend.Models.Student", b =>
                {
                    b.Navigation("StudentCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
