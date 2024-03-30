using Backend.Logic;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        public IStudentRepository<Admission, int> _admissionRepository;

        public AdmissionController(IStudentRepository<Admission, int> admissionRepository)
        {
            _admissionRepository = admissionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CollectionRespons<Admission>>> GetAdmissions()
        {
            return await _admissionRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SingleObjectRespons<Admission>>> GetAdmissionById(int id)
        {
            return await _admissionRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<SingleObjectRespons<Admission>>> CreateAdmission(Admission admission)
        {
            return await _admissionRepository.CreateAsync(admission);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SingleObjectRespons<Admission>>> UpdateAdmission(int id, Admission admission)
        {
            return await _admissionRepository.UpdateAsync(id, admission);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SingleObjectRespons<Admission>>> DeleteAdmission(int id)
        {
            return await _admissionRepository.DeleteAsync(id);


        }
    }
}
