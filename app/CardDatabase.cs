using System;
using System.Collections.Generic;
using System.IO;

public class CardDatabase
{
    private readonly List<Card> cards;

    public CardDatabase(string filePath)
    {
        cards = LoadCardsFromFile(filePath);
    }

    private List<Card> LoadCardsFromFile(string filePath)
    {
        List<Card> loadedCards = new List<Card>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                if (values.Length == 4)
                {
                    Card card = new Card
                    {
                        Number = values[0],
                        Type = Enum.TryParse<CardType>(values[1], out var cardType) ? cardType : CardType.Visa,
                        Pin = values[2],
                        Balance = decimal.Parse(values[3])
                    };

                    loadedCards.Add(card);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading cards from file: {ex.Message}");
        }

        return loadedCards;
    }

    public Card GetCard(string cardNumber)
    {
        return cards.Find(card => card.Number == cardNumber);
    }

    public void UpdateCard(Card updatedCard)
    {
        Card existingCard = cards.Find(card => card.Number == updatedCard.Number);
        if (existingCard != null)
        {
            existingCard.Type = updatedCard.Type;
            existingCard.Pin = updatedCard.Pin;
            existingCard.Balance = updatedCard.Balance;

            SaveCardsToFile("card_info.txt");
        }
    }

    private void SaveCardsToFile(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Card card in cards)
                {
                    writer.WriteLine($"{card.Number};{card.Type};{card.Pin};{card.Balance}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving cards to file: {ex.Message}");
        }
    }
}