namespace AtmMachine;

public class Atm
{
    private Card InsertedCard { get; }

    public Atm(string cardNumber)
    {
        Card.Validate(cardNumber);
        InsertedCard = new Card(cardNumber);
    }

    private void CheckWithdrawal(double amount)
    {
        if (amount > InsertedCard.Balance)
        {
            throw new InvalidOperationException("Insufficient balance");
        }
    }

    private void Withdraw()
    {
        Console.WriteLine("How much do you want to withdraw?");
        var amount = Convert.ToDouble(Console.ReadLine());
        CheckWithdrawal(amount);
        InsertedCard.Balance -= amount;
        Console.WriteLine($"Successfully withdraw {amount}, new balance is {InsertedCard.Balance}");
        RunOperator();
    }
    
    private void Transition()
    {
        Console.WriteLine("What is the card number you want to transfer to?");
        var cardNumber = Console.ReadLine() ?? "";
        Card.Validate(cardNumber);
        Console.WriteLine("How much do you want to transfer?");
        var amount = Convert.ToDouble(Console.ReadLine());
        InsertedCard.Balance -= amount;
        Console.WriteLine($"Successfully transferred {amount}, new balance is {InsertedCard.Balance}");
        RunOperator();
    }
    
    private void BillPayment()
    {
        Console.WriteLine("Which bill do you want to pay?");
        var billOptions = Enum.GetValues(typeof(AtmBillPaymentOption)).Cast<AtmBillPaymentOption>().ToArray();
        for (var i = 0; i < billOptions.Length; ++i)
        {
            Console.WriteLine($"{i + 1}. {billOptions[i]}");
        }

        var chosenBill = (AtmBillPaymentOption) Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the amount of what you're willing to pay:");
        var amount = Convert.ToDouble(Console.ReadLine());
        CheckWithdrawal(amount);
        InsertedCard.Balance -= amount;
        Console.WriteLine($"your {chosenBill} bill was successfully paid, new balance is {InsertedCard.Balance}");
        RunOperator();
    }

    public void RunOperator()
    {
        Console.WriteLine("How can I Help you?");
        var mainOptions = Enum.GetValues(typeof(AtmMainOption)).Cast<AtmMainOption>().ToArray();
        for (var i = 0; i < mainOptions.Length; ++i)
        {
            Console.WriteLine($"{i + 1}. {mainOptions[i]}");
        }

        var chosenOption = (AtmMainOption) Convert.ToInt32(Console.ReadLine());

        try
        {
            HandleResponse(chosenOption);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
            RunOperator();
        }
        
        void HandleResponse(AtmMainOption userResponse)
        {
            switch (userResponse)
            {
                case AtmMainOption.Withdrawal:
                    Withdraw();
                    break;
                case AtmMainOption.Transition:
                    Transition();
                    break;
                case AtmMainOption.BillPayment:
                    BillPayment();
                    break;
                default:
                    throw new InvalidOperationException($"Invalid option: {chosenOption}");
            }
        }
    }
}