using System;

public enum CardType
{
    Visa,
    MasterCard,
    AmericanExpress,
    VisaExpress
}

public class Card
{
    public string Number { get; set; }
    public CardType Type { get; set; }
    public string Pin { get; set; }
    public decimal Balance { get; set; }
}