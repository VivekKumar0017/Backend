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
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository<Student, int> _studentRepository;

        public StudentController(IStudentRepository<Student, int> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Authorize(Policy = "CollegePolicy")]
        public async Task<ActionResult<CollectionRespons<Student>>> GetStudents()
        {
            return await _studentRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CollegePolicy")]
        public async Task<ActionResult<SingleObjectRespons<Student>>> GetStudent(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<ActionResult<SingleObjectRespons<Student>>> CreateStudent(Student student)
        {
            return await _studentRepository.CreateAsync(student);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<ActionResult<SingleObjectRespons<Student>>> UpdateStudent(int id, Student student)
        {
            return await _studentRepository.UpdateAsync(id, student);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<ActionResult<SingleObjectRespons<Student>>> DeleteStudent(int id)
        {
            return await _studentRepository.DeleteAsync(id);
        }

        [HttpGet("pending/{collegeId}")]
        [Authorize(Policy = "CollegePolicy")]
        public async Task<ActionResult<CollectionRespons<Student>>> GetPendingStudents(int collegeId)
        {
            return await _studentRepository.GetPendingStudentsAsync(collegeId);
        }
    }
}
