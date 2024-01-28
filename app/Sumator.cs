// using System;
// using System.Collections.Generic;
// using System.IO;

// public enum CardType
// {
//     Visa,
//     MasterCard,
//     AmericanExpress,
//     VisaExpress
// }

// public class Card
// {
//     public string Number { get; set; }
//     public CardType Type { get; set; }
//     public string Pin { get; set; }
//     public decimal Balance { get; set; }
// }

// public class CardDatabase
// {
//     private readonly List<Card> cards;

//     public CardDatabase(string filePath)
//     {
//         cards = LoadCardsFromFile(filePath);
//     }

//     private List<Card> LoadCardsFromFile(string filePath)
//     {
//         List<Card> loadedCards = new List<Card>();

//         try
//         {
//             string[] lines = File.ReadAllLines(filePath);

//             foreach (string line in lines)
//             {
//                 string[] values = line.Split(';');
//                 if (values.Length == 4)
//                 {
//                     Card card = new Card
//                     {
//                         Number = values[0],
//                         Type = Enum.TryParse<CardType>(values[1], out var cardType) ? cardType : CardType.Visa,
//                         Pin = values[2],
//                         Balance = decimal.Parse(values[3])
//                     };

//                     loadedCards.Add(card);
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Error loading cards from file: {ex.Message}");
//         }

//         return loadedCards;
//     }

//     public Card GetCard(string cardNumber)
//     {
//         return cards.Find(card => card.Number == cardNumber);
//     }

//     public void UpdateCard(Card updatedCard)
//     {
//         Card existingCard = cards.Find(card => card.Number == updatedCard.Number);
//         if (existingCard != null)
//         {
//             existingCard.Type = updatedCard.Type;
//             existingCard.Pin = updatedCard.Pin;
//             existingCard.Balance = updatedCard.Balance;

//             SaveCardsToFile("card_info.txt");
//         }
//     }

//     private void SaveCardsToFile(string filePath)
//     {
//         try
//         {
//             using (StreamWriter writer = new StreamWriter(filePath))
//             {
//                 foreach (Card card in cards)
//                 {
//                     writer.WriteLine($"{card.Number};{card.Type};{card.Pin};{card.Balance}");
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Error saving cards to file: {ex.Message}");
//         }
//     }
// }


// public class Transaction
// {
//     public Card Card { get; }

//     public Transaction(Card card)
//     {
//         Card = card;
//     }

//     public bool CheckPin(string pin)
//     {
//         return Card.Pin == pin;
//     }

//     public void Withdraw()
//     {
//         Console.Write("Enter the amount to withdraw: ");
//         if (decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount))
//         {
//             if (Card.Balance >= withdrawalAmount)
//             {
//                 Card.Balance -= withdrawalAmount;
//                 Console.WriteLine($"Withdrawal successful. New balance: {Card.Balance:C}");
//                 SaveUpdatedBalance();
//             }
//             else
//             {
//                 Console.WriteLine("Insufficient funds.");
//             }
//         }
//         else
//         {
//             Console.WriteLine("Invalid amount. Please enter a valid number.");
//         }
//     }

//     public void Deposit()
//     {
//         Console.Write("Enter the amount to deposit: ");
//         if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
//         {
//             Card.Balance += depositAmount;
//             Console.WriteLine($"Deposit successful. New balance: {Card.Balance:C}");
//             SaveUpdatedBalance();
//         }
//         else
//         {
//             Console.WriteLine("Invalid amount. Please enter a valid number.");
//         }
//     }

//     private void SaveUpdatedBalance()
//     {
//         CardDatabase cardDatabase = new CardDatabase("card_info.txt");
//         cardDatabase.UpdateCard(Card);
//     }
// }

// public class Logger
// {
//     public static void Log(string message)
//     {
//         Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
//     }
// }

// public class Validator
// {
//     public static bool IsValidCardNumber(string cardNumber)
//     {
//         return !string.IsNullOrWhiteSpace(cardNumber);
//     }

//     public static bool IsValidPin(string pin)
//     {
//         return !string.IsNullOrWhiteSpace(pin);
//     }
// }

// public class AccountMenu
// {
//     private readonly Transaction transaction;

//     public AccountMenu(Transaction transaction)
//     {
//         this.transaction = transaction;
//     }

//     public void Show()
//     {
//         Console.WriteLine($"Current balance: {transaction.Card.Balance:C}");
//         Console.WriteLine("1. Withdraw money");
//         Console.WriteLine("2. Deposit money");
//         Console.WriteLine("3. Exit");
//         Console.Write("Select an option (1/2/3): ");

//         if (int.TryParse(Console.ReadLine(), out int option))
//         {
//             switch (option)
//             {
//                 case 1:
//                     transaction.Withdraw();
//                     Console.WriteLine("1. Return to the main menu");
//                     Console.WriteLine("2. Go out");
//                     int op = int.Parse(Console.ReadLine());
//                     switch (op)
//                     {
//                         case 1:
//                             Show();
//                             break;
//                         case 2:
//                             Console.WriteLine("Goodbye!");
//                             break;
//                         default:
//                             Console.WriteLine("Wrong choice.");
//                             break;
//                     }

//                     break;
//                 case 2:
//                     transaction.Deposit();
//                     Show();

//                     break;
//                 case 3:
//                     Logger.Log("User exited the account menu.");
//                     Console.WriteLine("Goodbye!");
//                     break;
//                 default:
//                     Console.WriteLine("Invalid choice.");
//                     break;
//             }
//         }
//         else
//         {
//             Console.WriteLine("Invalid choice. Please enter a number.");
//         }
//     }
// }

// public class Program
// {
//     static void Main()
//     {
//         Console.WriteLine("Welcome to the Virtual ATM!");

//         Console.Write("Enter your card number: ");
//         string cardNumber = Console.ReadLine();

//         if (Validator.IsValidCardNumber(cardNumber))
//         {
//             CardDatabase cardDatabase = new CardDatabase("card_info.txt");
//             Card card = cardDatabase.GetCard(cardNumber);

//             if (card != null)
//             {
//                 Console.Write("Enter your PIN: ");
//                 string pin = Console.ReadLine();

//                 if (Validator.IsValidPin(pin))
//                 {
//                     Transaction transaction = new Transaction(card);

//                     if (transaction.CheckPin(pin))
//                     {
//                         Logger.Log("Login successful!");
//                         Console.WriteLine($"Your card type: {card.Type}");
//                         FileSystemWatcher watcher = new FileSystemWatcher();
//                         watcher.Path = Environment.CurrentDirectory;
//                         watcher.Filter = "card_info.txt";
//                         watcher.NotifyFilter = NotifyFilters.LastWrite;
//                         watcher.EnableRaisingEvents = true;
//                         AccountMenu accountMenu = new AccountMenu(transaction);
//                         accountMenu.Show();
//                         watcher.EnableRaisingEvents = false;
//                     }
//                     else
//                     {
//                         Logger.Log("Invalid PIN. Access denied.");
//                         Console.WriteLine("Invalid PIN. Access denied.");
//                     }
//                 }
//                 else
//                 {
//                     Logger.Log("Invalid PIN format.");
//                     Console.WriteLine("Invalid PIN format.");
//                 }
//             }
//             else
//             {
//                 Logger.Log("Card not found in the database.");
//                 Console.WriteLine("Card not found in the database.");
//             }
//         }
//         else
//         {
//             Logger.Log("Invalid card number format.");
//             Console.WriteLine("Invalid card number format.");
//         }
//     }
// }