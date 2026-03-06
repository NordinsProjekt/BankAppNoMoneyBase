using Entities.Types;

namespace Services.Models;

public class AccountDetails(string accountName, string accountNumber, decimal startingBalance, AccountType accountType)
{
    public string AccountName { get; set; } = accountName;
    public string AccountNumber { get; set; } = accountNumber;
    public decimal StartingBalance { get; set; } = startingBalance;
    public AccountType AccountType { get; set; } = accountType;
}
