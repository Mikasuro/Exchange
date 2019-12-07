using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model
{
    class Metadata
    {
        public Id id { get; set; }
        public ColumnMetadata secid { get; set; }
        public ColumnMetadata shortname { get; set; }
        public ColumnMetadata regnumber { get; set; }
        public ColumnMetadata name { get; set; }
        public ColumnMetadata isin { get; set; }
        public Is_Traded is_traded { get; set; }
        public Emitent_Id emitent_id { get; set; }
        public ColumnMetadata emitent_title { get; set; }
        public ColumnMetadata emitent_inn { get; set; }
        public ColumnMetadata emitent_okpo { get; set; }
        public ColumnMetadata gosreg { get; set; }
        public Type type { get; set; }
        public ColumnMetadata group { get; set; }
        public ColumnMetadata primary_boardid { get; set; }
        public ColumnMetadata marketprice_boardid { get; set; }
    }
}
