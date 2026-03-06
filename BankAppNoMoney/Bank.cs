using BankAppNoMoney.Base;

namespace BankAppNoMoney;

internal class Bank
{
    private List<AccountBase> accounts = new List<AccountBase>();

    internal Bank()
    {
#if DEBUG
        accounts = SeedDataService.GenerateAccounts();
#endif
    }

    internal void AddAccount(AccountBase account)
    {
        accounts.Add(account);
    }

    internal void RemoveAccount(Guid accountId)
    {
        var account = accounts.FirstOrDefault(x => x.Id == accountId);
        if (account != null)
        {
            accounts.Remove(account);
        }
    }

    internal List<AccountBase> GetAccounts()
    {
        return accounts;
    }
}
