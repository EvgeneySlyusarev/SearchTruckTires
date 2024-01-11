using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
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
            LoginPages splashPage = new Pages.LoginPages();
            Application.Current.MainPage = new NavigationPage(this);
            await Navigation.PushModalAsync(splashPage);
            await Task.Delay(5000);
            _ = await Navigation.PopModalAsync();
        }
    }

}