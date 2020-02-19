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
            var root = JsonConvert.DeserializeAnonymousType(result, new { Marketdata = new RootObject() });
            var market = root.Marketdata.data.Select(
                d => new Marketdata
                {
                    secId = d[0] as string,
                    time = d[52] as string,
                    last = Convert.ToDouble(d[12]),
                    lastChange = Convert.ToDouble(d[13]),
                    valToday = Convert.ToDouble(d[28]),
                    volToday = Convert.ToDouble(d[27]),
                    value = Convert.ToDouble(d[16]),
                }
                ).ToArray();
            return market;
        }
        static HttpDownloader MoexDownloader = new HttpDownloader("https://iss.moex.com/");
    }
}
