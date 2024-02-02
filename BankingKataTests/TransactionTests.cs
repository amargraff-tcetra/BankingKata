using BankingKata.Endpoints;
using BankingKata.Mappers;
using BankingKata.Models;
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
    }
}
