using Exchange.Model;
using Exchange.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Exchange
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly Security security;
        int counter = 0;

        public Window1()
        {
            InitializeComponent();
        }
        public Window1(Security security)
        {
            InitializeComponent();
            /*DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(5);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();*/
            Task.Run(() => LoadData());
            /*
                        Task.Run(() => LoadData());
                        TradeLoader tradeLoader = new TradeLoader();
                        var trade = tradeLoader.LoadTradesFrom(security.SECID);
                        tbSecId.Text = security.SECID;
                        var total = 0;
                        if (trade != null)
                        {
                            foreach (var item in trade)
                            {
                                item.price = Convert.ToDouble(item.price);
                                item.quanitity = Convert.ToInt32(item.quanitity);
                                item.tradeTime = Convert.ToDateTime(item.tradeTime);
                                total += item.quanitity;
                            }
                        }
                        tbTotal.Text = string.Format("{0}",total);*/
            this.security = security;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(4);
            tbTotal.Text = (counter++).ToString();
            //throw new NotImplementedException();
        }

        void LoadData()
        {
            //разобраться как использовать try catch,в блоке finally вставить Task.Run(() => LoadData());

            //вызвать Loader
            MarketLoader marketData = new MarketLoader();
            var mk = marketData.LoadSecuritiesFrom(security.secId);
            Dispatcher.Invoke(() => {
                //обновлять форму здесь
                tbTotal.Text = (counter++).ToString();
                tbName.Text = security.secName;
                tbSecId.Text = security.secId;
                
            });
            //задержка обновления данных:
            Thread.Sleep(5000);
            Task.Run(() => LoadData());
        }
    }
}
