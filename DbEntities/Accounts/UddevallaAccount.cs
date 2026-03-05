using DbEntities.Base;

namespace DbEntities.Accounts;

public class UddevallaAccount : AccountBase
{
    public UddevallaAccount()
    {
    }

    public UddevallaAccount(string accountName, string accountNumber, decimal startingBalance, decimal interestRate = 3.0m)
        : base(accountName, accountNumber, startingBalance, interestRate)
    {
    }

    public override decimal Balance()
    {
        var t = bankTransactions.Sum(x => x.Amount);
        return t + StartingBalance;
    }

    public void HejKarim()
    {
        Console.WriteLine("Hej Karim");
    }
}
