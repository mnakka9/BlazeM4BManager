using BlazeM4BManager.Domain.Models;

namespace BlazeM4BManager.Services.DataServices;

public interface IAudioBookService
{
    Task<List<ViewBook>> GetBooksAsync(string searchTerm = "");

    Task<ViewBook?> GetBookByIdAsync(int id);

    Task<int> AddAudioBooksToDatabase(List<AudioBook> audioBooks);

    Task AddAudioBooks(List<string> files, string? imagePath = null);
}
