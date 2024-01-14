using SearchTruckTires.Pages;
using System;
using System.Linq;
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

        protected override void OnStart()
        {
            base.OnStart();

            SplashPage splashPage = new SplashPage();
            MainPage.Navigation.PushModalAsync(splashPage);

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>//splashPage (seconds)
            {
                if (MainPage.Navigation.ModalStack.Contains(splashPage))
                {
                    MainPage.Navigation.PopModalAsync();
                }
                return false;
            });
        }
    }
}
