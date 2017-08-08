using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.Subscription
{
    public class Category
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }
        public List<Translations> translations { get; set; }


    }

}
