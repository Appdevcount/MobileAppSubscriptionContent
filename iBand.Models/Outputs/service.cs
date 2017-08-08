using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{   
    public class service
    {
        public string serviceid { get; set; }

        public string price { get; set; }

        public string productid { get; set; }

        public string servicedesc { get; set; }

        public string servicename { get; set; }

        public string shortcode { get; set; }

       // public tone tone { get; set; }

        public List<Translations> translations { get; set; }

        public string usermsg { get; set; }

        public string validityperiod { get; set; }

        public string ImageURL { get; set; }

        public string ImageURL2 { get; set; }

        public string isFavourite { get; set; }

    }
}
