using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;

namespace PaidServicesRegistrator.Utils
{
    // work only with ASCII symbols
    public class AesCryptUtil
    {
        private static SymmetricAlgorithm aesProvider;
        private static ICryptoTransform encryptor;
        private static ICryptoTransform decryptor;
        private const String DefaultKey = "e0B9UIig05bNLXP1";
        private const String DefaultIV = "S1xPvjda0Ax96rDb";

        public static void Init()
        {
            Init(Encoding.ASCII.GetBytes(DefaultKey), Encoding.ASCII.GetBytes(DefaultIV));
        }

        public static void Init(byte[] key, byte[] IV) // key, initialization vector
        {
            aesProvider = new AesCryptoServiceProvider {Key = key, IV = IV};
            encryptor = aesProvider.CreateEncryptor();
            decryptor = aesProvider.CreateDecryptor();
        }

        public static String Encrypt(String text)
        {
            if (encryptor == null)
                Init();

            var textBytes = Encoding.ASCII.GetBytes(text);
            var encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

        public static String Decrypt(String encryptedText)
        {
            if (decryptor == null)
                Init();

            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var textBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.ASCII.GetString(textBytes);
        }
    }
}