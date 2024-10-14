using BankIntegrationIPN.Data;
using BankIntegrationIPN.Dtos;
using BankIntegrationIPN.Entities;
using BankIntegrationIPN.Services;
using Microsoft.AspNetCore.Mvc;


namespace BankIntegrationIPN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly string? _secretKey;

        public PaymentController(IConfiguration configuration, IPaymentService paymentService)
        {
            _secretKey = configuration.GetSection("PaymentSettings:SecretKey").Value;
            _paymentService = paymentService;
        }

        // POST: api/payment/ipn
        [HttpPost("ipn")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            if (string.IsNullOrEmpty(_secretKey))
            {
                return BadRequest("Secret key is missing.");
            }

            // Validate the checksum using the service
            if (!_paymentService.ValidateChecksum(_secretKey, paymentDto))
            {
                return BadRequest("Invalid checksum.");
            }

            // Use the PaymentService to process the payment
            var result = await _paymentService.ProcessPaymentAsync(paymentDto);
            if (!result)
            {
                return BadRequest("Payment processing failed.");
            }

            return Ok("Payment accepted successfully");
        }

        // GET: api/payment/students/{studentId}
        [HttpGet("students/{studentId}")]
        public async Task<IActionResult> GetPaymentsByStudentId(int studentId)
        {
            var payments = await _paymentService.GetPaymentsByStudentIdAsync(studentId);
            if (payments == null || !payments.Any())
            {
                return NotFound($"No payments found for student with ID = {studentId}.");
            }

            return Ok(payments);
        }
    }
}