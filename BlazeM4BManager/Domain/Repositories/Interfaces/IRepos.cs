using BlazeM4BManager.Domain.Models;

namespace BlazeM4BManager.Domain.Repositories.Interfaces;

public interface IAudioBookRepository : IRepository<AudioBook>;

public interface IViewBookRepository : IRepository<ViewBook>;
public interface IBookmarkRepository : IRepository<Bookmark>;
