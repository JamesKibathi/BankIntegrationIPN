namespace BankIntegrationIPN.Entities
{
    public class Student
    {

        public int Id { get; set; }
        public required string RegNo { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string Surname { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

    }
}
