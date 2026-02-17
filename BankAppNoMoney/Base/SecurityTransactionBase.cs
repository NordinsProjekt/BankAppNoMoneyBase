namespace BankAppNoMoney.Base;

internal class SecurityTransactionBase
{
    internal Guid Id { get; set; } = Guid.NewGuid();

    internal int Quantity { get; set; }
    internal decimal PurchasePrice { get; set; }
    internal DateTime PurchaseDate { get; set; }

    internal SecurityTransactionBase() { }

    internal SecurityTransactionBase(Guid id, int quantity, decimal purchasePrice, DateTime purchaseDate)
    {
        Id = id;
        Quantity = quantity;
        PurchasePrice = purchasePrice;
        PurchaseDate = purchaseDate;
    }

    internal decimal GetCurrentValue(decimal currentPrice) => currentPrice * Quantity;
    internal decimal GetTotalCost() => PurchasePrice * Quantity;
}
