namespace CurrencyConverterApp.Converters.Implementation;

public class CurrencyConverter : ICurrencyConverter
{
    private readonly Dictionary<(string, string), decimal> _exchangeRates = new();

    public void AddExchangeRate(string fromCurrency, string toCurrency, decimal rate)
    {
        _exchangeRates[(fromCurrency, toCurrency)] = rate;
        _exchangeRates[(toCurrency, fromCurrency)] = 1 / rate;
    }

    public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
    {
        if (fromCurrency == toCurrency) return amount;

        if (_exchangeRates.ContainsKey((fromCurrency, toCurrency)))
        {
            return amount * _exchangeRates[(fromCurrency, toCurrency)];
        }

        foreach (var intermediary in _exchangeRates.Keys)
        {
            if (intermediary.Item1 != fromCurrency ||
                !_exchangeRates.ContainsKey((intermediary.Item2, toCurrency))) continue;
            var toIntermediary = Convert(amount, fromCurrency, intermediary.Item2);
            return Convert(toIntermediary, intermediary.Item2, toCurrency);
        }
        throw new InvalidOperationException($"No exchange rate found for {fromCurrency} to {toCurrency}");
    }
}