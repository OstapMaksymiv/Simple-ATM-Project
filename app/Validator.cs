public class Validator
{
    public static bool IsValidCardNumber(string cardNumber)
    {
        return !string.IsNullOrWhiteSpace(cardNumber);
    }

    public static bool IsValidPin(string pin)
    {
        return !string.IsNullOrWhiteSpace(pin);
    }
}