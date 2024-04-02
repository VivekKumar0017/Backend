/*using Backend.Models;
using Backend.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public class AdmissionRepository : IStudentRepository<Admission, int>
    {
       AdmissionDbContext _context;
        CollectionRespons<Admission> collection = new CollectionRespons<Admission>();
        SingleObjectRespons<Admission> single = new SingleObjectRespons<Admission>();

        public AdmissionRepository(AdmissionDbContext context)
        {
            _context = context;
        }

        public async Task<SingleObjectRespons<Admission>> CreateAsync(Admission entity)
        {
            var response = new SingleObjectRespons<Admission>();
            try
            {
                var result = await _context.Admissions.AddAsync(entity);
                await _context.SaveChangesAsync();
                single.Record = result.Entity;
                single.Message = "New Admission Record is added successfully";
                single.StatusCode = 200;
            }
            catch (Exception ex)
            {
                single.Message = ex.Message;
                single.StatusCode = 500;
            }
            return single;
        }

        public async Task<SingleObjectRespons<Admission>> DeleteAsync(int id)
        {
            var response = new SingleObjectRespons<Admission>();
            try
            {
                var admissionToDelete = await _context.Admissions.FindAsync(id);
                if (admissionToDelete != null)
                {
                    _context.Admissions.Remove(admissionToDelete);
                    await _context.SaveChangesAsync();
                    single.Message = "Admission Record is deleted successfully";
                    single.StatusCode = 200;
                }
                else
                {
                    single.Message = $"Admission based on Id={id} is not found";
                    single.StatusCode = 404;
                }
            }
            catch (Exception ex)
            {
                single.Message = ex.Message;
                single.StatusCode = 500;
            }
            return single;
        }

        public async Task<CollectionRespons<Admission>> GetAllAsync()
        {
            var response = new CollectionRespons<Admission>();
            try
            {
                collection.Records = _context.Admissions.ToList();
                if (collection.Records != null)
                {


                    collection.Message = "All Admissions are retrieved successfully";
                    collection.StatusCode = 200;
                }
                else
                {
                    single.Message = $"Admissions not found";
                    single.StatusCode = 404;
                }
            }
            catch (Exception ex)
            {
                collection.Message = ex.Message;
                collection.StatusCode = 500;
            }
            return collection;
        }

        public async Task<SingleObjectRespons<Admission>> GetByIdAsync(int id)
        {
            var response = new SingleObjectRespons<Admission>();
            try
            {
                var admission = await _context.Admissions.FindAsync(id);
                if (admission != null)
                {
                    single.Record = admission;
                    single.Message = "Admission Record is retrieved successfully";
                    single.StatusCode = 200;
                }
                else
                {
                    single.Message = $"Admission based on Id={id} is not found";
                    single.StatusCode = 404;
                }
            }
            catch (Exception ex)
            {
                single.Message = ex.Message;
                single.StatusCode = 500;
            }
            return single;
        }

        public async Task<SingleObjectRespons<Admission>> UpdateAsync(int id, Admission entity)
        {
            var response = new SingleObjectRespons<Admission>();
            try
            {
                var admissionToUpdate = await _context.Admissions.FindAsync(id);

                if (admissionToUpdate != null)
                {
                    admissionToUpdate.CollegeId = entity.CollegeId;
                    admissionToUpdate.StudentId = entity.StudentId;
                    admissionToUpdate.Course = entity.Course;
                    

                    await _context.SaveChangesAsync();
                    single.Record = admissionToUpdate;
                    single.Message = "Admission Record is updated successfully";
                    single.StatusCode = 200;
                }
                else
                {
                    single.Message = $"Admission based on Id={id} is not found";
                    single.StatusCode = 404;
                }
            }
            catch (Exception ex)
            {
                single.Message = ex.Message;
                single.StatusCode = 500;
            }
            return single;
        }
    }
}
*/