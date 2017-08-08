using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.WalletOutputs
{
    public class MerchantDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string CountryCode { get; set; }
        public string Amount { get; set; }
    }
}
