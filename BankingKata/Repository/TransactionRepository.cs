using BankingKata.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BankingKata.Repository
{
    

    public class TransactionRepository: IRepository<Transaction>
    {
        IDbConnection _connection;
        public TransactionRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetSection("DB_CONNECTION_STRING").Value);
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            var transactions = await _connection.QueryAsync<Transaction>("SELECT * FROM transaction");
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

        public Task<int> AddAsync(Transaction transaction)
        {
            throw new NotImplementedException();
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
