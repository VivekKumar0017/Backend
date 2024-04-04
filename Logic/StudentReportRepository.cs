using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public class StudentReportRepository : IStudentReportRepository<StudentReport, int>
    {
        AdmissionDbContext ctx;
        CollectionRespons<StudentReport> collection = new CollectionRespons<StudentReport>();
        SingleObjectRespons<StudentReport> single = new SingleObjectRespons<StudentReport>();

        public StudentReportRepository(AdmissionDbContext ctx)
        {
            this.ctx = ctx;
        }

        async Task<SingleObjectRespons<StudentReport>> IStudentReportRepository<StudentReport, int>.CreateReportAsync(StudentReport entity)
        {
            try
            {
                ctx.StudentReports.Add(entity);
                await ctx.SaveChangesAsync();
                single.Record = entity;
                single.StatusCode = 200;
                single.Message = "Student report created successfully.";
                return single;
            }
            catch (Exception ex)
            {
                single.Record = null;
                single.StatusCode = 500;
                single.Message = "An error occurred: " + ex.Message;
                return single;
            }
        }

        async Task<SingleObjectRespons<StudentReport>> IStudentReportRepository<StudentReport, int>.DeleteAsync(int id)
        {
            try
            {
                var report = await ctx.StudentReports.FindAsync(id);
                if (report != null)
                {
                    ctx.StudentReports.Remove(report);
                    await ctx.SaveChangesAsync();
                    single.Record = report;
                    single.StatusCode = 200;
                    single.Message = "Student report deleted successfully.";
                }
                else
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "Student report not found.";
                }
                return single;
            }
            catch (Exception ex)
            {
                single.Record = null;
                single.StatusCode = 500;
                single.Message = "An error occurred: " + ex.Message;
                return single;
            }
        }

        async Task<SingleObjectRespons<StudentReport>> IStudentReportRepository<StudentReport, int>.GetReportByIdAsync(int id)
        {
            try
            {
                var report = await ctx.StudentReports.FirstOrDefaultAsync(r => r.AdmissionId == id);
                if (report != null)
                {
                    single.Record = report;
                    single.StatusCode = 200;
                    single.Message = "Student report retrieved successfully.";
                }
                else
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "Student report not found.";
                }
                return single;
            }
            catch (Exception ex)
            {
                single.Record = null;
                single.StatusCode = 500;
                single.Message = "An error occurred: " + ex.Message;
                return single;
            }
        }

        async Task<SingleObjectRespons<StudentReport>> IStudentReportRepository<StudentReport, int>.UpdateReportAsync(int id, StudentReport entity)
        {
            try
            {
                var report = await ctx.StudentReports.FindAsync(id);
                if (report != null)
                {
                    report.Attendance = entity.Attendance;
                    report.Grade = entity.Grade;
                    await ctx.SaveChangesAsync();
                    single.Record = report;
                    single.StatusCode = 200;
                    single.Message = "Student report updated successfully.";
                }
                else
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "Student report not found.";
                }
                return single;
            }
            catch (Exception ex)
            {
                single.Record = null;
                single.StatusCode = 500;
                single.Message = "An error occurred: " + ex.Message;
                return single;
            }
        }

        async Task<SingleObjectRespons<StudentReport>> IStudentReportRepository<StudentReport, int>.getFirstLastNameById(int id)
        {

            try
            {
                var report = await ctx.StudentReports.FirstOrDefaultAsync(r => r.AdmissionId == id);
                if (report != null)
                {
                    var student = await ctx.Students.FirstOrDefaultAsync(s => s.AdmissionId == id);
                    if (student != null)
                    {
                        // Combine first name and last name into one string
                        string fullName = $"{student.FirstName} {student.LastName}";

                        // Assign the combined name to a property in the StudentReport object
                        report.FullName = fullName;

                        return new SingleObjectRespons<StudentReport>
                        {
                            Record = report,
                            StatusCode = 200,
                            Message = "Student report retrieved successfully."
                        };
                    }
                    else
                    {
                        return new SingleObjectRespons<StudentReport>
                        {
                            Record = null,
                            StatusCode = 404,
                            Message = "Student not found."
                        };
                    }
                }
                else
                {
                    return new SingleObjectRespons<StudentReport>
                    {
                        Record = null,
                        StatusCode = 404,
                        Message = "Student report not found."
                    };
                }
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<StudentReport>
                {
                    Record = null,
                    StatusCode = 500,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }
    }
}
