namespace BlazeM4BManager.Domain.Models;

public partial class AudioBook
{
    public int AudioBookId { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Narrator { get; set; }

    public string? Genre { get; set; }

    public string FilePath { get; set; } = null!;

    public long? FileSize { get; set; }

    public int? Duration { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string? Author { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(FilePath);
    }
}
