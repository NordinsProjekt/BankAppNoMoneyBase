using BankAppNoMoney.Types;

namespace BankAppNoMoney.Models;

internal class AccountDetails
{
    internal string AccountName { get; set; } = "";
    internal string AccountNumber { get; set; } = string.Empty;
    internal decimal StartingBalance { get; set; }
    internal AccountType AccountType { get; set; }
}
