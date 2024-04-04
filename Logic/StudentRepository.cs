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

        async Task<CollectionRespons<string>> IStudentRepository<Student, int>.GetCoursesByAdmissionIdAsync(int id)
        {
            try
            {
                var student = await ctx.Students
                    .Include(s => s.Courses)
                    .FirstOrDefaultAsync(s => s.AdmissionId == id);

                if (student == null)
                {
                    return new CollectionRespons<string>
                    {
                        StatusCode = 404,
                        Message = "No data found.",
                        Records = null
                    };
                }

                var courses = student.Courses.Select(c => c.CourseName).ToList();

                if (courses == null || courses.Count == 0)
                {
                    return new CollectionRespons<string>
                    {
                        StatusCode = 404,
                        Message = "No courses found for the student.",
                        Records = null
                    };
                }

                return new CollectionRespons<string>
                {
                    StatusCode = 200,
                    Message = "Courses retrieved successfully.",
                    Records = courses
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new CollectionRespons<string>
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Records = null
                };
            }
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
                existingStudent.Email = entity.Email;
                existingStudent.PhoneNumber = entity.PhoneNumber;
                existingStudent.Gender = entity.Gender;
                existingStudent.collegeUniqueId = entity.collegeUniqueId;
                existingStudent.Address = entity.Address;

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

        async Task<SingleObjectRespons<Student>> IStudentRepository<Student, int>.AssignCoursesToStudentAsync(int studentId, List<int> courseUniqueIds)
        {
            try
            {
                var student = await ctx.Students.Include(s => s.Courses).FirstOrDefaultAsync(s => s.AdmissionId == studentId);

                if (student == null)
                {
                    return new SingleObjectRespons<Student>
                    {
                        StatusCode = 404,
                        Message = "Student not found.",
                        Record = null
                    };
                }

                var courses = await ctx.Courses.Where(c => courseUniqueIds.Contains(c.courseUniqueId)).ToListAsync();

                if (courses.Count != courseUniqueIds.Count)
                {
                    return new SingleObjectRespons<Student>
                    {
                        StatusCode = 404,
                        Message = "One or more courses not found.",
                        Record = null
                    };
                }

                // Assign courses to the student
                student.Courses ??= new List<Course>();
                foreach (var course in courses)
                {
                    if (!student.Courses.Any(c => c.courseUniqueId == course.courseUniqueId))
                    {
                        student.Courses.Add(course);
                    }
                }

                await ctx.SaveChangesAsync();

                return new SingleObjectRespons<Student>
                {
                    StatusCode = 200,
                    Message = "Courses assigned successfully.",
                    Record = student
                };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<Student>
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Record = null
                };
            }
        }
       
    }
}
