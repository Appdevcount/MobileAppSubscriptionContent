using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.WalletOutputs
{
    public class SaveKYCDTO
    {
        public string profileKYCid { get; set; }
        public List<WalletDTO> walletaccounts { get; set; }
        public string fullname { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string Nationality { get; set; }
        public string POB { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string IDProof1No { get; set; }
        public string IDProof1ImageURL { get; set; }
        public string IDProof1ImageURL2 { get; set; }
        public string IDProof2No { get; set; }
        public string IDProof2ImageURL { get; set; }
        public string IDProof2ImageURL2 { get; set; }
        public string IDProof3No { get; set; }
        public string IDProof3ImageURL { get; set; }
        public string IDProof3ImageURL2 { get; set; }
        public string KYCRating { get; set; }
    }
}
