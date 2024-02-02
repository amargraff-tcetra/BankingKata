using BankingKata.Contexts;
using BankingKata.Repository;
using BankingKata.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BankingKataTests
{
    public class BankFixture: IDisposable
    {
        public BankDbContext Context { get; set; } = null!;
        public TransactionRepository TransactionRepository { get; private set; } = null!;
        public TransactionService TransactionService { get; private set; } = null!;


        public BankFixture()
        {
            ResetFixture();
        }

        public void Dispose()
        {
            Context.DisposeAsync();
        }

        public void ResetFixture()
        {
            var dbName = Guid.NewGuid().ToString();
            var dbContextOptions = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            Context = new BankDbContext(dbContextOptions);
            TransactionRepository = new TransactionRepository(Context);
            TransactionService = new TransactionService(TransactionRepository);
        }
    }

    [CollectionDefinition("BankFixtureCollection")]
    public class BankFixtureCollection : ICollectionFixture<BankFixture> { }
}
