using BankingKata.Mappers;
using BankingKata.Models;
using BankingKata.Models.DTOs;
using FastEndpoints;

namespace BankingKata.Endpoints
{
    public class TransactionEndpoint: Endpoint<TransactionRequest,TransactionResponse, TransactionMapper>
    {
        public override void Configure()
        {
            Post("api/transaction");
            AllowAnonymous();
        }

        public override async Task HandleAsync(TransactionRequest request, CancellationToken ct)
        {
            Transaction transaction = Map.ToEntity(request);
            //TODO: Write transaction to database using Service and get back balance
            var response = Map.FromEntity(transaction);
            await SendAsync(response);
        }
    }
}
