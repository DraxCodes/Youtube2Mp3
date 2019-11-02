using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Xamarin.ViewModels;

namespace Youtube2Mp3.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchSongsPage : ContentPage
    {
        SearchSongsViewModel viewModel;

        public SearchSongsPage()
        {
            InitializeComponent();
        }
        
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Track song)) { return; }

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(song)));

            // Manually deselect item.
            SearchResults.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!string.IsNullOrEmpty(viewModel.UserInput))
            {
                viewModel.GetSearchResultsCommand.Execute(null);
            }
        }

        private void Entry_Completed(object sender, System.EventArgs e)
            => viewModel.GetSearchResultsCommand.Execute(null);
    }
}
