using Exchange.Model;
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

        public Trades[] LoadTradeFrom(int start)
        {
            var result = LoadPriceFrom(start);
            var root = JsonConvert.DeserializeAnonymousType(result, new { CurrentPrices = new RootObject() });
            var prices = root.CurrentPrices.data.Select(
                d => new CurrentPrice
                {
                    tradeDate = Convert.ToDateTime(d[0]),
                    secId = d[1] as string,
                    boardId = d[2] as string,
                    tradeTime = Convert.ToDateTime(d[3]),
                    curPrice = Convert.ToDouble(d[4]),
                    lastPrice = Convert.ToDouble(d[5]),
                    legalClose = Convert.ToDouble(d[6]),
                }
                ).ToArray();
            return prices;
        }
    }
}
