namespace ConsoleApp3;

public interface ICard
{ 
    public string CardNumber { get; }
    public double Balance { get; set; }

    public static bool Validate(string cardNumber)
    {
        if (cardNumber.Length != 16)
        {
            throw new InvalidOperationException($"Card Number {cardNumber} is not valid.");
        }

        return true;
    }
}