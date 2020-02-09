using Exchange.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Services
{
    class TradeLoader
    {
        public string LoadTradeFrom(int start)
        {
            var url = string.Format("iss/statistics/engines/stock/currentprices.json?start={0}", start);
            return MoexDownloader.Load(url);
        }

        public Trades[] LoadTradesFrom(int start)
        {
            var result = LoadTradeFrom(start);
            var root = JsonConvert.DeserializeAnonymousType(result, new { CurrentPrices = new RootObject() });
            var prices = root.CurrentPrices.data.Select(
                d => new Trades
                {
                    secid = d[1] as string,
                    boardid = d[2] as string,
                    tradeTime = Convert.ToDateTime(d[3]),
                    price = Convert.ToDouble(d[4]),
                    quanitity = Convert.ToDouble(d[5]),
                    tradeName = d[6] as string,
                }
                ).ToArray();
            return prices;
        }
        static HttpDownloader MoexDownloader = new HttpDownloader("https://iss.moex.com/");
    }
}
