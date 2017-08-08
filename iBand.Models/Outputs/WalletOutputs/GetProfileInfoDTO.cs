using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.WalletOutputs
{
    public class GetProfileInfoDTO
    {
        public KYCParametersDTO KYCParameters { get; set; }
        public WalletProfileDTO Profile { get; set; }
        public SaveKYCDTO UserKYC { get; set; }
    }
}
