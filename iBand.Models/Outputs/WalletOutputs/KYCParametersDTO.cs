using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models.Outputs.WalletOutputs
{
    public class KYCParametersDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string isFirstNameRequired { get; set; }
        public string isLastNameRequired { get; set; }
        public string isFullNameRequired { get; set; }
        public string DOB { get; set; }
        public string isDOBRequired { get; set; }
        public string Gender { get; set; }
        public string isGenderRequired { get; set; }
        public string Nationality { get; set; }
        public string isNationalityRequired { get; set; }
        public string CountryOfBirth { get; set; }
        public string isCountryOfBirthRequired { get; set; }
        public string FullAddress { get; set; }
        public string isFullAddressRequired { get; set; }
        public string IDProof1NoLabel { get; set; }
        public string isIDProof1Required { get; set; }
        public string isProof1CopyRequired { get; set; }
        public string isProof1DoublePaged { get; set; }
        public string IDProof2NoLabel { get; set; }
        public string isIDProof2Required { get; set; }
        public string isProof2CopyRequired { get; set; }
        public string isProof2DoublePaged { get; set; }
        public string IDProof3NoLabel { get; set; }
        public string isIDProof3Required { get; set; }
        public string isProof3CopyRequired { get; set; }
        public string isProof3DoublePaged { get; set; }
    }
}
