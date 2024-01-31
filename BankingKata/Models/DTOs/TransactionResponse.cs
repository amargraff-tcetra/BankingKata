namespace BankingKata.Models.DTOs
{
    public class TransactionResponse
    {
        public int transaction_id { get; set; }
        public int account_id { get; set; }
        public decimal balance { get; set; }
        public bool success { get; set; }
    }
}
