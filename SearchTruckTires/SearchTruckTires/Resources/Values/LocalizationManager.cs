using System.Collections.Generic;

namespace SearchTruckTires.Resources.Values
{
    public class LocalizationManager
    {
        public static LocalizationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocalizationManager();
                    _instance._Init();
                }
                return _instance;
            }
        }

        public string Translate(string key)
        {
            //if (_library.TryGetValue(key, out Entry value))
            //{
            //    return value.text;
            //}

            if (_library.ContainsKey(key))
            {
                return _library[key].text;
            }
            return "!!!" + key + "!!!";
        }
        private bool _Init()
        {
            if (App.Instance.Language == App.ELanguage.Russian)
            {
                _AddEntry("basketSize", "В корзине товаров:");
                _AddEntry("costCash", "Сумма заказа НАЛ :");
                _AddEntry("costBank", "Сумма заказа БАНК:");
            }
            else if (App.Instance.Language == App.ELanguage.English)
            {
                _AddEntry("basketSize", "Products in the basket");
                _AddEntry("costCash", "Order price by cash:");
                _AddEntry("costBank",  "Order price by bank:");
            }
            return true;
        }

        private void _AddEntry(string key, string text)
        {
            _library.Add(key, new Entry() { text = text });
        }

        private static LocalizationManager _instance = null;

        private struct Entry
        {
            public string text;
        };
        private readonly Dictionary<string, Entry> _library = new Dictionary<string, Entry>();
    };
}
