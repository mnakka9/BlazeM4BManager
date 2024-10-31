using System.Text;
using BlazeM4BManager.Services.DataServices;
using BlazeM4BManager.Services.MetadataServices;
using Microsoft.Extensions.Logging;

namespace BlazeM4BManager;

public static class BlazeExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAudioBookService, AudioBookService>();
        services.AddScoped<IExtractService, ExtractService>();
    }
}

public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, string filePath)
    {
        builder.AddProvider(new FileLoggerProvider(filePath));
        return builder;
    }
}