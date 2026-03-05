namespace DbEntities.Base;

public class SecurityTransactionBase
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int Quantity { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime PurchaseDate { get; set; }

    public SecurityTransactionBase() { }

    public SecurityTransactionBase(Guid id, int quantity, decimal purchasePrice, DateTime purchaseDate)
    {
        Id = id;
        Quantity = quantity;
        PurchasePrice = purchasePrice;
        PurchaseDate = purchaseDate;
    }

    public decimal GetCurrentValue(decimal currentPrice) => currentPrice * Quantity;
    public decimal GetTotalCost() => PurchasePrice * Quantity;
}
