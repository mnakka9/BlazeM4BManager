using BlazeM4BManager.Domain.Models;
using BlazeM4BManager.Services.DataServices;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;

namespace BlazeM4BManager
{
    public partial class MainPage : ContentPage
    {
        private IAudioBookService AudioBookService { get; }
        private readonly ILogger<MainPage> logger;

        public MainPage(IAudioBookService bookService, ILogger<MainPage> _logger)
        {
            InitializeComponent();
            AudioBookService = bookService;
            logger = _logger;
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            await LoadBooks();
            base.OnNavigatedTo(args);
        }

        private async void Search_Button_Clicked(object sender, EventArgs e)
        {
            await LoadBooks(searchTermEntry.Text);
        }

        private async void AddBooks_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FolderPicker.PickAsync(CancellationToken.None);

                if (result == null)
                {
                    await Shell.Current.DisplayAlert("Error", "No folder selected", "OK");
                }

                var directoryInfo = new DirectoryInfo(result!.Folder!.Path);

                if (directoryInfo.Exists)
                {
                    var files = directoryInfo.GetFiles("*.m4b", SearchOption.AllDirectories).Select(x => x.FullName).ToList();

                    await AudioBookService.AddAudioBooks(files, null);

                    await LoadBooks();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding books");
                await Shell.Current.DisplayAlert("Error", "Error occurred while trying to add books", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                AudioBooksList.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
                {
                    SnapPointsType = SnapPointsType.MandatorySingle,
                    SnapPointsAlignment = SnapPointsAlignment.Start
                };
            }
        }

        private async Task LoadBooks(string searchTerm = "")
        {
            var books = await AudioBookService.GetBooksAsync(searchTerm);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                AudioBooksList.ItemsSource = null;
                AudioBooksList.ItemsSource = books;
            });
        }

        private async void AudioBooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is ViewBook book)
            {
                await Shell.Current.GoToAsync(nameof(BookView), true, new Dictionary<string, object>
                {
                    ["Item"] = book
                });
            }
        }

        private async void SearchTermEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(searchTermEntry.Text))
            {
                await LoadBooks();
            }
        }
    }
}
