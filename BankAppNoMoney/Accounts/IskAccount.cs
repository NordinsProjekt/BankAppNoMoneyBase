using BankAppNoMoney.Base;

namespace BankAppNoMoney.Accounts;

internal class IskAccount : AccountBase
{
    public IskAccount()
    {
    }

    public IskAccount(string accountName, string accountNumber, decimal startingBalance, decimal interestRate) : base(accountName, accountNumber, startingBalance, interestRate)
    {
    }

    internal override decimal Balance()
    {
        var t = bankTransactions.Sum(x => x.Amount);
        return t + StartingBalance;
    }
}
