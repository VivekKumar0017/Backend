using Backend.Logic;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly IDataRepository<College, int> _collegeRepository;

        public CollegeController(IDataRepository<College, int> collegeRepository)
        {
            _collegeRepository = collegeRepository;
        }

        [HttpGet]
        [Authorize(Policy = "CollegePolicy")]
        /*[Authorize(Policy = "StudentPolicy")]*/
        
        public async Task<ActionResult<CollectionRespons<College>>> GetColleges()
        {
            return await _collegeRepository.GetAsync();
        }

        [HttpGet("{name}")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<ActionResult<SingleObjectRespons<College>>> GetCollegeByName(string name)
        {
            return await _collegeRepository.GetAsync(name);
        }

        [HttpPost]
        [Authorize(Policy = "CollegePolicy")]
        public async Task<ActionResult<SingleObjectRespons<College>>> CreateCollege(College college)
        {
            return await _collegeRepository.CreateAsync(college);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "CollegePolicy")]
        public async Task<ActionResult<SingleObjectRespons<College>>> UpdateCollege(int id, College college)
        {
            return await _collegeRepository.UpdateAsync(id, college);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "CollegePolicy")]
        public async Task<ActionResult<SingleObjectRespons<College>>> DeleteCollege(int id)
        {
            return await _collegeRepository.DeleteAsync(id);
        }
        [HttpPost("{collegeId}/ReviewStudentApplication/{admissionId}")]
        [Authorize(Policy = "CollegePolicy")]
        public async Task<ActionResult<SingleObjectRespons<College>>> ReviewStudentApplication(int collegeId, int admissionId, ApprovalStatus status)
        {
            return await _collegeRepository.ReviewStudentApplicationAsync(collegeId, admissionId, status);
        }
    }
}
