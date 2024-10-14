using System.ComponentModel.DataAnnotations.Schema;


namespace BankIntegrationIPN.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public required string TranId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public required string ResultCode { get; set; }
        public required string ResponseMessage { get; set; }
        public required string ReceiptNumber { get; set; }

        // Foreign key to associate payment with a student
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }
    }
}
