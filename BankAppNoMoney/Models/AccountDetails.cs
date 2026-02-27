using BankAppNoMoney.Types;

namespace BankAppNoMoney.Models;

internal class AccountDetails
{
    internal string q { get; set; } = "";
    internal string a { get; set; } = string.Empty;
    internal decimal c { get; set; }
    internal AccountType AccountType { get; set; }
}
