using SearchTruckTires.DB_ConectServis;
using SQLite;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private string _firstName = null;
        private string _secondName = null;
        private string _login = null;
        private string _password = null;
        private string _repeatPassword = null;

        private async Task<bool> UserTableFindAndCreate(string login)
        {
            try
            {
                string databasePath = DB_Conekt.GetDatabasePath();
                if (!File.Exists(databasePath))
                {
                    using SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(databasePath);
                    _ = await Task.Run(() => sQLiteConnectDBTires.CreateTable<User>());
                }

                using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(databasePath))
                {
                    System.Collections.Generic.List<SQLiteConnection.ColumnInfo> tableInfo = sQLiteConnectDBTires.GetTableInfo("User");
                    if (tableInfo.Count == 0)
                    {
                        _ = await Task.Run(() => sQLiteConnectDBTires.CreateTable<User>());
                    }

                    User existingUser = await Task.Run(() =>
                        sQLiteConnectDBTires.Table<User>().FirstOrDefault(u => u.Login == login));

                    return existingUser != null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserExistsAsync: {ex.Message}");
                return false;
            }
        }


        private async void Registration_Clicked(object sender, EventArgs e)
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
            if (UserIsRegistered())
            {
                EnteryLogin.Text = null;
                EnteryPassword.BackgroundColor = Color.Pink;
                await DisplayAlert("Error", "A user with this login exists", "OK");
                return;
            }
            await Registration_ClickedAsync();
        }
        private async Task Registration_ClickedAsync()
        {
            if (ValidateUserData())
            {
                if (await UserTableFindAndCreate(_login))
                {
                    SetErrorState();
                    await DisplayAlert("Error!", "User with this login already exists.", "Ok");
                }
                else
                {
                    using (SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath()))
                    {
                        _ = sQLiteConnectDBTires.CreateTable<User>();
                        User newUser = new User(_firstName, _secondName, _login, _password);
                        _ = sQLiteConnectDBTires.Insert(newUser);
                    }

                    await DisplayAlert("Notification", "Data successfully saved to the database.", "OK");
                    _ = await Navigation.PopModalAsync();
                }
            }
            else
            {
                SetErrorState();
                await DisplayAlert("Error!", "Please enter correct details!", "Ok");
            }
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
                EnteryPassword.BackgroundColor = Color.Pink;
                return false;
            }
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
        private bool UserIsRegistered()
        {
            using SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(DB_Conekt.GetDatabasePath());
            TableQuery<User> filteredUser = from item in sQLiteConnectDBTires.Table<User>()
                                            where item.Login == _login
                                            select item;
            return filteredUser.Any();
        }
        private bool ValidateUserData()
        {
            return _password == _repeatPassword && _firstName != null && _secondName != null && _login != null;
        }
        private static bool IsValidInput(string input)
        {
            if (input == null)
            {
                return false;
            }
            string pattern = "^[a-zA-Z0-9!@#$%^&*()_+.-]+$";
            return Regex.IsMatch(input, pattern);
        }

        private void SetErrorState()
        {
            EnteryPassword.BackgroundColor = Color.Red;
            EnteryRepeatPassword.BackgroundColor = Color.Red;
            _password = null;
            _repeatPassword = null;
        }

        private void EnteryFirstName_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _firstName = EnteryFirstName.Text;
        }
        private void EnterySecondName_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _secondName = EnterySecondName.Text;
        }
        private void EnteryLogin_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _login = EnteryLogin.Text;
        }
        private void EnteryPassword_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _password = EnteryPassword.Text;
        }
        private void EnteryRepeatPassword_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _repeatPassword = EnteryRepeatPassword.Text;
        }
    }
}