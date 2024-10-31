using BlazeM4BManager.Domain.Models;
using BlazeM4BManager.Domain.Repositories.Interfaces;
using BlazeM4BManager.Services.MetadataServices;
using Microsoft.EntityFrameworkCore;

namespace BlazeM4BManager.Services.DataServices;

public class AudioBookService
    (IUnitOfWork unitOfWork,
    IExtractService extractService
    ) : IAudioBookService
{
    public async Task<int> AddAudioBooks(List<AudioBook> audioBooks)
    {
        if (audioBooks is null or { Count: 0 })
        {
            throw new InvalidDataException("No audio books to add");
        }

        if (audioBooks.Any(x => !x.IsValid()))
        {
            throw new InvalidDataException("One or more audio books are invalid");
        }

        await unitOfWork.AudioBookRepository.AddRangeAsync(audioBooks);

        return await unitOfWork.SaveChangesAsync();
    }

    public async Task<ViewBook?> GetBookByIdAsync(int id)
    {
        return await unitOfWork.ViewBookRepository.GetByIdAsync(id);
    }

    public async Task<List<ViewBook>> GetBooksAsync(string searchTerm = "")
    {
        var query = unitOfWork.ViewBookRepository.GetQuery();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(x => x.Title!.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                                || x.AuthorName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task AddAudioBooks(List<string> files, string? imagePath = null)
    {
        List<AudioBook> audioBooks = [];

        foreach (var file in files)
        {
            var book = await extractService.GetAudioBookData(new FileInfo(file), imagePath);

            audioBooks.Add(book);
        }

        if (audioBooks.Count == 0 || audioBooks.Any(x => !x.IsValid()))
        {
            throw new InvalidDataException("One or more audio books are invalid");
        }

        await unitOfWork.AudioBookRepository.AddRangeAsync(audioBooks);

        await unitOfWork.SaveChangesAsync();
    }
}
