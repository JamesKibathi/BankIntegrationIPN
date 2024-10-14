using BankIntegrationIPN.Dtos;
using BankIntegrationIPN.Entities;

namespace BankIntegrationIPN.Services
{
    public interface IPaymentService
    {
        bool ValidateChecksum(string secretKey, PaymentDto paymentDto);
        string GenerateChecksum(string secretKey, PaymentDto paymentDto);
        Task<bool> ProcessPaymentAsync(PaymentDto paymentDto); // Method for processing payments
        Task<IEnumerable<Payment>> GetPaymentsByStudentIdAsync(int studentId); // New method to fetch payments by student ID
    }
}

