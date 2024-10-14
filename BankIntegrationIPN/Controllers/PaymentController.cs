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
        private readonly ApplicationDbContext _context;

        public PaymentController(IConfiguration configuration, IPaymentService paymentService)
        {
            _secretKey = configuration.GetSection("PaymentSettings:SecretKey").Value;
            _paymentService = paymentService;
            _context = context;

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

            // Proceed with payment processing
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

    }
}
