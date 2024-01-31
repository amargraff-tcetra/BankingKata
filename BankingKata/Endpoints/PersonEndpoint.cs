using BankingKata.Models.DTOs;
using FastEndpoints;

namespace BankingKata.Endpoints
{
    public class PersonEndpoint : Endpoint<PersonRequest, PersonResponse>
    {
        public override void Configure()
        {
            Post("/api/user/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PersonRequest req, CancellationToken ct)
        {
            await SendAsync(new()
            {
                FullName = req.FirstName + " " + req.LastName,
                IsOver18 = req.Age > 18
            });
        }
    }
}
