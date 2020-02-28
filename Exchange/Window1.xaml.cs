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
            Task.Run(() => LoadData());
            this.security = security;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(4);
            tbTotal.Text = (counter++).ToString();
        }

        void LoadData()
        {
            MarketLoader marketData = new MarketLoader();
            var mk = marketData.LoadSecuritiesFrom(security.secId);
            Dispatcher.Invoke(() => {
                tbTotal.Text = (counter++).ToString();
                tbName.Text = security.secName;
                tbSecId.Text = security.secId;
                tbValToday.Text = mk[0].valToday.ToString();
                tbLast.Text = mk[0].last.ToString();
                tbValue.Text = mk[0].value.ToString();
                tbVolToday.Text = mk[0].volToday.ToString();
                tbLastChange.Text = mk[0].lastChange.ToString();
                tbTime.Text = mk[0].time.ToString();
            });
            Thread.Sleep(5000);
            Task.Run(() => LoadData());
        }
    }
}
