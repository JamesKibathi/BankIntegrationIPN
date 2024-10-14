using BankIntegrationIPN.Data;
using BankIntegrationIPN.Dtos;
using BankIntegrationIPN.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace BankIntegrationIPN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/payment/ipn
        [HttpPost("ipn")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            // Validate checksum (optional)
            if (!ValidateChecksum(paymentDto))
            {
                return BadRequest("Invalid checksum.");
            }

            // Create payment and associate with student
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
            await _context.SaveChangesAsync();

            return Ok("Payment processed successfully");
        }
        private bool ValidateChecksum(PaymentDto paymentDto)
        {
            // Simulate checksum validation (HMAC-SHA256)
            string secretKey = "your-secret-key";
            string data = $"{paymentDto.TranId}{paymentDto.StudentId}{paymentDto.Amount}";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                var checksum = BitConverter.ToString(computedHash).Replace("-", "").ToLower();

                // In reality, the checksum should come from the client (bank)
                return checksum == "expected-checksum";
            }
        }


    }
}
