using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace SearchTruckTires
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Basket : ContentPage
    { 
        private readonly ObservableCollection<ProduktBasket> produktsBasket = new ObservableCollection<ProduktBasket>();
        public Basket()
        {
            BackgroundImageSource = "@Resources/Drawable/WheelMark2.png";
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Unspecified;
            ListViewBasket.ItemsSource = produktsBasket;
            BindingContext = this;
            ListViewBasket.HasUnevenRows = true;
        }
        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is ProduktTires selectedProdukt)
            {
                bool result = await DisplayAlert("Добавить в корзину: - ", $"{selectedProdukt.Title}", "Да", "Нет");
                await DisplayAlert("Уведомление", "Вы выбрали: " + (result ? "Добавить" : "Отменить"), "OK");
            }
        }
        public void AddToBasket(ProduktTires produkt)
        {
            if (produkt.ObjektToBasket == true)
            {
                produktsBasket.Add(new ProduktBasket{ TitleProduktBasket = produkt.Title, PriseNProduktBasket = produkt.PriseN, PriseBNProduktBasket = produkt.PriseBN, ImageURLProduktBasket = produkt.ImageURL });
            }
        }
    }
}