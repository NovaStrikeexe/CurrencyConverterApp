using CurrencyConverterApp.Converters;
using CurrencyConverterApp.Models;
using CurrencyConverterApp.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverterApp;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = ServiceConfigurator.ConfigureServices();

        var converter = serviceProvider.GetRequiredService<ICurrencyConverter>();
        var operations = serviceProvider.GetRequiredService<MoneyOperations>();

        Console.WriteLine("Welcome to the Currency Converter App!");
        Console.WriteLine("=======================================");

        SetUpExchangeRates(converter);
        PerformCurrencyOperations(operations, converter);

        Console.WriteLine("Thank you for using the Currency Converter App!");
    }

    private static void SetUpExchangeRates(ICurrencyConverter converter)
    {
        Console.WriteLine("\nLet's set up some exchange rates.");
        while (true)
        {
            var fromCurrency = ReadCurrency("Enter the source currency (e.g., USD):");
            var toCurrency = ReadCurrency("Enter the target currency (e.g., EUR):");
            var rate = ReadExchangeRate($"Enter the exchange rate from {fromCurrency} to {toCurrency} (e.g., 0.85):");

            converter.AddExchangeRate(fromCurrency, toCurrency, rate);
            Console.WriteLine($"Exchange rate from {fromCurrency} to {toCurrency} at {rate} has been added.");

            if (!PromptContinueAdding("Would you like to add another exchange rate? (y/n):"))
            {
                break;
            }
        }
    }

    private static void PerformCurrencyOperations(MoneyOperations operations, ICurrencyConverter converter)
    {
        while (true)
        {
            try
            {
                var money1 = ReadMoney("Enter the first amount:", "Enter the currency of the first amount (e.g., USD):");
                var money2 = ReadMoney("Enter the second amount:", "Enter the currency of the second amount (e.g., EUR):");
                var resultCurrency = ReadCurrency("Enter the currency for the result (e.g., GBP):");

                var operation = ReadOperation("Choose an operation (1 - Add, 2 - Subtract):");

                var result = PerformOperation(operations, money1, money2, resultCurrency, operation);
                Console.WriteLine($"\nResult: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            if (!PromptContinueAdding("Would you like to perform another operation? (y/n):"))
            {
                break;
            }
        }
    }

    private static string ReadCurrency(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine().ToUpper();
    }

    private static decimal ReadExchangeRate(string prompt)
    {
        Console.WriteLine(prompt);
        return decimal.Parse(Console.ReadLine());
    }

    private static Money ReadMoney(string amountPrompt, string currencyPrompt)
    {
        Console.WriteLine(amountPrompt);
        var amount = decimal.Parse(Console.ReadLine());
        var currency = ReadCurrency(currencyPrompt);
        return new Money(amount, currency);
    }

    private static string ReadOperation(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine();
    }

    private static Money PerformOperation(MoneyOperations operations, Money money1, Money money2, string resultCurrency, string operation)
    {
        return operation switch
        {
            "1" => operations.Add(money1, money2, resultCurrency),
            "2" => operations.Subtract(money1, money2, resultCurrency),
            _ => throw new InvalidOperationException("Invalid operation. Please choose 1 for Add or 2 for Subtract.")
        };
    }

    private static bool PromptContinueAdding(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine().ToLower() == "y";
    }
}