using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{
    public class Country
    {
        // Properties
        public string countrycode { get; set; }

        public string countryid { get; set; }

        public string countryisd { get; set; }

        public string countryname { get; set; }

        public List<Operator> operators { get; set; }

        public List<Translations> translations { get; set; }
    }


}
