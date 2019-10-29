using System;
using System.ComponentModel;
using Xamarin.Forms;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Xamarin.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Track Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Track("", new[] { "" }, 0);

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
