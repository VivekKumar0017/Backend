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
    public class CourseController : ControllerBase
    {
        private readonly IDataRepository<Course, int> _courseRepository;

        public CourseController(IDataRepository<Course, int> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CollectionRespons<Course>>> GetCourses()
        {
            return await _courseRepository.GetAsync();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<SingleObjectRespons<Course>>> GetCourse(string name)
        {
            return await _courseRepository.GetAsync(name);
        }

        [HttpPost]
        public async Task<ActionResult<SingleObjectRespons<Course>>> CreateCourse(Course course)
        {
            return await _courseRepository.CreateAsync(course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SingleObjectRespons<Course>>> UpdateCourse(int id, Course course)
        {
            return await _courseRepository.UpdateAsync(id, course);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SingleObjectRespons<Course>>> DeleteCourse(int id)
        {
            return await _courseRepository.DeleteAsync(id);
        }
    }
}
