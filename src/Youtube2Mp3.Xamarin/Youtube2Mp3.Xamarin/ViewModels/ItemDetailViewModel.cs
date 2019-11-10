using Xamarin.Forms;
using Youtube2Mp3.Core.Entities;
using Youtube2Mp3.Core.Services;

namespace Youtube2Mp3.Xamarin.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private IThumbnailRepository _thumbnailRepository;

        public ItemDetailViewModel(Track song = null)
        {
            DeleteCurrentItemCommand = new Command(DeleteCurrentItem, () => !IsBusy);

            Title = song?.Title;
            Song = song;
            SongThumbnailUrl = _thumbnailRepository.DownloadThumbnailAndGetFilePathAsync(song).GetAwaiter().GetResult();
        }

        public Command DeleteCurrentItemCommand { get; }
        public Track Song { get; set; }
        public string Artists
        {
            get
            {
                return string.Join(", ", Song.Authors);
            }
        }
        public string SongThumbnailUrl { get; set; } = "default_thumbnail.png";

        private void DeleteCurrentItem()
        {
            // TODO: Remove this item from user's device.
        }
    }
}
