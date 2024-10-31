using BlazeM4BManager.Domain.Models;
using BlazeM4BManager.Domain.Repositories.Interfaces;

namespace BlazeM4BManager.Domain.Repositories;

public class UnitOfWork(
    IAudioBookRepository audioBookRepository,
    IViewBookRepository viewBookRepository,
    IBookmarkRepository bookmarkRepository,
    BlazeAppContext context
) : IUnitOfWork
{
    public IAudioBookRepository AudioBookRepository => audioBookRepository;

    public IViewBookRepository ViewBookRepository => viewBookRepository;

    public IBookmarkRepository BookmarkRepository => bookmarkRepository;

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}
