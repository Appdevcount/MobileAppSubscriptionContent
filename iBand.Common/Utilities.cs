using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Common
{
    public class Utilities
    {
        // Fields
        public static string publicImgLoc = ConfigurationSettings.AppSettings["publiclocation"];
        public static string savlocation = ConfigurationSettings.AppSettings["locallocation"];

        // Methods
        public static string getDataFromURL(string url)
        {
            WebClient client = new WebClient();
            return client.DownloadString(url);
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
