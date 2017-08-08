using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{
    public class company
    {
        // Properties
        public string companyid { get; set; }

        public string companyname { get; set; }

        public string imageurl { get; set; }

        public List<Translations> translations { get; set; }
    }

 

}
