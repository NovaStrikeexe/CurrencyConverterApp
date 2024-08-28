using CurrencyConverterApp.Models;
using CurrencyConverterApp.Operations;

namespace CurrencyConverter.Tests;

public class CurrencyConverterTests
{
    [Fact]
    public void Convert_ShouldReturnSameAmount_WhenCurrencyIsSame()
    {
        var converter = new CurrencyConverterApp.Converters.Implementation.CurrencyConverter();

        var result = converter.Convert(100m, "USD", "USD");

        Assert.Equal(100m, result);
    }

    [Fact]
    public void Convert_ShouldReturnConvertedAmount_WhenExchangeRateExists()
    {
        var converter = new CurrencyConverterApp.Converters.Implementation.CurrencyConverter();
        converter.AddExchangeRate("USD", "EUR", 0.85m);

        var result = converter.Convert(100m, "USD", "EUR");

        Assert.Equal(85m, result);
    }

    [Fact]
    public void Convert_ShouldThrowException_WhenNoExchangeRateExists()
    {
        var converter = new CurrencyConverterApp.Converters.Implementation.CurrencyConverter();

        Assert.Throws<InvalidOperationException>(() => converter.Convert(100m, "USD", "EUR"));
    }

    [Fact]
    public void Convert_ShouldReturnAmountViaCrossRate_WhenDirectRateDoesNotExist()
    {
        var converter = new CurrencyConverterApp.Converters.Implementation.CurrencyConverter();
        converter.AddExchangeRate("USD", "EUR", 0.85m);
        converter.AddExchangeRate("EUR", "GBP", 0.9m);

        var result = converter.Convert(100m, "USD", "GBP");

        Assert.Equal(76.5m, result);
    }
}

public class MoneyOperationsTests
{
    [Fact]
    public void Add_ShouldReturnCorrectSum_WhenCurrenciesAreDifferent()
    {
        var converter = new CurrencyConverterApp.Converters.Implementation.CurrencyConverter();
        converter.AddExchangeRate("USD", "EUR", 0.85m);
        var operations = new MoneyOperations(converter);

        var money1 = new Money(100, "USD");
        var money2 = new Money(100, "EUR");

        var result = operations.Add(money1, money2, "USD");

        Assert.Equal(new Money(217.65m, "USD").ToString(), result.ToString());
    }

    [Fact]
    public void Subtract_ShouldReturnCorrectDifference_WhenCurrenciesAreDifferent()
    {
        var converter = new CurrencyConverterApp.Converters.Implementation.CurrencyConverter();
        converter.AddExchangeRate("USD", "EUR", 0.85m);
        var operations = new MoneyOperations(converter);

        var money1 = new Money(100, "USD");
        var money2 = new Money(100, "EUR");

        var result = operations.Subtract(money1, money2, "USD");

        Assert.Equal(new Money(-17.65m, "USD").ToString(), result.ToString());
    }

    [Fact]
    public void Add_ShouldReturnCorrectSum_WhenCurrenciesAreSame()
    {
        var converter = new CurrencyConverterApp.Converters.Implementation.CurrencyConverter();
        var operations = new MoneyOperations(converter);

        var money1 = new Money(100, "USD");
        var money2 = new Money(50, "USD");

        var result = operations.Add(money1, money2, "USD");

        Assert.Equal(new Money(150m, "USD").ToString(), result.ToString());
    }
}