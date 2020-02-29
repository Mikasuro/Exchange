using Exchange.Model;
using Exchange.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace Exchange
{
    public partial class Window1 : Window, INotifyPropertyChanged
    {
        bool _closed;
        private string[] _labels;
        private readonly Security _security;

        public event PropertyChangedEventHandler PropertyChanged;

        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();

        public string[] Labels { get => _labels;
            set
            {
                _labels = value;
                OnPropertyChanged();
            }
        }

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
            DataContext = this;
            _security = security;
            Closed += Window1_Closed;
            Task.Run(() => LoadData());
            Task.Run(() => LoadHistory());
        }

        void LoadHistory()
        {
            HistoryLoader history = new HistoryLoader();
            var hs = history.LoadHistory(_security.secId, DateTime.Today.AddDays(-60).ToString("yyyy-MM-dd").Replace('.', '-'), DateTime.Today.ToString("yyyy-MM-dd").Replace('.', '-'));
            var prices = hs.Select(s => s.close);
            var date = hs.Select(
                d => DateTime.ParseExact(d.tradeDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd MMM")
                )
                .ToArray();
            var length = date.Length;
            for (int i = 0; i < date.Length; i++)
            {

            }
            Labels = date;
            Dispatcher.Invoke(() =>
            {
                var series = new LineSeries
                {
                    Title = "Prices",
                    LineSmoothness = 0,
                    PointGeometrySize = 3,
                    PointForeground = Brushes.Gray
                };
                series.Values = new ChartValues<double>(prices);
                SeriesCollection.Add(series);
            });
        }

        void LoadData()
        {
            MarketLoader marketData = new MarketLoader();
            var mk = marketData.LoadMarket(_security.secId);
            Dispatcher.Invoke(() =>
            {
                tbName.Text = _security.secName;
                tbSecId.Text = _security.secId;
                tbValToday.Text = mk[0].valToday.ToString();
                tbLast.Text = mk[0].last.ToString();
                tbValue.Text = mk[0].value.ToString();
                tbVolToday.Text = mk[0].volToday.ToString();
                tbLastChange.Text = mk[0].lastChange.ToString();
                tbTime.Text = mk[0].time.ToString();
            });
            if (_closed != true)
            {
                Thread.Sleep(5000);
                Task.Run(() => LoadData());
            }
        }

        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
