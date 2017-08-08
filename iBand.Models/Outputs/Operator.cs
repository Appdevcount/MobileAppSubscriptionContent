using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{
    public class Operator
    {
        // Properties
        public string mcc { get; set; }

        public string mnc { get; set; }

        public string operatorid { get; set; }

        public string operatorname { get; set; }

        public List<Translations> translations { get; set; }
    }


}
