using BankingKata.Models;
using BankingKata.Repository;

namespace BankingKata.Services
{
    public class TransactionService
    {
        private IRepository<Transaction> _transactionRepository { get; set; }

        public TransactionService(IRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<Transaction>> GetAllAccountTransactions(int accountId) 
        {
            List<Transaction> transactions = await _transactionRepository.GetAllAsync();
            List<Transaction> accountTransactions = transactions.Where(t => t.account_id == accountId).ToList();
            return accountTransactions.OrderByDescending(t => t.date_time).ToList();
        }
    }
}
