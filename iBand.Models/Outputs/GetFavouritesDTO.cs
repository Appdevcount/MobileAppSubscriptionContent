using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs
{
    public class GetFavouritesDTO
    {
        public List<Favourite> Favourites { get; set; }
    }

    public class Favourite
    {
        public RBT rbt { get; set; }
        public service service { get; set; }
        public string contenttype { get; set; }
    }
}
