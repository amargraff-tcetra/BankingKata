using BankingKata.Contexts;
using BankingKata.Endpoints;
using BankingKata.Models;
using BankingKata.Models.DTOs;
using BankingKata.Repository;
using BankingKata.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System;

namespace BankingKataTests
{
    //[Collection("BankFixtureCollection")]
    public class DepositTests : IClassFixture<BankFixture>
    {
        private readonly BankFixture _fixture;

        public DepositTests(BankFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetFixture();
        }

        [Fact]
        public async Task DepositReturnsAccountId()
        {
            // Arrange
            TransactionEndpoint endpoint = Factory.Create<TransactionEndpoint>(_fixture.TransactionService);
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


        [Fact]
        public async Task DepositServiceTest()
        {
            //Arrange
            await _fixture.Context.Transaction.AddAsync(new Transaction()
            {
                id = 1,
                account_id = 1,
                amount = 100.00m,
                date_time = new DateTime(2024, 2, 2, 12, 12, 0),
                success = true
            });
            await _fixture.Context.SaveChangesAsync();

            var newTransaction = new Transaction()
            {
                account_id = 1,
                amount = 25.00m,
                date_time = new DateTime(2024, 2, 2, 12, 57, 0),
            };

            //Act
            var transaction = await _fixture.TransactionService.AddAsync(newTransaction);

            //Assert
            Assert.Equal(125.00m, transaction.balance);
        }


        [Fact]
        public async Task DepositReturnsBalance()
        {
            // Arrange
            TransactionEndpoint endpoint = Factory.Create<TransactionEndpoint>(_fixture.TransactionService);
            endpoint.Map = new();

            var request = new TransactionRequest
            {
                account_id = 1,
                TransactionType = "deposit",
                amount = 100.00m
            };
            var transactions = await _fixture.TransactionService.GetAllAccountTransactions(1);

            // Act
            await endpoint.HandleAsync(request, default);
            TransactionResponse response = endpoint.Response;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(100.00m, response.balance);
        }
    }
}
