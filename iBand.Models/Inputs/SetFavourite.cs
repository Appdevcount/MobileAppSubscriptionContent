using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Inputs
{
    public class SetFavourite
    {
        public int UserID { get; set; }
        public int ContentID { get; set; }
        public string Type { get; set; }

    }
}
