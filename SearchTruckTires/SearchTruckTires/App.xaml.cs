using Xamarin.Forms;

namespace SearchTruckTires
{
    public partial class App : Application
    {
        public static new App Instance { get { return Current as App; } }

        public enum ELanguage
        {
            English = 0,
            Russian
        };

        public ELanguage Language { get; }

        public App()
        {
            InitializeComponent();

            // Init user preferences
            Language = ELanguage.Russian;

            MainPage = new MainPage();
        }
    }
}
