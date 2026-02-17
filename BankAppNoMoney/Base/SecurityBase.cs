namespace BankAppNoMoney.Base;

internal abstract class SecurityBase
{
    internal Guid Id { get; set; } = Guid.NewGuid();
    protected string Symbol { get; set; } = "";
    protected string Name { get; set; } = "";
    internal List<SecurityTransactionBase> Transactions { get; set; } = [];

    internal SecurityBase(string symbol, string name)
    {
        Symbol = symbol;
        Name = name;
    }
}
