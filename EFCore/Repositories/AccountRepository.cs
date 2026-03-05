using Entities.Base;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;

namespace EFCore.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IDbContextFactory<BankContext> _contextFactory;

    public AccountRepository(IDbContextFactory<BankContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<AccountBase>> GetAllAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Accounts.ToListAsync();
    }

    public async Task<AccountBase?> GetByIdAsync(Guid id)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Accounts.FindAsync(id);
    }

    public async Task AddAsync(AccountBase account)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Accounts.Add(account);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        using var context = _contextFactory.CreateDbContext();
        var account = await context.Accounts.FindAsync(id);
        if (account != null)
        {
            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
        }
    }
}
