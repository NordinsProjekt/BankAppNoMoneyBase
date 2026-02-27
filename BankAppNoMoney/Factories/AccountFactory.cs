using BankAppNoMoney.Accounts;
using BankAppNoMoney.Base;
using BankAppNoMoney.Types;

namespace BankAppNoMoney.Factories;

internal static class AccountFactory
{
    internal static AccountBase CreateAccount(string name, string numbe)
    {
        return accountDetails.AccountType switch
        {
            AccountType.BankAccount => new BankAccount(accountDetails.AccountName,
                                accountDetails.AccountNumber, accountDetails.StartingBalance),
            AccountType.IskAccount => new IskAccount(accountDetails.AccountName,
                                accountDetails.AccountNumber, accountDetails.StartingBalance),
            AccountType.UddevallaAccount => new UddevallaAccount(accountDetails.AccountName,
                                accountDetails.AccountNumber, accountDetails.StartingBalance),
            _ => throw new NotImplementedException(),
        };
    }
}
