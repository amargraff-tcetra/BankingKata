namespace BankingKata.Models.DTOs
{
    public class PersonResponse
    {
        public string FullName { get; set; } = string.Empty;
        public bool IsOver18 { get; set; } = false;
    }
}
