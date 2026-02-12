using BankAppNoMoney.Base;

namespace BankAppNoMoney.Accounts;

internal class BankAccount : AccountBase
{
    internal override decimal Balance()
    {
        var t = bankTransactions.Sum(x => x.Amount);
        return t + StartingBalance;
    }
}

