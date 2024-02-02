using BankingKata.Mappers;
using BankingKata.Models;
using BankingKata.Models.DTOs;
using BankingKata.Services;
using FastEndpoints;

namespace BankingKata.Endpoints
{
    public class TransactionEndpoint: Endpoint<TransactionRequest,TransactionResponse, TransactionMapper>
    {
        private readonly TransactionService _service;
        public TransactionEndpoint(TransactionService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Post("api/transaction");
            AllowAnonymous();
        }

        public override async Task HandleAsync(TransactionRequest request, CancellationToken ct)
        {
            Transaction transaction = Map.ToEntity(request);
            var result = await _service.AddAsync(transaction);
            var response = Map.FromEntity(result);
            await SendAsync(response);
        }
    }
}
