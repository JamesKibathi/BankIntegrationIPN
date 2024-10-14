using BankIntegrationIPN.Data;
using BankIntegrationIPN.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankIntegrationIPN.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all students
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        // Retrieve a specific student by ID
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            }

            return student;
        }

        // Create a new student
        public async Task<Student> CreateStudentAsync(Student student)
        {
            // Ensure only valid student information is being added
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        // Update an existing student
        public async Task UpdateStudentAsync(int id, Student updatedStudent)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                // Update only the relevant student fields
                student.RegNo = updatedStudent.RegNo;
                student.FirstName = updatedStudent.FirstName;
                student.LastName = updatedStudent.LastName;
                student.Surname = updatedStudent.Surname;
                student.Email = updatedStudent.Email;
                student.Phone = updatedStudent.Phone; // Optional
                await _context.SaveChangesAsync();
            }
        }

        // Delete a student
        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}