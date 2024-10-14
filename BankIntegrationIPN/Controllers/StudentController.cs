using BankIntegrationIPN.Entities;
using BankIntegrationIPN.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankIntegrationIPN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        // GET: api/student/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound($"Student with Id = {id} not found.");
            }

            return Ok(student);
        }


        // POST: api/student
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student data is null.");
            }

            var createdStudent = await _studentService.CreateStudentAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
        }

        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            if (id != updatedStudent.Id)
            {
                return BadRequest("Student ID mismatch.");
            }

            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with Id = {id} not found.");
            }

            await _studentService.UpdateStudentAsync(id, updatedStudent);
            return NoContent(); // Successfully updated
        }


        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with Id = {id} not found.");
            }

            await _studentService.DeleteStudentAsync(id);
            return NoContent(); // Successfully deleted
        }

    }
}
