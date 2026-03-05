using EFCore;
using EFCore.Factories;
using EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Repositories;

namespace BankAppNoMoney;

public class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = BuildServiceProvider();
        var bankService = serviceProvider.GetRequiredService<BankService>();

        await new Bank(bankService).ShowBankMenu();
    }

    private static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IDbContextFactory<BankContext>, BankContextFactory>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<BankService>();

        return services.BuildServiceProvider();
    }
}
