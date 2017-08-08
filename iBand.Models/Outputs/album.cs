using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{
    public class album
    {
        // Properties
        public string albumid { get; set; }

        public string albumname { get; set; }

        public string imageurl { get; set; }

       

        public string rbtcount { get; set; }

        public List<RBT> rbts { get; set; }

        public List<Translations> translations { get; set; }
    }


}
