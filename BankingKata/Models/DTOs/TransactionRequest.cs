namespace BankingKata.Models.DTOs
{
    public class TransactionRequest
    {
        public int account_id { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public decimal amount { get; set; }
    }
}
