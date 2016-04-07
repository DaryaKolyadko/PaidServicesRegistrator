using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PaidServicesRegistrator.Utils
{
    public class TokenUtil
    {
        private const int RandomStringLength = 15;
        private const int MarkLength = 3;

        public enum TokenType
        {
            Undefined = -1,
            OneOff = 1,
            ThirtyDays = 30
        }

        public struct TokenLifeTimeInfo
        {
            public TokenLifeTimeInfo(TokenType tokenType, DateTime finishDate)
            {
                this.tokenType = tokenType;
                this.finishDate = finishDate;
            }

            public TokenType tokenType;
            public DateTime finishDate;
        }

        public static String GenerateUserTokenLifeTimeString(TokenType tokenType)
        {
            var finishDate = DateTime.Now.AddDays((int)tokenType);
            var finishDateStr = finishDate.Ticks.ToString();
            return AppendTokenTypeMark(finishDateStr, tokenType);
        }

        // extract finishTime and tockenType from decrypted string from database
        public static TokenLifeTimeInfo ExtractTokenInfo(String text)
        {
            TokenType tokenType;
            long finishDate;
            var parseResultMark = Enum.TryParse(text.Substring(text.Length - MarkLength), out tokenType);
            var parseResultDate = long.TryParse(text.Substring(0, text.Length - MarkLength), out finishDate);

            if (!parseResultMark || !parseResultDate)
                return new TokenLifeTimeInfo(TokenType.Undefined, DateTime.Now);

            return new TokenLifeTimeInfo(tokenType, new DateTime(finishDate));
        }

        public static String GenerateUserToken(String webService)
        {
            string rndString = randomString(RandomStringLength);
            var data = JoinDataBeforeHash(webService, rndString, DateTime.Now.ToString("F"));
            return HashUtil.GetHashedValue(data);
        }

        public static String GenerateServiceToken(String webService)
        {
            var data = JoinDataBeforeHash(webService, DateTime.Now.ToString("F"));
            return HashUtil.GetHashedValue(data);
        }

        //===============PRIVATE==============

        private static String AppendTokenTypeMark(String text, TokenType tokenType)
        {
            return String.Format("{0}{1:000}", text, (int)tokenType);
        }

        private static String JoinDataBeforeHash(params String[] stringList)
        {
            return String.Join(String.Empty, stringList);
        }

        private static String randomString(int length)
        {
            const int FIRST_SYM = 33; //inclusive
            const int LAST_SYM = 127; //exclusive
            String rndString = "";
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                rndString += (char)rnd.Next(FIRST_SYM, LAST_SYM);
            }
            return rndString;
        }
    }
}