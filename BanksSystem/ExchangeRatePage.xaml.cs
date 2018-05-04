using JsonFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BanksSystem
{
    /// <summary>
    /// Логика взаимодействия для ExchangeRatePage.xaml
    /// </summary>
    public partial class ExchangeRatePage : Page
    {
        private Window window;
        private User user;
        public static ObservableCollection<ValuteInfo> valutes { get; set; }
        public ExchangeRatePage(Window win,User use)
        {
            InitializeComponent();
            window = win;
            user = use;
            Thread threadFirst = new Thread(new ThreadStart(GetValueJson));
            threadFirst.Start();
            threadFirst.Join();
            dataValutes.ItemsSource = valutes;
        }

        public static void GetValueJson()
        {
            string valueJson;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                valueJson = webClient.DownloadString("https://www.cbr-xml-daily.ru/daily_json.js");
            }
            RootObject obj = JsonConvert.DeserializeObject<RootObject>(valueJson);
            valutes = new ObservableCollection<ValuteInfo>();
            valutes.Add(obj.Valute.CZK);
            valutes.Add(obj.Valute.EUR);
            valutes.Add(obj.Valute.JPY);
            valutes.Add(obj.Valute.KZT);
            valutes.Add(obj.Valute.USD);

            foreach(var a in valutes)
            {
                a.Value = a.Value / a.Nominal * 5.2357;
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            window.Content = new MainPage(user, window);
        }
    }
}
