using Xamarin.Forms;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Xamarin.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ItemDetailViewModel()
        {
            DeleteCurrentItemCommand = new Command(DeleteCurrentItem, () => !IsBusy);
        }

        public Command DeleteCurrentItemCommand { get; }
        public Track Song { get; set; }
        public ItemDetailViewModel(Track song = null)
        {
            Title = song?.Title;
            Song = song;
        }

        private void DeleteCurrentItem()
        {
            // TODO: Remove this item from user's device.
        }
    }
}
