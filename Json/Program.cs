using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
namespace JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://iss.moex.com/iss/securities.json?start=100");
            var task = client.GetStringAsync("https://iss.moex.com/iss/securities.json?start=100");
            var json = task.Result;
            var root = JsonConvert.DeserializeObject<Security>(json);
            var securities = root.securities.data.Select(
                d => new Security
                {
                    id = Convert.ToInt32(d[0]),
                    secid = d[1] as string,
                    shortname = d[2] as string,
                    regnumber = d[3] as string,
                    name = d[4] as string,
                    isin = d[5] as string,
                    is_traded = Convert.ToInt32(d[6]),
                    emitent_id = Convert.ToInt32(d[7]),
                    emitent_title = d[8] as string,
                    emitent_inn = d[9] as string,
                    emitent_okpo = d[10] as string,
                    gosreg = d[11] as string,
                    type = d[12] as string,
                    group = d[13] as string,
                    primary_boardid = d[14] as string,
                    marketprice_boardid = d[15] as string,
                }
                ).ToArray();
            foreach (var item in securities)
            {
                Console.WriteLine(item);
            }
        }

    }
}
