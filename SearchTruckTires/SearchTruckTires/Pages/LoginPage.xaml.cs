using SearchTruckTires.DB_ConectServis;
using SQLite;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPages : ContentPage
    {
        private string _login = string.Empty;
        private string _password = string.Empty;

        public LoginPages()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void ButtonLogin_Clicked(object sender, System.EventArgs e)
        {
            OnBackButtonPressedEventHandler();
        }

        private bool ValidLogin()
        {
            if (!string.IsNullOrEmpty(EnteryLogin.Text))
            {
                bool isValidEmail = IsEmailValid(EnteryLogin.Text);
                if (isValidEmail)
                {
                    EnteryLogin.BackgroundColor = Color.AliceBlue;
                    _login = EnteryLogin.Text;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                EnteryLogin.BackgroundColor = Color.Pink;
                return false;
            }
        }
        private bool ValidPassword()
        {
            if (!string.IsNullOrEmpty(EnteryPassword.Text))
            {
                if (IsValidInput(EnteryPassword.Text))
                {
                    EnteryPassword.BackgroundColor = Color.AliceBlue;
                    _password = EnteryPassword.Text;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                EnteryPassword.BackgroundColor = Color.Pink;
                return false;
            }
        }
        private bool IsUserAuthenticated()
        {
            using SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath());
            TableQuery<User> filteredUser = from item in sQLiteConnectDBTires.Table<User>()
                                            where item.Login == _login && item.Password == _password
                                            select item;
            return filteredUser.Any();
        }
        private static bool IsValidInput(string input)
        {
            if (input == null)
            {
                return false;
            }
            string pattern = "^[a-zA-Z0-9!@#$%^&*()_+]+$";
            return Regex.IsMatch(input, pattern);
        }
        private static bool IsEmailValid(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void EnteryLogin_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _login = EnteryLogin.Text;
        }
        private void EnteryPassword_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _password = EnteryPassword.Text;
        }

        private async void RegistrationLabel_TappedAsync()
        {
            RegistrationPage registrationPage = new RegistrationPage();
            await Navigation.PushModalAsync(registrationPage);
        }
        private void RegistrationLabel_Tapped(object sender, EventArgs e)
        {
            RegistrationLabel_TappedAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            OnBackButtonPressedEventHandler();
            return base.OnBackButtonPressed();
        }
        private async void OnBackButtonPressedEventHandler()
        {
            if (!ValidLogin())
            {
                EnteryLogin.Text = null;
                EnteryLogin.BackgroundColor = Color.Pink;
                _ = DisplayAlert("Error", "Login must be a valid email address", "OK");
                return;
            }
            if (!ValidPassword())
            {
                EnteryPassword.BackgroundColor = Color.Pink;
                EnteryPassword.Text = null;
                _ = DisplayAlert("Error", "Please enter your password correctly (a-zA-Z0-9!@#$%^&*()_+])", "OK");
                return;
            }
            if (!IsUserAuthenticated())
            {
                await DisplayAlert("Error", "The user does not exist or the username and password are incorrect.", "OK");
                return;
            }
            _ = await Navigation.PopModalAsync();
        }
    }
}
