﻿using BankIntegrationIPN.Entities;

namespace BankIntegrationIPN.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<Student> CreateStudentAsync(Student student);
        Task UpdateStudentAsync(int id, Student student);
        Task DeleteStudentAsync(int id);
    }
}
