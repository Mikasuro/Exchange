
using Exchange.Model;
using Exchange.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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


namespace Exchange
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //todo: добавить коллекцию для хранения всех акций, для использования в качестве основы для запроса
    
        private List<Security> _securities;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Security> Securities
        {
            get => _securities;
            set
            {
                _securities = value;
                OnPropertyChanged();
            }
        }
        
        void OnPropertyChanged([CallerMemberName] string propName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void LoadSecuritys()
        {
            SecutirysLoader secutirysLoader = new SecutirysLoader();
            Securities = secutirysLoader.LoadSecutiry()
                .ToList();
        }
        public void LoadPrices()
        {
            PriceLoader priceloader = new PriceLoader();
            var prices = priceloader.LoadPrice()
                .GroupBy(cp => cp.secId, cp => cp)
                .Select( gr => gr.OrderBy( price=> price.tradeTime).Last());
        }

            public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Thread threadSecuritys = new Thread(new ThreadStart(LoadSecuritys));
            threadSecuritys.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1((sender as Button).DataContext as Security);
            window.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var i = search.Text.IndexOf(search.Text);
            var result = Securities
                .Where(s => s.secName.Contains(search.Text))
                .ToList();
        }
    }
}
