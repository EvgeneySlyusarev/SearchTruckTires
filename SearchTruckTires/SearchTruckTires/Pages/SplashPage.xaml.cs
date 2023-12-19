using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            BackgroundImageSource = "@Resources/Drawable/WheelMark3.png";
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(2000);

            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}