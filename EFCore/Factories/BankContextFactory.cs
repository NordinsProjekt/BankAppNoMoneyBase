using Microsoft.EntityFrameworkCore;

namespace EFCore.Factories;

public class BankContextFactory : IDbContextFactory<BankContext>
{
    private readonly DbContextOptions<BankContext> _options;

    public BankContextFactory()
    {
        _options = new DbContextOptionsBuilder<BankContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankAppNoMoney;Trusted_Connection=True;")
            .Options;
    }

    public BankContext CreateDbContext() => new(_options);
}
