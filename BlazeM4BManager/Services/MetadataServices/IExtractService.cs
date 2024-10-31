using BlazeM4BManager.Domain.Models;

namespace BlazeM4BManager.Services.MetadataServices;

public interface IExtractService
{
    Task<AudioBook> GetAudioBookData(FileInfo fileInfo, string? imagePath);
}
