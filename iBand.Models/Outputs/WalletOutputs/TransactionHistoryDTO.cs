using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.WalletOutputs
{
    public  class TransactionHistoryDTO
    {
        public List<TransactionDTO> transaction { get; set; }
    }
}
