using System;
public class Transaction
{
    public Card Card { get; }

    public Transaction(Card card)
    {
        Card = card;
    }

    public bool CheckPin(string pin)
    {
        return Card.Pin == pin;
    }

    public void Withdraw()
    {
        Console.Write("Enter the amount to withdraw: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount))
        {
            if (Card.Balance >= withdrawalAmount)
            {
                Card.Balance -= withdrawalAmount;
                Console.WriteLine($"Withdrawal successful. New balance: {Card.Balance:C}");
                SaveUpdatedBalance();
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount. Please enter a valid number.");
        }
    }

    public void Deposit()
    {
        Console.Write("Enter the amount to deposit: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
        {
            Card.Balance += depositAmount;
            Console.WriteLine($"Deposit successful. New balance: {Card.Balance:C}");
            SaveUpdatedBalance();
        }
        else
        {
            Console.WriteLine("Invalid amount. Please enter a valid number.");
        }
    }

    private void SaveUpdatedBalance()
    {
        CardDatabase cardDatabase = new CardDatabase("card_info.txt");
        cardDatabase.UpdateCard(Card);
    }
}