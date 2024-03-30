using Backend.Repositories;


using Backend.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Logic
{
    public class CollegeRepository : IDataRepository<College, int>
    {
        AdmissionDbContext ctx;

        CollectionRespons<College> collection = new CollectionRespons<College>();
        SingleObjectRespons<College> single = new SingleObjectRespons<College> ();

        public CollegeRepository(AdmissionDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<SingleObjectRespons<College>> CreateAsync(College entity)
        {
            try
            {
                ctx.Colleges.Add(entity);
                await ctx.SaveChangesAsync();
                return new SingleObjectRespons<College> { Record = entity, StatusCode = 200, Message = "College created successfully" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<College> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<SingleObjectRespons<College>> DeleteAsync(int id)
        {
            try
            {
                var college = await ctx.Colleges.FindAsync(id);
                if (college == null)
                {
                    return new SingleObjectRespons<College> { Record = null, StatusCode = 404, Message = "College not found" };
                }

                ctx.Colleges.Remove(college);
                await ctx.SaveChangesAsync();

                return new SingleObjectRespons<College> { Record = college, StatusCode = 200, Message = "College deleted successfully" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<College> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<SingleObjectRespons<College>> GetAsync(string name)
        {
            try
            {
                var college = await ctx.Colleges.FirstOrDefaultAsync(c => c.Name == name);
                if (college == null)
                {
                    return new SingleObjectRespons<College> { Record = null, StatusCode = 404, Message = "College not found" };
                }

                return new SingleObjectRespons<College> { Record = college, StatusCode = 200, Message = "College found" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<College> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<CollectionRespons<College>> GetAsync()
        {
            try
            {
                var colleges = await ctx.Colleges.ToListAsync();
                return new CollectionRespons<College> { Records = colleges, StatusCode = 200, Message = "Colleges retrieved successfully" };
            }
            catch (Exception ex)
            {
                return new CollectionRespons<College> { Records = null, StatusCode = 500, Message = ex.Message };
            }
        }

        public async Task<SingleObjectRespons<College>> UpdateAsync(int id, College entity)
        {
            try
            {
                var existingCollege = await ctx.Colleges.FindAsync(id);
                if (existingCollege == null)
                {
                    return new SingleObjectRespons<College> { Record = null, StatusCode = 404, Message = "College not found" };
                }

                existingCollege.Name = entity.Name;
                existingCollege.Description = entity.Description;

                ctx.Colleges.Update(existingCollege);
                await ctx.SaveChangesAsync();

                return new SingleObjectRespons<College> { Record = existingCollege, StatusCode = 200, Message = "College updated successfully" };
            }
            catch (Exception ex)
            {
                return new SingleObjectRespons<College> { Record = null, StatusCode = 500, Message = ex.Message };
            }
        }

       
    }
}