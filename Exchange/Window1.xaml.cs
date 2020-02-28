using Exchange.Model;
using Exchange.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using LiveCharts;

namespace Exchange
{
    public partial class Window1 : Window
    {
        bool _closed;
        private readonly Security security;
        public Window1()
        {
            InitializeComponent();
            this.Closed += Window1_Closed;

        }
        private void Window1_Closed(object sender, EventArgs e)
        {
            _closed = true;
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
        }

        void LoadData()
        {
            HistoryLoader history = new HistoryLoader();
            var hs = history.LoadHistory(security.secId, DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd").Replace('.','-'), DateTime.Today.ToString("yyyy-MM-dd").Replace('.', '-'));
            var length = hs.Length;
            MarketLoader marketData = new MarketLoader();
            var mk = marketData.LoadMarket(security.secId);
            Dispatcher.Invoke(() => {
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
