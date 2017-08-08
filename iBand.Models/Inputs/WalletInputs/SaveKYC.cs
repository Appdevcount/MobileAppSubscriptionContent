using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Inputs.WalletInputs
{
    public class SaveKYC
    {
        public string userid { get; set; }
        public string profileid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string Nationality { get; set; }
        public string PlaceOfBirth { get; set; }
        public string IDProof1No { get; set; }
        public string IDProof1Image1 { get; set; }
        public string IDProof1Image2 { get; set; }
        public string IDProof2No { get; set; }
        public string IDProof2Image1 { get; set; }
        public string IDProof2Image2 { get; set; }
        public string IDProof3No { get; set; }
        public string IDProof3Image1 { get; set; }
        public string IDProof3Image2 { get; set; }

    }
}
