using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;

namespace Youtube2Mp3.Xamarin.ViewModels
{
    public class SearchSongsViewModel : BaseViewModel
    {
        private IStreamRepository _streamRepository;

        public SearchSongsViewModel(IStreamRepository streamRepository)
        {
            _streamRepository = streamRepository;
        }

        public Command GetSearchResultsCommand { get; set; }
        public string UserInput { get; set; }
        public IEnumerable<ITrack> Songs
        {
            get => GetSearchResults(UserInput).GetAwaiter().GetResult();
        }

        public SearchSongsViewModel()
        {
            GetSearchResultsCommand = new Command(async () => await GetSearchResults(UserInput), () => !IsBusy);
        }

        private async Task<IEnumerable<ITrack>> GetSearchResults(string query)
            => await _streamRepository.SearchAsync(query);
    }
}
