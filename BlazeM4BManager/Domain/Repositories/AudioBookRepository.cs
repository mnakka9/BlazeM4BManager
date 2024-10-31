using BlazeM4BManager.Domain.Models;
using BlazeM4BManager.Domain.Repositories.Interfaces;

namespace BlazeM4BManager.Domain.Repositories;

public class AudioBookRepository(BlazeAppContext context) : Repository<AudioBook>(context), IAudioBookRepository;
public class ViewBookRepository(BlazeAppContext context) : Repository<ViewBook>(context), IViewBookRepository;
public class BookmarksRepository(BlazeAppContext context) : Repository<Bookmark>(context), IBookmarkRepository;