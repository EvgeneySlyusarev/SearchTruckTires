using SearchTruckTires.Pages;
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
            //NavigationPage navigationPage = new NavigationPage(new SplashPage());
            //Children.Add(navigationPage);
        }
    }
}