namespace BankIntegrationIPN.Dtos
{
    public class PaymentDto
    {
        public required string TranId { get; set; }
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public required string ResultCode { get; set; }
        public required string ResponseMessage { get; set; }
        public required string ReceiptNumber { get; set; }
        public required string Checksum { get; set; }
    }
}
