using BankIntegrationIPN.Dtos;
using System.Security.Cryptography;
using System.Text;

namespace BankIntegrationIPN.Services
{
    public class PaymentService:IPaymentService
    {
        public bool ValidateChecksum(string secretKey, PaymentDto paymentDto)
        {
            var generatedChecksum = GenerateChecksum(secretKey, paymentDto);
            return generatedChecksum == paymentDto.Checksum;
        }

        public string GenerateChecksum(string secretKey, PaymentDto paymentDto)
        {
            var dataToHash = $"{paymentDto.TranId}{paymentDto.Amount}{paymentDto.StudentId}";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
