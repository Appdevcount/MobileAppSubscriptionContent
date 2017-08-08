using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.WalletOutputs
{
    public class QROrderResponseDTO
    {
        public DebitTransactionsDTO transactionDetails { get; set; }
        public MerchantDTO merchantDetails { get; set; }
    }
}
