using BankingKata.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingKata.Contexts
{
    public class BankDbContext: DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

        public DbSet<Transaction> Transaction { get; set; }
    }
}
