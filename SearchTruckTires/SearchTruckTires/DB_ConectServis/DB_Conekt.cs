using SQLite;
using Xamarin.Forms;
using System.IO;
using XAct.IO;
using System.Collections.Generic;
using System;

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
                _ = sQLiteConnectDBTires.CreateTable<ProduktDB>();
            }
        }
        
        public static string GetDatabasePath()
        {
            string databaseName = "DatabaseTires.db3";
            string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);

            return databasePath;
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
