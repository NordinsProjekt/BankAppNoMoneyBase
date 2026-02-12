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
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write(options[i] + "\n");
            }

            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (--selectedOption < 0)
                    {
                        selectedOption = options.Length - 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (++selectedOption >= options.Length)
                    {
                        selectedOption = 0;
                    }
                    break;
                case ConsoleKey.Enter:
                    return selectedOption;
                default: break;
            }
        }
    }
}
