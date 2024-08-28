namespace CurrencyConverterApp.Converters;

public interface ICurrencyConverter
{
    decimal Convert(decimal amount, string fromCurrency, string toCurrency);
    void AddExchangeRate(string fromCurrency, string toCurrency, decimal rate);
}