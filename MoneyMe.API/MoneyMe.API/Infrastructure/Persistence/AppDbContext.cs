using Microsoft.EntityFrameworkCore;
using MoneyMe.API.Models;

namespace MoneyMe.API.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        // dotnet ef migrations add {MIGRATION NAME} -c AppDbContext --output-dir .\Infrastructure\Migrations -to Create Migration
        // dotnet ef database update InitialMigration -c AppDbContext -to Migrate to Database
        // dotnet ef migrations remove -c AppDbContext -to Remove Migration

        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Loan> Loans => Set<Loan>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("MoneyMeDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }

}
