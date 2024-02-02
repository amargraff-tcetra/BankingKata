using BankingKata.Contexts;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKataTests
{
    public class BankFixture: IDisposable
    {
        public BankDbContext Context { get;}

        public BankFixture()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(databaseName: "Bank")
                        .Options;
            Context = new BankDbContext(dbContextOptions);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
