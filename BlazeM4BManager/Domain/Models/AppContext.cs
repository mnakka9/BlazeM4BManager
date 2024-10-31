using Microsoft.EntityFrameworkCore;

namespace BlazeM4BManager.Domain.Models;

public partial class BlazeAppContext
    : DbContext
{
    public BlazeAppContext()
    {
    }

    public BlazeAppContext(DbContextOptions<BlazeAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AudioBook> AudioBooks { get; set; }

    public virtual DbSet<ViewBook> ViewBooks { get; set; }

    public virtual DbSet<Bookmark> Bookmarks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AudioBook>(entity =>
        {
            entity.Property(e => e.AudioBookId).HasColumnName("audiobook_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.FilePath).HasColumnName("file_path");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.Genre).HasColumnName("genre");
            entity.Property(e => e.Narrator).HasColumnName("narrator");
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("DATE")
                .HasColumnName("release_date");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.ImagePath).HasColumnName("image_path");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Author).HasColumnName("author");

        });

        modelBuilder.Entity<ViewBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_Books");

            entity.Property(e => e.AudioBookId).HasColumnName("audiobook_id");
            entity.Property(e => e.AuthorName).HasColumnName("author_name");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.FilePath).HasColumnName("file_path");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.Genre).HasColumnName("genre");
            entity.Property(e => e.Narrator).HasColumnName("narrator");
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("DATE")
                .HasColumnName("release_date");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.ImagePath).HasColumnName("image_path");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
