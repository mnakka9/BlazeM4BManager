using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazeM4BManager.Domain.Models;

public class Bookmark
{
    [Key]
    public int Id { get; set; }

    public int? BookId { get; set; }

    public double Position { get; set; }

    [NotMapped]
    public string PositionInString => TimeSpan.FromMilliseconds(Position).ToString(@"hh\:mm\:ss");

    public required string Title { get; set; }

    public string? Comment { get; set; }
}
