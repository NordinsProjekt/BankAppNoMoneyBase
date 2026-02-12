namespace BankAppNoMoney;

internal class Program
{
    static void Main(string[] args)
    {
        var b = new Bank();

        while (true)
        {
            Console.WriteLine("Meny");
            Console.WriteLine("1. Skapa konto");
            Console.WriteLine("2. Ta bort konto");
            Console.WriteLine("3. Visa konton");
            Console.WriteLine("4: Hantera konto");

            var input = Console.ReadLine();
        }


    }
}
