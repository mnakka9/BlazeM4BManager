using System.Collections.ObjectModel;
using BlazeM4BManager.Domain.Models;
using BlazeM4BManager.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazeM4BManager;

[QueryProperty("Item", "Item")]
public partial class BookView : ContentPage
{
    private readonly IUnitOfWork unitOfWork;

    public BookView(IUnitOfWork _unitOfWork)
    {
        InitializeComponent();
        unitOfWork = _unitOfWork;
    }

    public ObservableCollection<Bookmark> Bookmarks { get; set; } = [];

    protected override void OnAppearing()
    {
        if (Item is not null)
        {
            this.Title = $"{Item.Title} by {Item.AuthorName}";

            if (!string.IsNullOrEmpty(Item.FilePath))
            {
                bookPlayer.Source = Item.FileSource;
                bookPlayer.Play();
            }

            var query = unitOfWork.BookmarkRepository.GetQuery();

            var bookmarks = query.AsNoTracking().Where(b => b.BookId == Item.AudioBookId).ToList();

            if (bookmarks.Count > 0)
            {
                Bookmarks = new ObservableCollection<Bookmark>(bookmarks);
                BookmarksView.ItemsSource = null;
                BookmarksView.ItemsSource = Bookmarks;
            }
        }
        base.OnAppearing();
    }

    public ViewBook Item
    {
        get => (ViewBook)BindingContext;
        set => BindingContext = value;
    }

    private void Rewind_Clicked(object sender, EventArgs e)
    {
        bookPlayer.SeekTo(TimeSpan.FromSeconds(bookPlayer.Position.TotalSeconds - 30));
    }

    private void Forward_Clicked(object sender, EventArgs e)
    {
        bookPlayer.SeekTo(TimeSpan.FromSeconds(bookPlayer.Position.TotalSeconds + 30));
    }

    private async void AddBookMark_Clicked(object sender, EventArgs e)
    {
        var bookmark = new Bookmark
        {
            Title = "Bookmark",
            BookId = Item.AudioBookId,
            Position = bookPlayer.Position.TotalMilliseconds
        };

        bookmark.Comment = bookmark.PositionInString;

        await unitOfWork.BookmarkRepository.AddAsync(bookmark);

        await unitOfWork.SaveChangesAsync();

        await LoadBookmarksAsync();
    }

    private async Task LoadBookmarksAsync()
    {
        var query = unitOfWork.BookmarkRepository.GetQuery();

        var bookmarks = await query.AsNoTracking().Where(b => b.BookId == Item.AudioBookId).ToListAsync();

        if (bookmarks.Count > 0)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Bookmarks = new ObservableCollection<Bookmark>(bookmarks);
                BookmarksView.ItemsSource = null;
                BookmarksView.ItemsSource = Bookmarks;
            });
        }
    }

    private async void ShowBookmarks_Clicked(object sender, EventArgs e)
    {
        if (Bookmarks.Count == 0)
        {
            await Shell.Current.DisplayAlert("No Bookmarks", "No bookmarks found", "OK");

            return;
        }
        mnGrid.IsVisible = !mnGrid.IsVisible;
        BookmarksPanel.IsVisible = !BookmarksPanel.IsVisible;
    }

    private void BookmarksPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Bookmark bookmark)
        {
            bookPlayer.SeekTo(TimeSpan.FromMilliseconds(bookmark.Position));
            mnGrid.IsVisible = true;
            BookmarksPanel.IsVisible = false;
        }
    }

    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        bookPlayer.Handler?.DisconnectHandler();
    }
}