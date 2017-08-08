using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Inputs.WalletInputs
{
    public class QRRequest
    {
        public List<QROrderItem> orderitems { get; set; }
        public string name { get; set; }
        public string merchantid { get; set; }
        public string merchantuserid { get; set; }
        public string category { get; set; }
        public string code { get; set; }
        public string countrycode { get; set; }
    }
}
