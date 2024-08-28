using Microsoft.Extensions.DependencyInjection;
using CurrencyConverterApp.Converters;
using CurrencyConverterApp.Converters.Implementation;
using CurrencyConverterApp.Operations;

namespace CurrencyConverterApp;

public static class ServiceConfigurator
{
    public static IServiceProvider ConfigureServices() 
        => new ServiceCollection()
            .AddSingleton<ICurrencyConverter, CurrencyConverter>()
            .AddSingleton<MoneyOperations>()
            .BuildServiceProvider();
}