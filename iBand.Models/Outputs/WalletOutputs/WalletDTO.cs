using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.WalletOutputs
{
    public class WalletDTO
    {
        public string userid { get; set; }
        public string accountnumber { get; set; }
        public string walletID { get; set; }
        public string walletType { get; set; }
        public string walletTypeID { get; set; }
        public string walletTypeCountryID { get; set; }
        public string countrycode { get; set; }
        public string walletName { get; set; }
        public string walletCode { get; set; }
        public string balance { get; set; }
        public string currency { get; set; }
    }
}
