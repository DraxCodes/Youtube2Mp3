using Xamarin.Forms;
using Youtube2Mp3.Xamarin.Views;

namespace Youtube2Mp3.Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            //Register any Dependencies in here, like above ^^

            // Mainpage is a TabbedContent page (basically NavigationPage)
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
