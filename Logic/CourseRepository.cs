using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public class CourseRepository : ICourseRepository<Course, int>
    {
        private readonly AdmissionDbContext ctx;

        private SingleObjectRespons<Course> single = new SingleObjectRespons<Course>();
        private CollectionRespons<Course> collection = new CollectionRespons<Course>();

        public CourseRepository(AdmissionDbContext ctx)
        {
            this.ctx = ctx;
        }

        async Task<SingleObjectRespons<Course>> ICourseRepository<Course, int>. CreatecourseAsync(Course entity)
        {
            try
            {
                ctx.Courses.Add(entity);
                await ctx.SaveChangesAsync();
                single.Record = entity;
                single.StatusCode = 200;
                single.Message = "Course created successfully";
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

        async Task<SingleObjectRespons<Course>> ICourseRepository<Course, int>.DeletecourseAsync(int id)
        {
            try
            {
                var course = await ctx.Courses.FindAsync(id);
                if (course == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "Course not found";
                    return single;
                }

                ctx.Courses.Remove(course);
                await ctx.SaveChangesAsync();

                single.Record = course;
                single.StatusCode = 200;
                single.Message = "Course deleted successfully";
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

         async Task<SingleObjectRespons<Course>> ICourseRepository<Course, int>.GetcourseAsync(string name)
        {
            try
            {
                var course = await ctx.Courses.FirstOrDefaultAsync(c => c.CourseName == name);
                if (course == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "Course not found";
                    return single;
                }

                single.Record = course;
                single.StatusCode = 200;
                single.Message = "Course found";
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

        async Task<CollectionRespons<Course>> ICourseRepository<Course, int>.GetcourseAsync()
        {
            try
            {
                var courses = await ctx.Courses.ToListAsync();
                collection.Records = courses;
                collection.StatusCode = 200;
                collection.Message = "Courses retrieved successfully";
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

         async Task<SingleObjectRespons<Course>> ICourseRepository<Course, int>.UpdatecourseAsync(int id, Course entity)
        {
            try
            {
                var existingCourse = await ctx.Courses.FindAsync(id);
                if (existingCourse == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "Course not found";
                    return single;
                }

                existingCourse.CourseName = entity.CourseName;

                ctx.Courses.Update(existingCourse);
                await ctx.SaveChangesAsync();

                single.Record = existingCourse;
                single.StatusCode = 200;
                single.Message = "Course updated successfully";
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

         async Task<CollectionRespons<Course>> ICourseRepository<Course, int>.GetcourseByCollegeIdAsync(int CollegeId)
        {
            try
            {
                var courses = await ctx.Courses
                                        .Where(c => c.collegeUniqueId == CollegeId)
                                        .ToListAsync();

                if (courses == null || courses.Count == 0)
                {
                    collection.Records = null;
                    collection.StatusCode = 404;
                    collection.Message = "No courses found for the specified college";
                    return collection;
                }

                collection.Records = courses;
                collection.StatusCode = 200;
                collection.Message = "Courses retrieved successfully";
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
