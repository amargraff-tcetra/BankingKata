using BankingKata.Endpoints;
using BankingKata.Models.DTOs;
using FakeItEasy;
using FastEndpoints;
using Microsoft.Extensions.Configuration;

namespace BankingKataTests
{
    public class SampleUnitTest
    {
        [Fact]
        public async Task Test1()
        {
            // Arrange
            var fakeConfig = A.Fake<IConfiguration>();
            PersonEndpoint endpoint = Factory.Create<PersonEndpoint>();

            var request = new PersonRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 25
            };

            // Act
            await endpoint.HandleAsync(request, default);
            PersonResponse response = endpoint.Response;

            // Assert
            Assert.NotNull(response);
            Assert.Equal("John Doe", response.FullName);
            Assert.True(response.IsOver18);
        }
    }
}