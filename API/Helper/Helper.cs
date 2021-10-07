using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;

namespace API.Helper
{
    internal static class Helper
    {
        internal static string GetRandomAlphanumericString(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";
            return GetRandomString(length, alphanumericCharacters);
        }

        private static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            var result = new char[length];
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetBytes(bytes);
            }
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
    }


    public class RsaHelper
    {
        public string Encrypt(string strText)
        {
            CspParameters CSApars = new CspParameters();
            CSApars.KeyContainerName = "Test001";

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(CSApars);

            byte[] byteText = Encoding.UTF8.GetBytes(strText);
            byte[] byteEntry = rsa.Encrypt(byteText, false);

            return Convert.ToBase64String(byteEntry);
        }


        public string Decrypt(string strEntryText)
        {
            CspParameters CSApars = new CspParameters();
            CSApars.KeyContainerName = "Test001";

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(CSApars);

            byte[] byteEntry = Convert.FromBase64String(strEntryText);
            byte[] byteText = rsa.Decrypt(byteEntry, false);

            return Encoding.UTF8.GetString(byteText);
        }
      
    }
}