using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PaidServicesRegistrator.Utils
{
    public class HashUtil
    {
        private const String AlgorithmSalt = "J0we864RZ8VxoHsmLQCn";
        private static readonly String Key = "e0B9UIig05bNLXP1";

        public static String GetHashedValue(String text)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Key)))
            {
                hmac.ComputeHash(Encoding.UTF8.GetBytes(text + AlgorithmSalt));
                return Convert.ToBase64String(hmac.Hash);
            }
        }
    }
}