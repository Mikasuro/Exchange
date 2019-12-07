﻿using Exchange.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            var url = string.Format("iss/securities.json?start={0}", start);
            return MoexDownloader.Load(url);
        }

        public Security[] LoadSecuritiesFrom(int start)
        {
            var result = LoadSecutiryFrom(start);
            var root = JsonConvert.DeserializeObject<Security>(result);
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
            return securities;
        }
        static HttpDownloader MoexDownloader = new HttpDownloader("https://iss.moex.com/");
        
        public void LoadSecutiry()
        {
            string sec = Environment.CurrentDirectory + @"\sec.txt";
            List<Security> securities = new List<Security>();
            if (File.Exists(sec))
            {
                var path = JsonConvert.DeserializeObject<Security[]>(File.ReadAllText(sec));
                securities.AddRange(path);
            }
            else
            {
                int threadCount = Environment.ProcessorCount * 10;
                ThreadPool.SetMinThreads(threadCount, threadCount);
                DateTime dt = DateTime.Now;
                for (int i = 0; ; i += 100 * threadCount)
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
                Console.WriteLine(securities.Count);
                string file = JsonConvert.SerializeObject(securities);
                File.WriteAllText("sec.txt", file);
                Console.WriteLine((dt - DateTime.Now).TotalSeconds);
            }
        }
    }
}