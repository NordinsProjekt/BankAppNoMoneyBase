using Entities.Types;

namespace Services.Models;

public class AccountDetails
{
    public string AccountName { get; set; } = "";
    public string AccountNumber { get; set; } = string.Empty;
    public decimal StartingBalance { get; set; }
    public AccountType AccountType { get; set; }
}
