using BankAppNoMoney.Extensions;

namespace BankAppNoMoney.Base;

internal abstract class MenuBase
{
    private string[] options = [];
    private string title = "Menu";
    private int selectedOption = 0;

    internal MenuBase(string title, string[] options)
    {
        this.options = options;
        this.title = title;
    }

    internal int GetOptionsLength()
    {
        return options.Length;
    }

    internal string[] GetOptions()
    {
        return options.ToArray();
    }

    internal virtual int ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"..:: {title} ::..");
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedOption)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                }
                Console.Write(options[i] + "\n");
                Console.ResetColor();
            }

            var input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Enter)
                return selectedOption;
            else
                selectedOption = input.GetNewSelectionIndex(selectedOption, GetOptionsLength());
        }
    }
}
