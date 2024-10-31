namespace BlazeM4BManager.Domain.Repositories.Interfaces;

public interface IUnitOfWork
{
    IAudioBookRepository AudioBookRepository { get; }
    IViewBookRepository ViewBookRepository { get; }
    IBookmarkRepository BookmarkRepository { get; }
    Task<int> SaveChangesAsync();
}
