using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.Subscription
{
    public class service
    {
        public string serviceid { get; set; }
        public string servicename { get; set; }
        public string servicedesc { get; set; }
        public string ImageUrl { get; set; }
        public List<Translations> translations { get; set; }
    }
}
