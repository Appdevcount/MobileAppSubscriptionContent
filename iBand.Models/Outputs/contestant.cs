using iBand.Models.Outputs.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{
    public class contestant
    {
        public string contestantid { get; set; }
        public string contestantname { get; set; }
        public string contestantdesc { get; set; }
        public string ImageURL { get; set; }
        public List<PaymentChannelDTO> paymentchannel { get; set; }
        public List<Translations> translations { get; set; }
        public string usermsg { get; set; }
    }
}
