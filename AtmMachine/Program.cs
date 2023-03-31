namespace ConsoleApp3;

class Program
{
    public static void Main(string[] args)
    {
        Init();
    }

    private static void Init()
    {
        Console.WriteLine("Please enter your card number:");
        var cardNumber = Console.ReadLine() ?? "";
        try
        {
            var atm = new Atm(cardNumber);
            atm.RunOperator();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
            Init();
        }
    }
}