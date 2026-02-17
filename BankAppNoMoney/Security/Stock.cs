using BankAppNoMoney.Base;

namespace BankAppNoMoney.Security;

internal class Stock : SecurityBase
{
    public Stock(string symbol, string name) : base(symbol, name)
    {
    }
}
