using BlazeM4BManager.Domain;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazeM4BManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Logging.AddFileLogger(Path.Combine(FileSystem.AppDataDirectory, "app.log"));

            builder.Services.AddSingleton(FolderPicker.Default);

            builder.Services.RegisterRepositories();

            builder.EnsureDatabaseCreated();

            builder.Services.AddServices();

            builder.Services.TryAddTransient<MainPage>();
            builder.Services.TryAddTransient<BookView>();

            return builder.Build();
        }
    }
}
