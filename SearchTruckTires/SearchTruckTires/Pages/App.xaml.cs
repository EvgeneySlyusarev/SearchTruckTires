using SearchTruckTires.Pages;
using Xamarin.Forms;

namespace SearchTruckTires
{
    public partial class App : Application
    {
        public static App Instance => Current as App;

        public App()
        {
            InitializeComponent();
            UserAppTheme = OSAppTheme.Dark;
            MainPage = new MainPage();
        }
    }
}
