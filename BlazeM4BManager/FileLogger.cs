using System.Text;
using Microsoft.Extensions.Logging;

namespace BlazeM4BManager;

public class FileLogger(string filePath, string categoryName) : ILogger
{
    private static readonly object _lock = new();

    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var logRecord = new StringBuilder();
        logRecord.AppendLine($"[{DateTime.Now}] [{logLevel}] [{categoryName}] {formatter(state, exception)}");

        if (exception != null)
        {
            logRecord.AppendLine(exception.ToString());
        }

        lock (_lock)
        {
            File.AppendAllText(filePath, logRecord.ToString());
        }
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }
}
