using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PaidServicesRegistrator.Utils
{
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

            var textBytes = Encoding.Unicode.GetBytes(text);
            //var encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoSystem = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoSystem.Write(textBytes, 0, textBytes.Length);
                    cryptoSystem.Close();
                }
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        //TODO error when decrypting data fix
        public static String Decrypt(String encryptedText)
        {
            if (decryptor == null)
                Init();

            var encryptedBytes = Encoding.Unicode.GetBytes(encryptedText);
           // byte[] textBytes = new byte[encryptedText.Length*2];
           // decryptor.TransformBlock(encryptedBytes, 0, encryptedBytes.Length, textBytes, 0);
           
            //var textBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoSystem = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                {
                    cryptoSystem.Write(encryptedBytes, 0, encryptedBytes.Length);
                    cryptoSystem.Close();
                }

                return Encoding.Unicode.GetString(memoryStream.ToArray());
            }
        }
    }
}