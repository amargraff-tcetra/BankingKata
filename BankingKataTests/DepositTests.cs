using BankingKata.Contexts;
using BankingKata.Endpoints;
using BankingKata.Mappers;
using BankingKata.Models;
using BankingKata.Models.DTOs;
using BankingKata.Repository;
using BankingKata.Services;
using FakeItEasy;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKataTests
{
    public class DepositTests: IClassFixture<BankFixture>
    {
        private readonly BankDbContext _db;
        public DepositTests(BankFixture fixture)
        {
            _db = fixture.Context;
        }

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

        [Fact]
        public async Task DepositServiceTest()
        {
            //Arrange
            await _db.Transaction.AddAsync(new Transaction()
            {
                id = 1,
                account_id = 1,
                amount = 100.00m,
                date_time = new DateTime(2024, 2, 2, 12, 12, 0),
                success = true
            });
            _db.SaveChanges();

            var repository = new TransactionRepository(_db);
   
            var service = new TransactionService(repository);
            var newTransaction = new Transaction()
            {
                account_id = 1,
                amount = 50.00m,
                date_time = new DateTime(2024, 2, 2, 12, 57, 0),
            };

            //Act
            var transaction = await service.AddAsync(newTransaction);

            //Assert
            Assert.Equal(150.00m, transaction.balance);
        }
    }
}
