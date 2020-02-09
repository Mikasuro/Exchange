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
            string bordid;
            foreach (var item in trade)
            {

            }
            tbBordid.Text = bordid;
            //tbQuanitity.Text = string.Format("{0}", item.quanitity);
            //tbPrice.Text = string.Format("{0}", item.price);
            //tbTradeTime.Text = string.Format("{0}", item.tradeTime);
            
           
        }
    }
}
