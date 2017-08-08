using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.WalletOutputs
{
    public class WalletProfileDTO
    {
        public long userid { get; set; }
        public long profileid { get; set; }
        public string appuserid { get; set; }
        public List<WalletDTO> walletaccounts { get; set; }
        public string fullname { get; set; }
        public string code { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string countrycode { get; set; }
        public string status { get; set; }
        public string isemailverified { get; set; }
        public string ismobileverified { get; set; }
        public string rating { get; set; }
    }
}
