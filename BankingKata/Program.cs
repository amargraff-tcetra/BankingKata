using BankingKata.Contexts;
using BankingKata.Models;
using BankingKata.Repository;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository<Transaction>, TransactionRepository>();
builder.Services
.AddFastEndpoints()
.SwaggerDocument();

var useInMemoryDatabase = builder.Configuration.GetValue<bool>("USE_IN_MEMORY_DATABASE");

if (useInMemoryDatabase)
{
    builder.Services.AddDbContext<BankDbContext>(options => options.UseInMemoryDatabase("InMemoryDatabase"));
}
else
{
    // Use a real database connection string from configuration
    var connectionString = builder.Configuration.GetConnectionString("DB_CONNECTION_STRING");
    builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer(connectionString));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAuthentication()
   .UseAuthorization()
   .UseFastEndpoints()
   .UseSwaggerGen();
app.Run();
