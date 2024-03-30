using Backend.Logic;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public  IStudentRepository<Student, int> _studentRepository;

        public StudentController(IStudentRepository<Student, int> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CollectionRespons<Student>>> GetStudents()
        {
            return await _studentRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SingleObjectRespons<Student>>> GetStudent(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<SingleObjectRespons<Student>>> CreateStudent(Student student)
        {
            return await _studentRepository.CreateAsync(student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SingleObjectRespons<Student>>> UpdateStudent(int id, Student student)
        {
            return await _studentRepository.UpdateAsync(id, student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SingleObjectRespons<Student>>> DeleteStudent(int id)
        {
            return await _studentRepository.DeleteAsync(id);
        }
    }
}
