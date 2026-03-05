using DbEntities.Base;

namespace DbEntities.Security;

public class Stock : SecurityBase
{
    public Stock(string symbol, string name) : base(symbol, name)
    {
    }
}
