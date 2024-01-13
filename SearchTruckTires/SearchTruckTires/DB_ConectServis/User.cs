
using SQLite;

namespace SearchTruckTires.DB_ConectServis
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User(string firstName, string secondName, string login, string pasword)
        {
            FirstName = firstName;
            SecondName = secondName;
            Login = login;
            Password = pasword;
        }
        public User()
        {
        }
    }
}

