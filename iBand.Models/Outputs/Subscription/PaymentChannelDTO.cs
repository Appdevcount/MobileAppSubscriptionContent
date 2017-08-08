using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.Subscription
{
    public class PaymentChannelDTO
    {
        public string PaymentID { get; set; }
        public string PaymentName { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }
    }
}
