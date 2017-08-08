using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Inputs.OneInInputs
{
    public class RegisterUser
    {
        public string mobile { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname { get; set; }
        public string countrycode { get; set; }
        public string dateofbirth { get; set; }


    }
}
