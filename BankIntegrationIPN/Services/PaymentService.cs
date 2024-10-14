using BankIntegrationIPN.Data;
using BankIntegrationIPN.Dtos;
using BankIntegrationIPN.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BankIntegrationIPN.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context; // Initialize context
        }

        public bool ValidateChecksum(string secretKey, PaymentDto paymentDto)
        {
            var generatedChecksum = GenerateChecksum(secretKey, paymentDto);
            return generatedChecksum == paymentDto.Checksum;
        }

        public string GenerateChecksum(string secretKey, PaymentDto paymentDto)
        {
            var dataToHash = $"{paymentDto.TranId}{paymentDto.Amount}{paymentDto.StudentId}"; // Use StudentId in checksum
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public async Task<bool> ProcessPaymentAsync(PaymentDto paymentDto)
        {
            // Retrieve the student from the database using StudentId
            var student = await _context.Students.FindAsync(paymentDto.StudentId);

            // Check if the student exists
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {paymentDto.StudentId} not found.");
            }

            var payment = new Payment
            {
                TranId = paymentDto.TranId,
                StudentId = paymentDto.StudentId,
                Amount = paymentDto.Amount,
                DatePaid = paymentDto.DatePaid,
                ResultCode = paymentDto.ResultCode,
                ResponseMessage = paymentDto.ResponseMessage,
                ReceiptNumber = paymentDto.ReceiptNumber
            };

            await _context.Payments.AddAsync(payment);
            return await _context.SaveChangesAsync() > 0; // Return true if payment is saved
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByStudentIdAsync(int studentId) // New method to fetch payments for a student
        {
            return await _context.Payments
                .Where(p => p.StudentId == studentId)
                .ToListAsync();
        }
    }
}