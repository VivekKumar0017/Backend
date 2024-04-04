using Backend.Logic;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentReportController : ControllerBase
    {
        private readonly IStudentReportRepository<StudentReport, int> _studentReportRepository;

        public StudentReportController(IStudentReportRepository<StudentReport, int> studentReportRepository)
        {
            _studentReportRepository = studentReportRepository;
        }

        [HttpPost]
      //  [Authorize(Policy = "CollegePolicy")]
        public async Task<IActionResult> CreateReport(StudentReport entity)
        {
            var result = await _studentReportRepository.CreateReportAsync(entity);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        //[//Authorize(Policy = "StudentPolicy")]
       // [Authorize(Policy = "CollegePolicy")]
        public async Task<IActionResult> GetReportById(int id)
        {
            var result = await _studentReportRepository.GetReportByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
       // [Authorize(Policy = "CollegePolicy")]
        public async Task<IActionResult> UpdateReport(int id, StudentReport entity)
        {
            var result = await _studentReportRepository.UpdateReportAsync(id, entity);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
       // [Authorize(Policy = "CollegePolicy")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var result = await _studentReportRepository.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("getFirstLastNameById/{id}")]
        public async Task<IActionResult> GetFirstLastName(int id)
        {
            var result = await _studentReportRepository.getFirstLastNameById(id);
            return StatusCode(result.StatusCode, result);


        }

    }
}
