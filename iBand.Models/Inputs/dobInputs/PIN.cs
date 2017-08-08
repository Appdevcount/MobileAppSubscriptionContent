using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Inputs.dobInputs
{
    public class PIN
    {
        public string PaymentID { get; set; }
        public string app { get; set; }
        public string appuser { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string service { get; set; }
        public string authkey { get; set; }
        public string hash { get; set; }
        public string msisdn { get; set; }
        public string language { get; set; }
        public string template { get; set; }
    }
}
