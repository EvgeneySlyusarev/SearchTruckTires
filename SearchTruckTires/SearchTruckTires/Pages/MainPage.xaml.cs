using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            ShowSplashPage();
        }

        private async void ShowSplashPage()
        {
            Pages.SplashPage splashPage = new Pages.SplashPage();
            Application.Current.MainPage = new NavigationPage(this);
            await Navigation.PushModalAsync(splashPage);
            await Task.Delay(5000);
           // _ = await splashPage.FadeTo(0, 1000);
            _ = await Navigation.PopModalAsync();
        }
    }

}