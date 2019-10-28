using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Youtube2Mp3.Xamarin.Models;
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
            if (!(args.SelectedItem is Item item)) { return; }

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            SearchResults.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}