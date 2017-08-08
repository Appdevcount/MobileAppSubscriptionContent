using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Common
{
    public class Security
    {
        // Fields
        private static string master_IV = "SHARHARIRIYATAIY";
        private static string master_key = "NANNUEVARUHACKCHEYYALERUMEMUTAPA";

        // Methods
        public static string decrypt(string ciphertext)
        {
            string s = ciphertext;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(master_key);
                aes.IV = Encoding.UTF8.GetBytes(master_IV);
                string str2 = DecryptStringFromBytes_Aes(Convert.FromBase64String(s), aes.Key, aes.IV);
                Console.WriteLine("Round Trip: {0}", str2);
                return str2;
            }
        }

        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if ((cipherText == null) || (cipherText.Length <= 0))
            {
                throw new ArgumentNullException("cipherText");
            }
            if ((Key == null) || (Key.Length <= 0))
            {
                throw new ArgumentNullException("Key");
            }
            if ((IV == null) || (IV.Length <= 0))
            {
                throw new ArgumentNullException("Key");
            }
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream stream = new MemoryStream(cipherText))
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(stream2))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string encrypt(string plaintext)
        {
            string plainText = plaintext;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(master_key);
                aes.IV = Encoding.UTF8.GetBytes(master_IV);
                return Convert.ToBase64String(EncryptStringToBytes_Aes(plainText, aes.Key, aes.IV));
            }
        }

        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            if ((plainText == null) || (plainText.Length <= 0))
            {
                throw new ArgumentNullException("plainText");
            }
            if ((Key == null) || (Key.Length <= 0))
            {
                throw new ArgumentNullException("Key");
            }
            if ((IV == null) || (IV.Length <= 0))
            {
                throw new ArgumentNullException("Key");
            }
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(stream2))
                        {
                            writer.Write(plainText);
                        }
                        return stream.ToArray();
                    }
                }
            }
        }

        public static string GenerateMD5(string vkey, string username, string password)
        {
            string s = null;
            s = username + password + vkey;
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            bytes = provider.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte num in bytes)
            {
                builder.Append(num.ToString("x2").ToLower());
            }
            return builder.ToString();
        }
    }


}
