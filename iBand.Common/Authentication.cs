using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Common
{
    public class Authentication
    {
        public string UserAuth(string msisdn, string service)
        {
            string username = "isystest";
            string password = "isys969";
            string service_name = service;

            string dataToComputeHash = username + "username" + password + "password" + msisdn + "msisdn" + service_name + "service";

            string decryptedOriginal = "123456abcdef";

            HMACSHA256 hmac = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(decryptedOriginal));

            string hash = Hash.ByteToString(hmac.ComputeHash(System.Text.UTF8Encoding.Default.GetBytes(dataToComputeHash)));

            return hash;
        }
        

    }
}
