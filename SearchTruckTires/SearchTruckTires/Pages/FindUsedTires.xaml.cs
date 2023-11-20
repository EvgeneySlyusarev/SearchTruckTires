using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTruckTires.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindUsedTires : ContentPage
    {
        public FindUsedTires()
        {
            InitializeComponent();
        }

    //    private List<ProduktDB> BD_Conection()
    //    {
    //        var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "database.db");
    //        var db = new SQLiteConnection(databasePath);
    //        //db.CreateTable<ProduktDB>();

    //        // Получение всех элементов
    //        List<ProduktDB> items = db.Table<>.ToList();
    //        return items;
    //    }
    }
}