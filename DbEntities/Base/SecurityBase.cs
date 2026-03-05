namespace DbEntities.Base;

public abstract class SecurityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    protected string Symbol { get; set; } = "";
    protected string Name { get; set; } = "";

    public SecurityBase(string symbol, string name)
    {
        Symbol = symbol;
        Name = name;
    }
}
