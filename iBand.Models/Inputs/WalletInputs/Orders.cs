using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Inputs.WalletInputs
{
    public class Orders
    {
        public string category { get; set; }
        public string merchantname { get; set; }
        public string merchantid { get; set; }
        public string merchantuserid { get; set; }
        public string itemid { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        public string countrycode { get; set; }
        public string addressid { get; set; }
    }
}
