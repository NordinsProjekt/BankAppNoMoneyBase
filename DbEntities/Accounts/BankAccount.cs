using DbEntities.Base;

namespace DbEntities.Accounts;

public class BankAccount : AccountBase
{
    public BankAccount()
    {
    }

    public BankAccount(string accountName, string accountNumber, decimal startingBalance, decimal interestRate = 1.0m)
        : base(accountName, accountNumber, startingBalance, interestRate)
    {
    }

    public override decimal Balance()
    {
        var t = bankTransactions.Sum(x => x.Amount);
        return t + StartingBalance;
    }
}

