using System;
using Xamarin.Forms;
using Youtube2Mp3.Xamarin.Models;

namespace Youtube2Mp3.Xamarin.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ItemDetailViewModel()
        {
            DeleteCurrentItemCommand = new Command(DeleteCurrentItem, () => !IsBusy);
        }

        public Command DeleteCurrentItemCommand { get; }
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }

        private void DeleteCurrentItem()
        {
            // TODO: Remove this item from user's device.
        }
    }
}
