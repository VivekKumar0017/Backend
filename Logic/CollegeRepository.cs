using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public class CollegeRepository : IDataRepository<College, int>
    {
        private readonly AdmissionDbContext ctx;

        private CollectionRespons<College> collection = new CollectionRespons<College>();
        private SingleObjectRespons<College> single = new SingleObjectRespons<College>();

        public CollegeRepository(AdmissionDbContext ctx)
        {
            this.ctx = ctx;
        }

        async Task<SingleObjectRespons<College>> IDataRepository<College, int>. CreateAsync(College entity)
        {
            try
            {
                ctx.Colleges.Add(entity);
                await ctx.SaveChangesAsync();
                single.Record = entity;
                single.StatusCode = 200;
                single.Message = "College created successfully";
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

        async Task<SingleObjectRespons<College>> IDataRepository<College, int>.DeleteAsync(int id)
        {
            try
            {
                var college = await ctx.Colleges.FindAsync(id);
                if (college == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "College not found";
                    return single;
                }

                ctx.Colleges.Remove(college);
                await ctx.SaveChangesAsync();

                single.Record = college;
                single.StatusCode = 200;
                single.Message = "College deleted successfully";
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

         async Task<SingleObjectRespons<College>> IDataRepository<College, int>.GetAsync(string name)
        {
            try
            {
                var college = await ctx.Colleges.FirstOrDefaultAsync(c => c.Name == name);
                if (college == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "College not found";
                    return single;
                }

                single.Record = college;
                single.StatusCode = 200;
                single.Message = "College found";
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

       async Task<CollectionRespons<College>> IDataRepository<College, int>.GetAsync()
        {
            try
            {
                var colleges = await ctx.Colleges.ToListAsync();
                collection.Records = colleges;
                collection.StatusCode = 200;
                collection.Message = "Colleges retrieved successfully";
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

       async Task<SingleObjectRespons<College>> IDataRepository<College, int>.UpdateAsync(int id, College entity)
        {
            try
            {
                var existingCollege = await ctx.Colleges.FindAsync(id);
                if (existingCollege == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "College not found";
                    return single;
                }

                existingCollege.Name = entity.Name;
                existingCollege.Description = entity.Description;

                ctx.Colleges.Update(existingCollege);
                await ctx.SaveChangesAsync();

                single.Record = existingCollege;
                single.StatusCode = 200;
                single.Message = "College updated successfully";
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

         async Task<SingleObjectRespons<College>> IDataRepository<College, int>.ReviewStudentApplicationAsync(int collegeId, int admissionId, ApprovalStatus status)
        {
            try
            {
                var college = await ctx.Colleges.Include(c => c.Students)
                                                .FirstOrDefaultAsync(c => c.collegeUniqueId == collegeId);
                if (college == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "College not found";
                    return single;
                }

                var student = college.Students.FirstOrDefault(s => s.AdmissionId == admissionId);
                if (student == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "Student not found";
                    return single;
                }

                if (status == ApprovalStatus.Rejected)
                {
                   
                    ctx.Students.Remove(student);
                    await ctx.SaveChangesAsync();
                    single.Record = college;
                    single.StatusCode = 200;
                    single.Message = "Student application rejected and record deleted successfully";
                    return single;
                }
                else
                {
                   
                    student.Status = status;
                    ctx.Students.Update(student);
                    await ctx.SaveChangesAsync();
                    single.Record = college;
                    single.StatusCode = 200;
                    single.Message = "Student review updated successfully";
                    return single;
                }
            }
            catch (Exception ex)
            {
                single.Record = null;
                single.StatusCode = 500;
                single.Message = ex.Message;
                return single;
            }
        }
        public async Task<SingleObjectRespons<College>> GetCollegeByIdAsync(int id)
        {
            try
            {
                var college = await ctx.Colleges.FindAsync(id);
                if (college == null)
                {
                    single.Record = null;
                    single.StatusCode = 404;
                    single.Message = "College not found";
                    return single;
                }

                single.Record = college;
                single.StatusCode = 200;
                single.Message = "College found";
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

    }
}
