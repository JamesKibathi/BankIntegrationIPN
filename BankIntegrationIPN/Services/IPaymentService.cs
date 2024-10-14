using BankIntegrationIPN.Dtos;

namespace BankIntegrationIPN.Services
{
    public interface IPaymentService
    {
        bool ValidateChecksum(string secretKey, PaymentDto paymentDto);
        string GenerateChecksum(string secretKey, PaymentDto paymentDto);
    }
}
