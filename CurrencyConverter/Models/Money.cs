namespace CurrencyConverterApp.Models;

public class Money(decimal amount, string currency)
{
    public decimal Amount { get; private set; } = amount;
    public string Currency { get; private set; } = currency;

    public override string ToString() 
        => $"{Amount} {Currency}";
}