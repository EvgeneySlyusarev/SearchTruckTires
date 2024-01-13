using SQLite;
using System;
using System.IO;

namespace SearchTruckTires.DB_ConectServis
{
    public class DB_Conekt
    {
        public DB_Conekt()
        {
            bool databaseExists = File.Exists(GetDatabasePath());
            if (!databaseExists)
            {
                SQLiteConnection sQLiteConnectDBTires = new SQLiteConnection(GetDatabasePath());
                _ = sQLiteConnectDBTires.CreateTable<Product>();
            }
        }

        public static string GetDatabasePath()
        {
            string databaseName = "DatabaseTires.db3";
            string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);

            return databasePath;


            //// Для фотографий
            //string photoPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;

            //// Для базы данных
            //string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "your_app_name", "db_name.db");
            ///data/user/0/com.companyname.searchtrucktires/files/DatabaseTires.db3
        }
        public static SQLiteConnection GetConnection()
        {
            // Створити підключення до бази даних SQLite
            string databasePath = GetDatabasePath();
            var connection = new SQLiteConnection(databasePath);

            // Повернути об'єкт підключення
            return connection;
        }
    }
}
