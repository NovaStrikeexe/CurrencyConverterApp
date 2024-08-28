using CurrencyConverterApp.Converters;
using CurrencyConverterApp.Models;

namespace CurrencyConverterApp.Operations;

public class MoneyOperations(ICurrencyConverter currencyConverter)
{
    public Money Add(Money a, Money b, string resultCurrency)
    {
        var convertedAmountB = currencyConverter.Convert(b.Amount, b.Currency, resultCurrency);
        return new Money(a.Amount + convertedAmountB, resultCurrency);
    }

    public Money Subtract(Money a, Money b, string resultCurrency)
    {
        var convertedAmountB = currencyConverter.Convert(b.Amount, b.Currency, resultCurrency);
        return new Money(a.Amount - convertedAmountB, resultCurrency);
    }
}