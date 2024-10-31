using Microsoft.Extensions.Logging;

namespace BlazeM4BManager;

[ProviderAlias("FileLogger")]
public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _filePath;

    public FileLoggerProvider(string filePath)
    {
        _filePath = filePath;

        // Ensure the directory exists
        var logDirectory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory!);
        }
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(_filePath, categoryName);
    }

    public void Dispose()
    {
    }
}
