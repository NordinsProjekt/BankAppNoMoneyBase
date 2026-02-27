using BankAppNoMoney.Base;

namespace BankAppNoMoney.Accounts;

internal class UddevallaAccount : AccountBase
{
    public UddevallaAccount()
    {
    }

    public UddevallaAccount(string accountName, string accountNumber, decimal startingBalance, decimal interestRate = 3.0m)
        : base(accountName, accountNumber, startingBalance, interestRate)
    {
    }

    internal override decimal Balance()
    {
        var t = bankTransactions.Sum(x => x.Amount);
        return t + StartingBalance;
    }

    internal void HejKarim()
    {
        Console.WriteLine("Hej Karim");
    }
}
