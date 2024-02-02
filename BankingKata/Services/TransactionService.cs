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

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            var transactionId = await _transactionRepository.AddAsync(transaction);
            var accountTransactions = await GetAllAccountTransactions(transaction.account_id);
            Transaction response = transaction;
            if (transactionId != 0)
            {
                response.success = true;
                response.id = transactionId;
                response.balance = accountTransactions.Select(t => t.amount).Sum();
            }

            return response;
        }
    }
}
