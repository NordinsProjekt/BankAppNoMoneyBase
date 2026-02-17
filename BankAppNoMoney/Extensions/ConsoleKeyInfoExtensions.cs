namespace BankAppNoMoney.Extensions;

public static class ConsoleKeyInfoExtensions
{
    public static int GetNewSelectionIndex(this ConsoleKeyInfo input, int selectedOption, int optionsLength)
    {
        switch (input.Key)
        {
            case ConsoleKey.UpArrow:
                if (--selectedOption < 0)
                {
                    selectedOption = optionsLength - 1;
                }
                break;
            case ConsoleKey.DownArrow:
                if (++selectedOption >= optionsLength)
                {
                    selectedOption = 0;
                }
                break;
            default: break;
        }

        return selectedOption;
    }
}