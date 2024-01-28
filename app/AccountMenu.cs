using System;
public class AccountMenu
{
    private readonly Transaction transaction;

    public AccountMenu(Transaction transaction)
    {
        this.transaction = transaction;
    }

    public void Show()
    {
        Console.WriteLine($"Current balance: {transaction.Card.Balance:C}");
        Console.WriteLine("1. Withdraw money");
        Console.WriteLine("2. Deposit money");
        Console.WriteLine("3. Exit");
        Console.Write("Select an option (1/2/3): ");

        if (int.TryParse(Console.ReadLine(), out int option))
        {
            switch (option)
            {
                case 1:
                    transaction.Withdraw();
                    Console.WriteLine("1. Return to the main menu");
                    Console.WriteLine("2. Go out");
                    int op = int.Parse(Console.ReadLine());
                    switch (op)
                    {
                        case 1:
                            Show();
                            break;
                        case 2:
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Wrong choice.");
                            break;
                    }

                    break;
                case 2:
                    transaction.Deposit();
                    Show();

                    break;
                case 3:
                    Logger.Log("User exited the account menu.");
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Please enter a number.");
        }
    }
}