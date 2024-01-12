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
            Application.Current.MainPage = new NavigationPage(this);
            LoginPages loginPage = new LoginPages();
            await Navigation.PushModalAsync(loginPage);
        }
    }

}