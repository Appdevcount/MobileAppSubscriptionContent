using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.OneInOutputs
{
    public class Login
    {
        public string mobileno { get; set; }
        public string email { get; set; }
        public string isemailverified { get; set; }
        public string ismobileverified { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string userid { get; set; }
       
        public string appuserid { get; set; }
        public string code { get; set; }
        public string dob { get; set; }
    }
}
