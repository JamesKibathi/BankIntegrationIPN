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
            public async Task<IEnumerable<Student>> GetAllStudentsAsync()
            {
                return await _context.Students.ToListAsync();
            }
            public async Task<Student> GetStudentByIdAsync(int id)
            {
                return await _context.Students.FindAsync(id);
            }
            public async Task<Student> CreateStudentAsync(Student student)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return student;
            }
            public async Task UpdateStudentAsync(int id, Student updatedStudent)
            {
                var student = await _context.Students.FindAsync(id);
                if (student != null)
                {
                    student.RegNo = updatedStudent.RegNo;
                    student.FirstName = updatedStudent.FirstName;
                    student.LastName = updatedStudent.LastName;
                    student.Surname = updatedStudent.Surname;
                    student.Email = updatedStudent.Email;
                    student.Phone = updatedStudent.Phone;
                    await _context.SaveChangesAsync();
                }
            }

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
