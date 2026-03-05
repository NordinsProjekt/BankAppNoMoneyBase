using DbEntities.Base;

namespace DbEntities.Security;

public class MutualFund : SecurityBase
{
    public MutualFund(string symbol, string name) : base(symbol, name)
    {
    }
}
