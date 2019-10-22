using System.ComponentModel;
using Xamarin.Forms;
using Youtube2Mp3.Xamarin.Models;
using Youtube2Mp3.Xamarin.ViewModels;

namespace Youtube2Mp3.Xamarin.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        private ItemDetailViewModel _viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }
        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Placeholder text",
                Description = "If you see this, something probably went wrong."
            };

            _viewModel = new ItemDetailViewModel(item);
            BindingContext = _viewModel;
        }
    }
}
