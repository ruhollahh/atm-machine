namespace ConsoleApp3;

public class Card: ICard
{
    public string CardNumber { get; }
    public double Balance { get; set; } = 1_000_000;

    public Card(string cardNumber)
    {
        CardNumber = cardNumber;
    }
    
    public static bool Validate(string cardNumber)
    {
        if (cardNumber.Length != 16)
        {
            throw new InvalidOperationException($"Card Number {cardNumber} is not valid.");
        }

        return true;
    }
}