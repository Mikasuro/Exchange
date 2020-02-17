using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model
{
    class Marketdata
    {
        public string SecId { get; set; }
        public double Last { get; set; }
        public int VolToday { get; set; }
        public int ValToday { get; set; }
        public double Value { get; set; }
        public double LastChange { get; set; }
        public DateTime Time { get; set; }
    }
}
