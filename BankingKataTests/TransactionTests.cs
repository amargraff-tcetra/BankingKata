using BankingKata.Endpoints;
using BankingKata.Mappers;
using BankingKata.Models;
using BankingKata.Models.DTOs;
using BankingKata.Repository;
using BankingKata.Services;
using FakeItEasy;
using FastEndpoints;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKataTests
{
    public class TransactionTests
    {
        [Fact]
        public void TransactionDepositMapping()
        {
            // Arrange
            TransactionMapper map = new TransactionMapper();
            var request = new TransactionRequest
            {
                account_id = 1,
                TransactionType = "deposit",
                amount = 100.00m,
            };

            // Act
            Transaction transaction = map.ToEntity(request);

            // Assert
            Assert.NotNull(transaction);
            Assert.Equal(100.00m, transaction.amount);
            Assert.False(transaction.success);
        }

        [Fact]
        public void TransactionWithdrawalMapping()
        {
            // Arrange
            TransactionMapper map = new TransactionMapper();
            var request = new TransactionRequest
            {
                account_id = 1,
                TransactionType = "withdraw",
                amount = 100.00m,
            };

            // Act
            Transaction transaction = map.ToEntity(request);

            // Assert
            Assert.NotNull(transaction);
            Assert.Equal(-100.00m, transaction.amount);
            Assert.False(transaction.success);
        }

        [Fact]
        public void TransactionUnknownMapping()
        {
            // Arrange
            TransactionMapper map = new TransactionMapper();
            var request = new TransactionRequest
            {
                account_id = 1,
                TransactionType = "unknown",
                amount = 100.00m,
            };

            // Act
            Transaction transaction = map.ToEntity(request);

            // Assert
            Assert.Equal(0.0m, transaction.amount);
        }

        [Fact]
        public async Task AccountTransactionsServiceTest() 
        {
            //Arrange
            var mockTransactions = new List<Transaction>()
            {
                new Transaction()
                {
                    id = 1,
                    account_id = 1,
                    amount = 100.00m,
                    date_time = new DateTime(2024,2,2,12,12,0),
                    success = true
                },
                new Transaction()
                {
                    id = 2,
                    account_id = 2,
                    amount = 150.00m,
                    date_time = new DateTime(2024,2,2,12,18,0),
                    success = true
                },
            };
            var mockRepository = new Mock<IRepository<Transaction>>();
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockTransactions);

            //Act
            var service = new TransactionService(mockRepository.Object);
            var transactions = await service.GetAllAccountTransactions(1);

            //Assert
            Assert.Single(transactions);
            Assert.Equal(1, transactions.Single().account_id);
            Assert.Equal(100.00m, transactions.Single().amount);
        }

        [Fact]
        public async Task AccountTransactionsServiceNegativeTest() 
        {
            //Arrange
            var mockTransactions = new List<Transaction>()
            {
                new Transaction()
                {
                    id = 1,
                    account_id = 1,
                    amount = 100.00m,
                    date_time = new DateTime(2024,2,2,12,12,0),
                    success = true
                },
                new Transaction()
                {
                    id = 2,
                    account_id = 1,
                    amount = 50.00m,
                    date_time = new DateTime(2024,2,2,12,18,0),
                    success = true
                },
            };
            var mockRepository = new Mock<IRepository<Transaction>>();
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockTransactions);

            //Act
            var service = new TransactionService(mockRepository.Object);
            var transactions = await service.GetAllAccountTransactions(1);

            //Assert
            Assert.Equal(2,transactions.Count());
        }
    }
}
