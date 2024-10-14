using BankIntegrationIPN.Dtos;
using BankIntegrationIPN.Entities;

namespace BankIntegrationIPN.Services
{
    public interface IPaymentService
    { 
        bool ValidateChecksum(string secretKey, PaymentDto paymentDto);
        string GenerateChecksum(string secretKey, PaymentDto paymentDto);
        Task<bool> ProcessPaymentAsync(PaymentDto paymentDto); // New method for processing payments
    }
}

