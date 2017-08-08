using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{
    public class tone
    {
        // Properties
        public album album { get; set; }

        public artist artist { get; set; }

        public category category { get; set; }

        public company company { get; set; }

        public string imageurl { get; set; }

        public string tonedesc { get; set; }

        public string toneid { get; set; }

        public string tonename { get; set; }

        public List<Translations> translations { get; set; }
    }


}
