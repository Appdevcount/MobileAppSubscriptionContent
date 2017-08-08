using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.WalletOutputs
{
    public class CreditTransactionsDTO
    {
        public string Userid { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionRef { get; set; }
        public string TransactionDate { get; set; }
        public string PaymentRef { get; set; }
        public string PaymentService { get; set; }
        public string Amount { get; set; }
        public string Balance { get; set; }
    }
}
