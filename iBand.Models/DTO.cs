using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models
{
    public class DTO<T>
    {
        public T response;

        public string objname { get; set; }

        public Status status { get; set; }
    }


}
