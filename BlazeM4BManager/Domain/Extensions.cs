using BlazeM4BManager.Domain.Models;
using BlazeM4BManager.Domain.Repositories;
using BlazeM4BManager.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazeM4BManager.Domain
{
    public static class Extensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAudioBookRepository, AudioBookRepository>();
            services.AddScoped<IViewBookRepository, ViewBookRepository>();
            services.AddScoped<IBookmarkRepository, BookmarksRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void EnsureDatabaseCreated(this MauiAppBuilder builder)
        {
            builder.Services.AddDbContext<BlazeAppContext>(option =>
                option.UseSqlite("Data Source=blazebooks.db3")
            );

            using var scope = builder.Services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BlazeAppContext>();
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw(
                "DROP VIEW IF EXISTS view_Books"
            );
            context.Database.ExecuteSqlRaw(
                "CREATE VIEW view_Books AS SELECT A.audiobook_id, A.title,A.description,A.image_path,        A.created_at,        A.duration,        A.file_path,        A.file_size,        A.narrator,        A.genre,        A.release_date,        A.author AS author_name   FROM Audiobooks A ORDER BY A.created_at DESC;"
            );
            context.Dispose();
        }
    }
}
