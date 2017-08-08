using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Common
{
    public class Global
    {
        // Fields
        public static string ipaddress = "";
        public static string password = "";
        public static string uri = "";
        public static string username = "";

        // Methods
        public static string GetUniqueAlphaNumericKey(int KeyLength)
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789abcdefghijklmnopqrstuvwxyz";
            char[] chArray = new char[str.Length];
            chArray = str.ToCharArray();
            byte[] data = new byte[KeyLength];
            new RNGCryptoServiceProvider().GetNonZeroBytes(data);
            StringBuilder builder = new StringBuilder(KeyLength);
            foreach (byte num in data)
            {
                builder.Append(chArray[num % chArray.Length]);
            }
            return builder.ToString();
        }

        public static string GetUniqueKey(int KeyLength)
        {
            string str = "123456789";
            char[] chArray = new char[str.Length];
            chArray = str.ToCharArray();
            byte[] data = new byte[KeyLength];
            new RNGCryptoServiceProvider().GetNonZeroBytes(data);
            StringBuilder builder = new StringBuilder(KeyLength);
            foreach (byte num in data)
            {
                builder.Append(chArray[num % chArray.Length]);
            }
            return builder.ToString();
        }
    }


}
