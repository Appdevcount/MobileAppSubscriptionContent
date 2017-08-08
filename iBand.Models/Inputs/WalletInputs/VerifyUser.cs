using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iBand.Models.Inputs.WalletInputs
{
    public class VerifyUser
    {
       
        public string code { get; set; }
        public string mobile { get; set; }
        public string appuserid { get; set; }
        public string userid { get; set; }
        public string countrycode { get; set; }
    }
}
