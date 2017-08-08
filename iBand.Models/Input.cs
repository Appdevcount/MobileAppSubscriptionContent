using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models
{

    public class Input<T>
    {
        public T input { get; set; }

        public CommonInputParams param { get; set; }

        public SecureHash secure { get; set; }
    }
}
