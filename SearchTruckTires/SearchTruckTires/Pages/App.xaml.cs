using Xamarin.Forms;

namespace SearchTruckTires
{
    public partial class App : Application
    {
        public static App Instance { get { return Current as App; } }

        public enum ELanguage
        {
            English = 0,
            Russian
        };

        public ELanguage Language { get; }

        public App()
        {
            InitializeComponent();
            Language = ELanguage.English;
            UserAppTheme = OSAppTheme.Dark;
            MainPage = new MainPage();
        }
    }
}
