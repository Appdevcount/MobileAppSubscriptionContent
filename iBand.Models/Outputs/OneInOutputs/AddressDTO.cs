using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.OneInOutputs
{
    public class AddressDTO
    {
        public string userid { get; set; }
        public string addressid { get; set; }
        public string alias { get; set; }
        public string addressfield1 { get; set; }
        public string addressfield2 { get; set; }
        public string country { get; set; }
        public string pincode { get; set; }
    }
}
