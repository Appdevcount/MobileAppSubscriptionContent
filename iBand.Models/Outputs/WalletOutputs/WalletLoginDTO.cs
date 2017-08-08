using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.WalletOutputs
{
    
    public class WalletLoginDTO
    {
        public string userid { get; set; }
        public string profileid { get; set; }
        public string accountnumber { get; set; }
        public string fullname { get; set; }
        public string isemailverified { get; set; }
        public string ismobileverified { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string walletbalance { get; set; }
    }
}
