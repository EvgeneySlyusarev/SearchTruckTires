using SearchTruckTires.DB_ConectServis;
using SQLite;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPages : ContentPage
    {
        private string _login = null;
        private string _password = null;

        public LoginPages()
        {
            InitializeComponent();
        }

        private bool IsUserAuthenticated()
        {
            using SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath());
            TableQuery<User> filteredUser = from item in sQLiteConnectDBTires.Table<User>()
                                            where item.Login == _login && item.Password == _password
                                            select item;
            return filteredUser.Any();
        }

        protected override bool OnBackButtonPressed()
        {
            OnBackButtonPressedEventHandler();
            return base.OnBackButtonPressed();
        }
        private async void OnBackButtonPressedEventHandler()
        {
            if (!IsUserAuthenticated())
            {
                await DisplayAlert("Error", "The user does not exist or the username and password are incorrect.", "OK");
                return;
            }
            _ = await Navigation.PopModalAsync();
        }
        private void ButtonLogin_Clicked(object sender, System.EventArgs e)
        {
            OnBackButtonPressedEventHandler();
        }

        private async Task RegistrationLabel_TappedAsync()
        {
            RegistrationPage registrationPage = new RegistrationPage();
            await Navigation.PushModalAsync(registrationPage);
        }
        private void RegistrationLabel_Tapped(object sender, System.EventArgs e)
        {
            _ = RegistrationLabel_TappedAsync();
        }

        private void EnteryPassword_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (EnteryPassword != null)
            {
                _password = EnteryPassword.Text;
            }
        }
        private void EnteryLogin_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (EnteryLogin != null)
            {
                _login = EnteryLogin.Text;
            }
        }
    }
}
