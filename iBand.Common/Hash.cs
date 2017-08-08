using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Common
{
    public class Hash
    {
        // Methods
        public static string ByteToString(byte[] buff)
        {
            string str = "";
            for (int i = 0; i < buff.Length; i++)
            {
                str = str + buff[i].ToString("X2");
            }
            return str;
        }

        public static bool CheckHash(string original, string hashString, HashType hashType)
        {
            return (GetHash(original, hashType) == hashString);
        }

        public static string GetHash(string text, HashType hashType)
        {
            switch (hashType)
            {
                case HashType.MD5:
                    return GetMD5(text);

                case HashType.SHA1:
                    return GetSHA1(text);

                case HashType.SHA256:
                    return GetSHA256(text);

                case HashType.SHA512:
                    return GetSHA512(text);
            }
            return "Invalid Hash Type";
        }

        public static string GetHash(string text, HashType hashType, string key)
        {
            switch (hashType)
            {
                case HashType.MD5:
                    return GetMD5(text);

                case HashType.SHA1:
                    return GetSHA1(text);

                case HashType.SHA256:
                    return GetSHA256(text);

                case HashType.SHA512:
                    return GetSHA512(text);

                case HashType.HMACSHA256:
                    return GetHMACSHA256(text, key);
            }
            return "Invalid Hash Type";
        }

        private static string GetHMACSHA256(string text, string key)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            HMACSHA256 hmacsha = new HMACSHA256(encoding.GetBytes(key));
            byte[] bytes = encoding.GetBytes(text);
            return ByteToString(hmacsha.ComputeHash(bytes));
        }

        private static string GetMD5(string text)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(text);
            MD5 md = new MD5CryptoServiceProvider();
            string str = "";
            foreach (byte num in md.ComputeHash(bytes))
            {
                str = str + string.Format("{0:x2}", num);
            }
            return str;
        }

        private static string GetSHA1(string text)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(text);
            SHA1Managed managed = new SHA1Managed();
            string str = "";
            foreach (byte num in managed.ComputeHash(bytes))
            {
                str = str + string.Format("{0:x2}", num);
            }
            return str;
        }

        private static string GetSHA256(string text)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(text);
            SHA256Managed managed = new SHA256Managed();
            string str = "";
            foreach (byte num in managed.ComputeHash(bytes))
            {
                str = str + string.Format("{0:x2}", num);
            }
            return str;
        }

        private static string GetSHA512(string text)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(text);
            SHA512Managed managed = new SHA512Managed();
            string str = "";
            foreach (byte num in managed.ComputeHash(bytes))
            {
                str = str + string.Format("{0:x2}", num);
            }
            return str;
        }

        // Nested Types
        public enum HashType
        {
            MD5,
            SHA1,
            SHA256,
            SHA512,
            HMACSHA256
        }
    }


}
