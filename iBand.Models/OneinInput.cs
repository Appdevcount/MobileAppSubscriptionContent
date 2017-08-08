using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models
{
    public class OneinInput<T>
    {
        public T input { get; set; }

        public OneInCommonInputParams param { get; set; }

        public SecureHash secure { get; set; }
    }
}
