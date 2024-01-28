using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Virtual ATM!");

        Console.Write("Enter your card number: ");
        string cardNumber = Console.ReadLine();

        if (Validator.IsValidCardNumber(cardNumber))
        {
            CardDatabase cardDatabase = new CardDatabase("card_info.txt");
            Card card = cardDatabase.GetCard(cardNumber);

            if (card != null)
            {
                Console.Write("Enter your PIN: ");
                string pin = Console.ReadLine();

                if (Validator.IsValidPin(pin))
                {
                    Transaction transaction = new Transaction(card);

                    if (transaction.CheckPin(pin))
                    {
                        Logger.Log("Login successful!");
                        Console.WriteLine($"Your card type: {card.Type}");
                        AccountMenu accountMenu = new AccountMenu(transaction);
                        accountMenu.Show();
                    }
                    else
                    {
                        Logger.Log("Invalid PIN. Access denied.");
                        Console.WriteLine("Invalid PIN. Access denied.");
                    }
                }
                else
                {
                    Logger.Log("Invalid PIN format.");
                    Console.WriteLine("Invalid PIN format.");
                }
            }
            else
            {
                Logger.Log("Card not found in the database.");
                Console.WriteLine("Card not found in the database.");
            }
        }
        else
        {
            Logger.Log("Invalid card number format.");
            Console.WriteLine("Invalid card number format.");
        }
    }
}

