using SearchTruckTires.DB_ConectServis;
using SQLite;
using System;
using System.IO;
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

        private void EnteryFirstName_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (EnteryFirstName.Text != null)
            {
                if (IsValidInput(_firstName))
                {
                    EnteryFirstName.BackgroundColor = Color.AliceBlue;
                    _firstName = EnteryFirstName.Text;
                }
                else
                {
                    EnteryFirstName.BackgroundColor = Color.Pink;
                    EnteryFirstName.Text = null;
                    _ = DisplayAlert("Error", "Please enter your name correctly (a-zA-Z0-9!@#$%^&*()_+]+$)", "OK");
                }
            }
            else
            {
                EnteryFirstName.BackgroundColor = Color.Pink;
            }
        }
        private void EnterySecondName_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (EnterySecondName.Text != null)
            {
                if (IsValidInput(_secondName))
                {
                    EnterySecondName.BackgroundColor = Color.AliceBlue;
                    _secondName = EnterySecondName.Text;
                }
                else
                {
                    EnterySecondName.BackgroundColor = Color.Pink;
                    EnterySecondName.Text = null;
                    _ = DisplayAlert("Error", "Please enter your secondname correctly (a-zA-Z0-9!@#$%^&*()_+]+$)", "OK");
                }
            }
            else
            {
                EnterySecondName.BackgroundColor = Color.Pink;
            }
        }
        private void EnteryLogin_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (EnteryLogin.Text != null)
            {
                if (IsValidInput(_login))
                {
                    EnteryLogin.BackgroundColor = Color.AliceBlue;
                    _login = EnteryLogin.Text;
                }
                else
                {
                    EnteryLogin.BackgroundColor = Color.Pink;
                    EnteryLogin.Text = null;
                    _ = DisplayAlert("Error", "Login must be a valid email address", "OK");
                }
            }
            else
            {
                EnteryLogin.BackgroundColor = Color.Pink;
            }
        }
        private void EnteryPassword_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (EnteryPassword.Text != null)
            {
                if (IsValidInput(_password))
                {
                    EnteryPassword.BackgroundColor = Color.AliceBlue;
                    _password = EnteryPassword.Text;
                }
                else
                {
                    EnteryPassword.BackgroundColor = Color.Pink;
                    EnteryPassword.Text = null;
                    _ = DisplayAlert("Error", "Please enter your password correctly (a-zA-Z0-9!@#$%^&*()_+]+$)", "OK");
                }
            }
            else
            {
                EnteryPassword.BackgroundColor = Color.Pink;
            }
        }
        private void RepeatPassword_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (EnteryRepeatPassword.Text != null)
            {
                if (IsValidInput(_repeatPassword))
                {
                    EnteryRepeatPassword.BackgroundColor = Color.AliceBlue;
                    _repeatPassword = EnteryRepeatPassword.Text;
                }
                else
                {
                    EnteryRepeatPassword.BackgroundColor = Color.Pink;
                    EnteryRepeatPassword.Text = null;
                    _ = DisplayAlert("Error", "Please enter your RepeatPassword correctly (a-zA-Z0-9!@#$%^&*()_+]+$)", "OK");
                }
            }
            else
            {
                EnteryRepeatPassword.BackgroundColor = Color.Pink;
            }
        }

        private async Task<bool> UserExistsAsync(string login)
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
        private bool ValidateUserData()
        {
            return _password == _repeatPassword && _firstName != null && _secondName != null && _login != null;
        }

        private static bool IsValidInput(string input)
        {
            string pattern = "^[a-zA-Z0-9!@#$%^&*()_+]+$";
            return Regex.IsMatch(input, pattern);
        }
        private void SetErrorState()
        {
            EnteryPassword.BackgroundColor = Color.Red;
            EnteryRepeatPassword.BackgroundColor = Color.Red;
            _password = null;
            _repeatPassword = null;
        }
        private async void Registration_Clicked(object sender, EventArgs e)
        {
            await Registration_ClickedAsync();
        }
        private async Task Registration_ClickedAsync()
        {
            if (ValidateUserData())
            {
                if (await UserExistsAsync(_login))
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
    }
}