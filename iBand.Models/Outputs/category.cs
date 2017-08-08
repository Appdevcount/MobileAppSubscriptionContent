using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{
    public class category
    {
        // Properties
        public string albumcount { get; set; }

        public List<album> albums { get; set; }

        public string categoryid { get; set; }

        public string categoryname { get; set; }

        public string imageurl { get; set; }

        public List<Translations> translations { get; set; }
    }


}
