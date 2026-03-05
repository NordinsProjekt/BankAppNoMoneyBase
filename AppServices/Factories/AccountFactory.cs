using BankAppNoMoney.Models;
using DbEntities.Accounts;
using DbEntities.Base;
using DbEntities.Types;

namespace AppServices.Factories;

public static class AccountFactory
{
    public static AccountBase CreateAccount(AccountDetails accountDetails)
    {

        return accountDetails.AccountType switch
        {
            AccountType.BankAccount => new BankAccount(accountDetails.AccountName,
                                accountDetails.AccountNumber, accountDetails.StartingBalance),
            AccountType.IskAccount => new IskAccount(accountDetails.AccountName,
                                accountDetails.AccountNumber, accountDetails.StartingBalance),
            AccountType.UddevallaAccount => new UddevallaAccount(accountDetails.AccountName,
                                accountDetails.AccountNumber, accountDetails.StartingBalance),
            AccountType.MillionAccount => new MillionAccount(accountDetails.AccountName, accountDetails.AccountNumber, accountDetails.StartingBalance),
            AccountType.GoldAccount => new GoldAccount(accountDetails.AccountName, accountDetails.AccountNumber, accountDetails.StartingBalance),
            _ => throw new NotImplementedException(),
        };
    }
}
