using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Xamarin.ViewModels;

namespace Youtube2Mp3.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchSongsPage : ContentPage
    {
        BrowseSongsViewModel viewModel;

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
            
            /*if (viewModel.Songs.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);*/
        }
    }
}
