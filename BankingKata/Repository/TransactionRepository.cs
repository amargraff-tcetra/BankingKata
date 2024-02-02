using BankingKata.Contexts;
using BankingKata.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BankingKata.Repository
{
    

    public class TransactionRepository: IRepository<Transaction>
    {
        //IDbConnection _connection;
        private readonly BankDbContext _context;
        public TransactionRepository(BankDbContext context)
        {
            //dapper
            //_connection = new SqlConnection(configuration.GetSection("DB_CONNECTION_STRING").Value);
            //EF (for in memory db testing)
            _context = context;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            //var transactions = await _connection.QueryAsync<Transaction>("SELECT * FROM transaction");
            var transactions = await _context.Transaction.Select(t => t).ToListAsync();
            return transactions.ToList();
        }

        //public async Task<List<Transaction>> GetAllAsync(int account_id)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@account_id", account_id);
        //    return await _connection.QueryAsync<Transaction>("SELECT * FROM transaction WHERE account_id = @account_id", parameters);
        //}

        public Task<Transaction> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Transaction transaction)
        {
            //return await _connection.ExecuteAsync("INSERT INTO transaction (account_id, amount, date_time) VALUES @account_id, @amount, @date_time", transaction);
            await _context.Transaction.AddAsync(transaction);
            _context.SaveChanges();
            return transaction.id;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
