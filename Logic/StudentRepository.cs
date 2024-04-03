using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public class StudentRepository : IStudentRepository<Student, int>
    {
        AdmissionDbContext ctx;

        SingleObjectRespons<Student> single = new SingleObjectRespons<Student>();
        CollectionRespons<Student> collection = new CollectionRespons<Student>();

        public StudentRepository(AdmissionDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<CollectionRespons<Student>> GetAllAsync()
        {
            try
            {
                var students = await ctx.Students.ToListAsync();
                collection.Records = students;
                collection.StatusCode = 200;
                collection.Message = "Students retrieved successfully";
                return collection;
            }
            catch (Exception ex)
            {
                collection.Records = null;
                collection.StatusCode = 500;
                collection.Message = ex.Message;
                return collection;
            }
        }

        public async Task<SingleObjectRespons<Student>> GetByIdAsync(int id)
        {
            try
            {
                var student = await ctx.Students.FindAsync(id);
                if (student == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "Student not found";
                    return single;
                }

                single.Record = student;
                single.StatusCode = 200;
                single.Message = "Student found";
                return single;
            }
            catch (Exception ex)
            {
                single.Record = null;
                single.StatusCode = 500;
                single.Message = ex.Message;
                return single;
            }
        }

        public async Task<SingleObjectRespons<Student>> CreateAsync(Student entity)
        {
            try
            {
                ctx.Students.Add(entity);
                await ctx.SaveChangesAsync();
                single.Record = entity;
                single.StatusCode = 200;
                single.Message = "Student created successfully";
                return single;
            }
            catch (Exception ex)
            {
                single.Record = null;
                single.StatusCode = 500;
                single.Message = ex.Message;
                return single;
            }
        }

        public async Task<SingleObjectRespons<Student>> UpdateAsync(int id, Student entity)
        {
            try
            {
                var existingStudent = await ctx.Students.FindAsync(id);
                if (existingStudent == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "Student not found";
                    return single;
                }

                existingStudent.FirstName = entity.FirstName;
                existingStudent.LastName = entity.LastName;

                ctx.Students.Update(existingStudent);
                await ctx.SaveChangesAsync();

                single.Record = existingStudent;
                single.StatusCode = 200;
                single.Message = "Student updated successfully";
                return single;
            }
            catch (Exception ex)
            {
                single.Record = null;
                single.StatusCode = 500;
                single.Message = ex.Message;
                return single;
            }
        }

        public async Task<SingleObjectRespons<Student>> DeleteAsync(int id)
        {
            try
            {
                var student = await ctx.Students.FindAsync(id);
                if (student == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "Student not found";
                    return single;
                }

                ctx.Students.Remove(student);
                await ctx.SaveChangesAsync();

                single.Record = student;
                single.StatusCode = 200;
                single.Message = "Student deleted successfully";
                return single;
            }
            catch (Exception ex)
            {
                single.Record = null;
                single.StatusCode = 500;
                single.Message = ex.Message;
                return single;
            }
        }

        async Task<CollectionRespons<Student>> IStudentRepository<Student, int>.GetPendingStudentsAsync(int collegeId)
        {
            try
            {
                
                var college = await ctx.Colleges.Include(c => c.Students)
                                                .FirstOrDefaultAsync(c => c.collegeUniqueId == collegeId);
                if (college == null)
                {
                    collection.Records = null;
                    collection.StatusCode = 404;
                    collection.Message = "College not found";
                    return collection;
                }

                
                var pendingStudents = college.Students
                                             .Where(s => s.Status == ApprovalStatus.Pending)
                                             .ToList();

                if (!pendingStudents.Any())
                {
                    collection.Records = null;
                    collection.StatusCode = 404;
                    collection.Message = "No pending students found";
                    return collection;
                }

                
                collection.Records = pendingStudents;
                collection.StatusCode = 200;
                collection.Message = "Pending students retrieved successfully";
                return collection;
            }
            catch (Exception ex)
            {
               
                collection.Records = null;
                collection.StatusCode = 500;
                collection.Message = ex.Message;
                return collection;
            }
        }
    }
}
