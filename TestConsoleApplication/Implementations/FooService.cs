using TestConsoleApplication.Abstractions;
using Microsoft.Extensions.Logging;

namespace TestConsoleApplication.Implementations
{
    public class FooService : IFooService
    {
        private readonly ILogger<FooService> _logger;

        public FooService(ILoggerFactory loggerFactory) =>
            _logger = loggerFactory.CreateLogger<FooService>();
        
        public void LogSomething(int number) =>
            _logger.LogInformation($"Doing something with number: {number}");
    }
}