using Xamarin.Forms;

namespace SearchTruckTires
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
            Current.UserAppTheme = OSAppTheme.Dark;
        }
    }
}
