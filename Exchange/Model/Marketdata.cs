﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model
{
    class Marketdata
    {
        public string secId { get; set; }
        public double last { get; set; }
        public double volToday { get; set; }
        public double valToday { get; set; }
        public double value { get; set; }
        public double lastChange { get; set; }
        public string time { get; set; }
    }
}
