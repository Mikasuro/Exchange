using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model
{
    class ColumnMetadata
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }
}
