using Entities.Base;

namespace Services.Repositories;

public interface IAccountRepository
{
    Task<List<AccountBase>> GetAllAsync();
    Task<AccountBase?> GetByIdAsync(Guid id);
    Task AddAsync(AccountBase account);
    Task DeleteAsync(Guid id);
}
