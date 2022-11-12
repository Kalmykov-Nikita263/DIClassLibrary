using DIClassLibrary;
using TestConsoleApplication.Abstractions;
using TestConsoleApplication.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = ApplicationContainer.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddLogging(builder =>
            builder.SetMinimumLevel(LogLevel.Trace).AddSimpleConsole());

        builder.Services.AddTransient<IFooService, FooService>();
        builder.Services.AddTransient<IBarService, BarService>();

        //build application
        var app = builder.Build();


        var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

        logger.LogDebug("Starting application...");
        
        var bar = app.Services.GetService<IBarService>();
        bar.DoSomeRealWork();

        logger.LogDebug("Done!");
    }
}