using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Xamarin.ViewModels
{
    public class DownloadedSongsViewModel : BaseViewModel
    {
        public ObservableCollection<Track> Songs { get; set; }
        public Command LoadItemsCommand { get; set; }

        public DownloadedSongsViewModel()
        {
            Title = "Your downloaded songs";
            Songs = new ObservableCollection<Track>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy) { return; }
            IsBusy = true;

            // Get downloaded songs
        }
    }
}
