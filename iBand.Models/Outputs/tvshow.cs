using iBand.Models.Outputs.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{
    public class tvshow
    {
        public string showid { get; set; }

        public string price { get; set; }

        public string productid { get; set; }

        public string showdesc { get; set; }

        public string showname { get; set; }

        public string shortcode { get; set; }

        // public tone tone { get; set; }

        public List<PaymentChannelDTO> paymentchannel { get; set; }
        public List<Translations> translations { get; set; }

        public string usermsg { get; set; }

        public string validityperiod { get; set; }

        public string ImageURL { get; set; }

        public string ImageURL2 { get; set; }
    }
}
