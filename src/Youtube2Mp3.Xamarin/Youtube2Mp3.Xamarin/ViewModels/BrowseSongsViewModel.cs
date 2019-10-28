using System.Threading.Tasks;
using Xamarin.Forms;

namespace Youtube2Mp3.Xamarin.ViewModels
{
    public class BrowseSongsViewModel : BaseViewModel
    {
        public Command GetSearchResultsCommand { get; set; }
        public string UserInput { get; set; }

        public BrowseSongsViewModel()
        {
            GetSearchResultsCommand = new Command(async () => await GetSearchResults(UserInput), () => !IsBusy);
        }

        private async Task GetSearchResults(string query)
        {
            // Use Youtube2Mp3 library here

            await Task.CompletedTask;
        }
    }
}
