using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Inputs.WalletInputs
{
    public class PayItemRequest
    {
        public Orders Order { get; set; }
        public string mobileno { get; set; }
        public string password { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string paymenttype { get; set; }
        public string paymentcode { get; set; }
        public string transactionamount { get; set; }
        public string transCurrency { get; set; }
    }
}
