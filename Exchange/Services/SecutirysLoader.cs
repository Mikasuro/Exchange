﻿using Exchange.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exchange.Services
{
    class SecutirysLoader
    {
        public string LoadSecutiryFrom(int start)
        {
            var url = "iss/engines/stock/markets/shares/boards/TQBR/securities.json";
            return MoexDownloader.Load(url);
        }

        public Security[] LoadSecuritiesFrom(int start)
        {
            var result = LoadSecutiryFrom(start);
            var root = JsonConvert.DeserializeAnonymousType(result, new { Securities =  new RootObject() });
            var securities = root.Securities.data.Select(
                d => new Security
                {
                    SECID = d[0] as string,
                    BOARDID = d[1] as string,
                    SHORTNAME = d[2] as string,
                    PREVPRICE = Convert.ToDouble(d[3], CultureInfo.InvariantCulture),
                    LOTSIZE = Convert.ToDouble(d[4], CultureInfo.InvariantCulture),
                    FACEVALUE = Convert.ToDouble(d[5], CultureInfo.InvariantCulture),
                    STATUS = d[6] as string,
                    BOARDNAME = d[7] as string,
                    DECIMALS = Convert.ToDouble(d[8], CultureInfo.InvariantCulture),
                    SECNAME = d[9] as string,
                    REMARKS = d[10] as string,
                    MARKETCODE = d[11] as string,
                    INSTRID = d[12] as string,
                    SECTORID = d[13] as string,
                    MINSTEP = Convert.ToDouble(d[14], CultureInfo.InvariantCulture),
                    PREVWAPRICE = Convert.ToDouble(d[15], CultureInfo.InvariantCulture),
                    FACEUNIT = d[16] as string,
                    PREVDATE = d[17] as string,
                    ISSUESIZE = Convert.ToDouble(d[18], CultureInfo.InvariantCulture),
                    ISIN = d[19] as string,
                    LATNAME = d[20] as string,
                    REGNUMBER = d[21] as string,
                    PREVLEGALCLOSEPRICE = Convert.ToDouble(d[22], CultureInfo.InvariantCulture),
                    PREVADMITTEDQUOTE = Convert.ToDouble(d[23], CultureInfo.InvariantCulture),
                    CURRENCYID = d[24] as string,
                    SECTYPE = d[25] as string,
                    LISTLEVEL = Convert.ToDouble(d[26], CultureInfo.InvariantCulture),
                    SETTLEDATE = d[27] as string,
                }
                ).ToArray();
            return securities;
        }
        static HttpDownloader MoexDownloader = new HttpDownloader("https://iss.moex.com/");
        

        public List<Security> LoadSecutiry()
        {
            string securityFile = Environment.CurrentDirectory + @"\sec.txt";
            List<Security> securities = new List<Security>();
            if (File.Exists(securityFile))
            {
                var path = JsonConvert.DeserializeObject<Security[]>(File.ReadAllText(securityFile));
                securities.AddRange(path);
            }
            else
            {
                int threadCount = Environment.ProcessorCount * 2;
                ThreadPool.SetMinThreads(threadCount, threadCount);
                DateTime dt = DateTime.Now;
                for (int i = 0;i < 1000 ; i += 100 * threadCount)
                {
                    bool hasEmpty = false;
                    var allTasks = new Task<Security[]>[threadCount];
                    Parallel.For(0, threadCount, (j) =>
                    {
                        allTasks[j] = Task.Run<Security[]>(() => LoadSecuritiesFrom(i + j * 100));
                    });
                    foreach (var item in allTasks)
                    {
                        if (item.Result.Length == 0)
                        {
                            hasEmpty = true;
                            break;
                        }
                        securities.AddRange(item.Result);
                    }
                    if (hasEmpty) { break; }
                }
                string file = JsonConvert.SerializeObject(securities);
                File.WriteAllText("sec.txt", file);
            }
            return securities;
        }
    }
}
