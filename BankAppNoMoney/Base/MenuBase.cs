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

    internal virtual int ShowMenu(bool usePosition = false)
    {
        var cursorTop = Console.CursorTop + 1;
        while (true)
        {
            if (!usePosition)
            {
                Console.Clear();
                cursorTop = 0;
            }

            Console.SetCursorPosition(0, cursorTop);
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
