using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.Subscription
{
    public class ServiceChannel
    {
        public string ChannelID { get; set; }
        public string ChannelName { get; set; }
        public string ChannelDesc { get; set; }
        public string ChannelCost { get; set; }
        public string ImageURL { get; set; }
        public string isSubscribed { get; set; }

        public Service services { get; set; }
        public List<PaymentChannelDTO> paymentchannel { get; set; }
        public List<Translations> translations { get; set; }

    }

    public class Service
    {
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDesc { get; set; }
        public string ImageURL { get; set; }
        public List<Translations> translations { get; set; }
    }

}
