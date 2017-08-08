using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{
    public class artist
    {
        // Properties
        public List<album> albums { get; set; }

        public string artistid { get; set; }

        public string artistname { get; set; }

        public string imageurl { get; set; }

        public string albumcount { get; set; }

        public List<Translations> translations { get; set; }
    }


}
