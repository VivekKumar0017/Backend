
using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Logic
{
    public class CourseRepository : ICourseRepository<Course, int>
    {
        AdmissionDbContext ctx;

        CollectionRespons<Course> collection = new CollectionRespons<Course>();
        SingleObjectRespons<Course> single = new SingleObjectRespons<Course>();

        public CourseRepository(AdmissionDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<SingleObjectRespons<Course>> CreatecourseAsync(Course entity)
        {
            try
            {
                ctx.Courses.Add(entity);
                await ctx.SaveChangesAsync();
                return new SingleObjectRespons<Course> { Record = entity, StatusCode = 200, Message = "Course created successfully" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<Course> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<SingleObjectRespons<Course>> DeletecourseAsync(int id)
        {
            try
            {
                var course = await ctx.Courses.FindAsync(id);
                if (course == null)
                {
                    return new SingleObjectRespons<Course> { Record = null, StatusCode = 404, Message = "Course not found" };
                }

                ctx.Courses.Remove(course);
                await ctx.SaveChangesAsync();

                return new SingleObjectRespons<Course> { Record = course, StatusCode = 200, Message = "Course deleted successfully" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<Course> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<SingleObjectRespons<Course>> GetcourseAsync(string name)
        {
            try
            {
                var course = await ctx.Courses.FirstOrDefaultAsync(c => c.CourseName == name);
                if (course == null)
                {
                    return new SingleObjectRespons<Course> { Record = null, StatusCode = 404, Message = "Course not found" };
                }

                return new SingleObjectRespons<Course> { Record = course, StatusCode = 200, Message = "Course found" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<Course> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<CollectionRespons<Course>> GetcourseAsync()
        {
            try
            {
                var courses = await ctx.Courses.ToListAsync();
                return new CollectionRespons<Course> { Records = courses, StatusCode = 200, Message = "Courses retrieved successfully" };
            }
            catch (Exception ex)
            {
                return new CollectionRespons<Course> { Records = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<SingleObjectRespons<Course>> UpdatecourseAsync(int id, Course entity)
        {
            try
            {
                var existingCourse = await ctx.Courses.FindAsync(id);
                if (existingCourse == null)
                {
                    return new SingleObjectRespons<Course> { Record = null, StatusCode = 404, Message = "Course not found" };
                }

                existingCourse.CourseName = entity.CourseName;
               

                ctx.Courses.Update(existingCourse);
                await ctx.SaveChangesAsync();

                return new SingleObjectRespons<Course> { Record = existingCourse, StatusCode = 200, Message = "Course updated successfully" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<Course> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<CollectionRespons<Course>> GetcourseByCollegeIdAsync(int CollegeId)
        {
            try
            {
                var courses = await ctx.Courses
                                        .Where(c => c.collegeUniqueId == CollegeId)
                                        .ToListAsync();

                if (courses == null || courses.Count == 0)
                {
                    return new CollectionRespons<Course> { Records = null, StatusCode = 404, Message = "No courses found for the specified college" };
                }

                return new CollectionRespons<Course> { Records = courses, StatusCode = 200, Message = "Courses retrieved successfully" };
            }
            catch (Exception ex)
            {
                return new CollectionRespons<Course> { Records = null, StatusCode = 500, Message = ex.Message };
            }
        }
    }
}