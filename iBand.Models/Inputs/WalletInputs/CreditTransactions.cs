using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Inputs.WalletInputs
{
    public class CreditTransactions
    {
        public string userid { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string paymentservice { get; set; }
        public string paymentref { get; set; }
        public string paymentstatus { get; set; }
        public string amount { get; set; }
        public string transactionamount { get; set; }
        public string currency { get; set; }
        public string transCurrency { get; set; }

    }
}
