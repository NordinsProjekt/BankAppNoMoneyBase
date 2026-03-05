using Entities.Base;
using Services.Repositories;

namespace Services;

public class BankService
{
    private readonly IAccountRepository _accountRepository;

    public BankService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public Task<List<AccountBase>> GetAccountsAsync() =>
        _accountRepository.GetAllAsync();

    public Task<AccountBase?> GetAccountByIdAsync(Guid id) =>
        _accountRepository.GetByIdAsync(id);

    public Task AddAccountAsync(AccountBase account) =>
        _accountRepository.AddAsync(account);

    public Task DeleteAccountAsync(Guid id) =>
        _accountRepository.DeleteAsync(id);
}
