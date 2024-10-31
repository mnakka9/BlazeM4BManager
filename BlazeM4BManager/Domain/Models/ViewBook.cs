using System.ComponentModel.DataAnnotations.Schema;
using CommunityToolkit.Maui.Views;

namespace BlazeM4BManager.Domain.Models;

public partial class ViewBook
{
    public int? AudioBookId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Duration { get; set; }

    public string? FilePath { get; set; }

    public int? FileSize { get; set; }

    public string? Narrator { get; set; }

    public string? Genre { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string AuthorName { get; set; } = string.Empty;

    [NotMapped]
    public MediaSource? ImageSource
    {
        get
        {
            if (ImagePath is null)
            {
                return MediaSource.FromUri(new Uri("https://angelahaddon.com/wp-content/uploads/2021/01/audiobook_cover.png"));
            }

            return MediaSource.FromFile(ImagePath);
        }
    }

    [NotMapped]
    public MediaSource FileSource => MediaSource.FromFile(FilePath);
}
