using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPages : ContentPage
    {
        public LoginPages()
        {
            InitializeComponent();
        }

        private void ButtonLogin_ClickedAsync()
        {
            _ = Navigation.PopModalAsync();
        }

        private async Task RegistrationLabel_TappedAsync()
        {
            RegistrationPage registrationPage = new RegistrationPage();
            await Navigation.PushModalAsync(registrationPage);
        }

        private void ButtonLogin_Clicked(object sender, System.EventArgs e)
        {
           ButtonLogin_ClickedAsync();
        }

        private void RegistrationLabel_Tapped(object sender, System.EventArgs e)
        {
            _ = RegistrationLabel_TappedAsync();
        }
    }
}