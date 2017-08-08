using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Inputs.Subscription
{
    public class SubscribeUserChannel
    {
        public string userID { get; set; }
        public string serviceID { get; set; }
        public string countryID { get; set; }
        public string channelID { get; set; }
        public string operatorID { get; set; }

       // public WalletInputs.DebitTransactions Wallet { get; set; }

    }
}
