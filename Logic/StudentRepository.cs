
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

        CollectionRespons<Student> collection = new CollectionRespons<Student>();
        SingleObjectRespons<Student> single = new SingleObjectRespons<Student>();

        public StudentRepository(AdmissionDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<CollectionRespons<Student>> GetAllAsync()
        {
            try
            {
                var students = await ctx.Students.ToListAsync();
                return new CollectionRespons<Student> { Records = students, StatusCode = 200, Message = "Students retrieved successfully" };
            }
            catch (Exception ex)
            {
                return new CollectionRespons<Student> { Records = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<SingleObjectRespons<Student>> GetByIdAsync(int id)
        {
            try
            {
                var student = await ctx.Students.FindAsync(id);
                if (student == null)
                {
                    return new SingleObjectRespons<Student> { Record = null, StatusCode = 404, Message = "Student not found" };
                }

                return new SingleObjectRespons<Student> { Record = student, StatusCode = 200, Message = "Student found" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<Student> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<SingleObjectRespons<Student>> CreateAsync(Student entity)
        {
            try
            {
                ctx.Students.Add(entity);
                await ctx.SaveChangesAsync();
                return new SingleObjectRespons<Student> { Record = entity, StatusCode = 200, Message = "Student created successfully" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<Student> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<SingleObjectRespons<Student>> UpdateAsync(int id, Student entity)
        {
            try
            {
                var existingStudent = await ctx.Students.FindAsync(id);
                if (existingStudent == null)
                {
                    return new SingleObjectRespons<Student> { Record = null, StatusCode = 404, Message = "Student not found" };
                }

                existingStudent.FirstName = entity.FirstName;
                existingStudent.LastName = entity.LastName;

                ctx.Students.Update(existingStudent);
                await ctx.SaveChangesAsync();

                return new SingleObjectRespons<Student> { Record = existingStudent, StatusCode = 200, Message = "Student updated successfully" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<Student> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<SingleObjectRespons<Student>> DeleteAsync(int id)
        {
            try
            {
                var student = await ctx.Students.FindAsync(id);
                if (student == null)
                {
                    return new SingleObjectRespons<Student> { Record = null, StatusCode = 404, Message = "Student not found" };
                }

                ctx.Students.Remove(student);
                await ctx.SaveChangesAsync();

                return new SingleObjectRespons<Student> { Record = student, StatusCode = 200, Message = "Student deleted successfully" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<Student> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }
    }
}
