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

        private void ButtonLogin_Clicked(object sender, System.EventArgs e)
        {
            // Обработка логики входа
            // Это может включать проверку учетных данных и навигацию на следующую страницу
            DisplayAlert("Login", "Login button clicked", "OK");
        }

        private void RegistrationLabel_Tapped(object sender, System.EventArgs e)
        {
            // Обработка логики регистрации
            // Это может включать переход к странице регистрации или другие действия
            DisplayAlert("Registration", "Registration Label Clicked", "OK");
        }
    }
}