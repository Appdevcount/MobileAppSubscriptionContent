using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.Subscription
{
    public class channels
    {
       public string channelid { get; set; }
        public string channelname { get; set; }
        public string channeldesc { get; set; }
        public string ImageURL { get; set; }
        public List<Translations> translations { get; set; }
        
    }
}
