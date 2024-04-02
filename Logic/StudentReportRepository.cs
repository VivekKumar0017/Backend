using Backend.Models;
using Backend.Repositories;
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
                var report = await ctx.StudentReports.FindAsync(id);
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
    }
}
