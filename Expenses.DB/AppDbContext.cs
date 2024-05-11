using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Expenses.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public class ExpenseContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Hard coding dev server since this is only used to create migrations
            optionsBuilder.UseSqlServer(@"Server=.;Database=ExpensesDB;TrustServerCertificate=True;User Id=nop_sa;Password=XYZ");

            return new AppDbContext(optionsBuilder.Options);
        }

    }
}
