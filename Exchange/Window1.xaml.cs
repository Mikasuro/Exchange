using Exchange.Model;
using Exchange.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Exchange
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
       
        public Window1()
        {
            InitializeComponent();
        }
        public Window1(Security security)
        {
            InitializeComponent();
            TradeLoader tradeLoader = new TradeLoader();
            var trade = tradeLoader.LoadTradesFrom(security.secid);
            tbName.Text = security.name;
            tbSecId.Text = security.secid;
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
            tbTotal.Text = string.Format("{0}",total);
        }
    }
}
