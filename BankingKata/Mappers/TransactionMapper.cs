using BankingKata.Models;
using BankingKata.Models.DTOs;
using FastEndpoints;

namespace BankingKata.Mappers
{
    public class TransactionMapper: Mapper<TransactionRequest, TransactionResponse, Transaction>
    {
        public override Transaction ToEntity(TransactionRequest r)
        {
            Transaction transaction = new Transaction()
            {
                account_id = r.account_id,
                date_time = DateTime.Now,
            };

            switch (r.TransactionType.ToLower())
            {
                case "deposit":
                    transaction.amount = r.amount;
                    break;
                case "withdraw":
                    transaction.amount = -1 * r.amount;
                    break;
                default:
                    transaction.amount = 0;
                    break;
            }

            return transaction;
        }

        public override TransactionResponse FromEntity(Transaction e)
        {
            TransactionResponse response = new TransactionResponse()
            {
                transaction_id = e.id,
                account_id = e.account_id,
                balance = e.balance,
                success = e.success,
            };

            return response;
        }
    }
}
