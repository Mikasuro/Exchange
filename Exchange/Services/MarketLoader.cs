using Exchange.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Services
{
    class MarketLoader
    {
        public string LoadSecutiryFrom(string start)
        {
            var url = string.Format("iss/engines/stock/markets/shares/boards/TQBR/securities/{0}.json", start);
            return MoexDownloader.Load(url);
        }

        public Marketdata[] LoadSecuritiesFrom(string start)
        {
            var result = LoadSecutiryFrom(start);
            var root = JsonConvert.DeserializeAnonymousType(result, new { Securities = new RootObject() });
            var market = root.Securities.data.Select(
                d => new Marketdata
                {
                    Last = Convert.ToDouble(d[0]),
                    VolToday = Convert.ToInt32(d[1]),
                    ValToday = Convert.ToInt32(d[2]),
                    Value = Convert.ToDouble(d[3]),
                    LastChange = Convert.ToDouble(d[4]),
                    Time = Convert.ToDateTime(d[5]),
                    SecId = d[6] as string,
                }
                ).ToArray();
            return market;
        }
        static HttpDownloader MoexDownloader = new HttpDownloader("https://iss.moex.com/");
    }
}
