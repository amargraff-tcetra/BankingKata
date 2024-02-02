using BankingKata.Endpoints;
using BankingKata.Mappers;
using BankingKata.Models.DTOs;
using FakeItEasy;
using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKataTests
{
    public class DepositTests
    {
        [Fact]
        public async Task DepositReturnsAccountId()
        {
            // Arrange
            TransactionEndpoint endpoint = Factory.Create<TransactionEndpoint>();
            endpoint.Map = new();

            var request = new TransactionRequest
            {
                account_id = 1,
                TransactionType = "deposit",
                amount = 100.00m
            };

            // Act
            await endpoint.HandleAsync(request, default);
            TransactionResponse response = endpoint.Response;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.account_id);
        }
    }
}
