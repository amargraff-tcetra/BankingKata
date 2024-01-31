using BankingKata.Models;

namespace BankingKata.Endpoints
{
    public class Account
    {
        public int id { get; set; }
        public List<Transaction> transactions { get; set; }

        public Account()
        {
            transactions = new List<Transaction>();
        }

        public void Deposit(int amount)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(int amount)
        {
            throw new NotImplementedException();
        }

        public string PrintStatement()
        {
            throw new NotImplementedException();
        }
    }
}
