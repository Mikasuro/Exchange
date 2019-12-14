using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model
{
    public class Security
    {
        public int id { get; set; }
        public string secid { get; set; }
        public string shortname { get; set; }
        public string regnumber { get; set; }
        public string name { get; set; }
        public string isin { get; set; }
        public int is_traded { get; set; }
        public int emitent_id { get; set; }
        public string emitent_title { get; set; }
        public string emitent_inn { get; set; }
        public string emitent_okpo { get; set; }
        public string gosreg { get; set; }
        public string type { get; set; }
        public string group { get; set; }
        public string primary_boardid { get; set; }
        public string marketprice_boardid { get; set; }
    }
}
