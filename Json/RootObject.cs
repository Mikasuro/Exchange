using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
    public class Rootobject
    {
        public Securities securities { get; set; }
    }
    public class Metadata
    {
        public Id id { get; set; }
        public Secid secid { get; set; }
        public Shortname shortname { get; set; }
        public Regnumber regnumber { get; set; }
        public Name name { get; set; }
        public Isin isin { get; set; }
        public Is_Traded is_traded { get; set; }
        public Emitent_Id emitent_id { get; set; }
        public Emitent_Title emitent_title { get; set; }
        public Emitent_Inn emitent_inn { get; set; }
        public Emitent_Okpo emitent_okpo { get; set; }
        public Gosreg gosreg { get; set; }
        public Type type { get; set; }
        public Group group { get; set; }
        public Primary_Boardid primary_boardid { get; set; }
        public Marketprice_Boardid marketprice_boardid { get; set; }
    }
    public class Security
    {
        public Securities securities { get; set; }
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

    public class Securities
    {
        public string[] columns { get; set; }
        public object[][] data { get; set; }
    }

    public class Id
    {
        public string type { get; set; }
    }

    public class Secid
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Shortname
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Regnumber
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Name
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Isin
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Is_Traded
    {
        public string type { get; set; }
    }

    public class Emitent_Id
    {
        public string type { get; set; }
    }

    public class Emitent_Title
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Emitent_Inn
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Emitent_Okpo
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Gosreg
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Type
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Group
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Primary_Boardid
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

    public class Marketprice_Boardid
    {
        public string type { get; set; }
        public int bytes { get; set; }
        public int max_size { get; set; }
    }

}
