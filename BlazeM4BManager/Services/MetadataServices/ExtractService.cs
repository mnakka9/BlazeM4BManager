using System.Text.Json;
using ATL;
using BlazeM4BManager.Domain.Models;
using Microsoft.Extensions.Logging;

namespace BlazeM4BManager.Services.MetadataServices;

internal class ExtractService(ILogger<ExtractService> logger) : IExtractService
{
    public async Task<AudioBook> GetAudioBookData(FileInfo fileInfo, string? imagePath)
    {
        Track track = new(fileInfo.FullName);

        logger.LogInformation("Extracting meta data from {FileName}", fileInfo.Name);

        string bookName = (track.Album, track.Title) switch
        {
            (var album, _) when !string.IsNullOrEmpty(album) => album,
            (_, var title) when !string.IsNullOrEmpty(title) => title,
            _ => fileInfo.Name
        };

        var authorName = (track.Artist, track.AlbumArtist) switch
        {
            (var artist, _) when !string.IsNullOrEmpty(artist) => artist,
            (_, var albumArtist) when !string.IsNullOrEmpty(albumArtist) => albumArtist,
            _ => string.Empty
        };

        var book = new AudioBook
        {
            Title = bookName,
            Description = track.Comment,
            CreatedAt = DateTime.Now,
            ImagePath = imagePath,
            FilePath = fileInfo.FullName,
            FileSize = fileInfo.Length,
            Duration = track.Duration,
            Genre = track.Genre,
            Narrator = track.Composer,
            ReleaseDate = track.Date
        };

        if (!string.IsNullOrEmpty(authorName))
        {
            book.Author = authorName;
        }
        else
        {
            logger.LogInformation("Author not found for {FileName}", fileInfo.Name);
        }

        if (book.ImagePath is null or { Length: 0 })
        {
            var pictures = track.EmbeddedPictures;

            if (pictures.Count > 0)
            {
                string? imageExtension = pictures[0].MimeType switch
                {
                    "image/jpeg" => ".jpg",
                    "image/png" => ".png",
                    _ => null // Default case if MIME type doesn't match known types
                };

                if (!string.IsNullOrEmpty(imageExtension))
                {
                    string imageFileName = $"{Guid.NewGuid().ToString()}{imageExtension}";
                    string imageFilePath = Path.Combine(fileInfo.Directory!.FullName, imageFileName);


                    if (!File.Exists(imageFilePath))
                    {
                        try
                        {
                            await File.WriteAllBytesAsync(imageFilePath, pictures[0].PictureData);
                            book.ImagePath = imageFilePath;
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "Failed to write image to {FileName}", imageFileName);
                        }
                    }
                }
            }
        }

        logger.LogInformation("Extraction of meta data is done {FileName}", fileInfo.Name);

        return book;
    }
}