using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;
using PaidServicesRegistrator.Utils;

namespace PaidServicesRegistrator.View
{
    public partial class MainView : Page
    {
        private static bool firstStart = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (firstStart)
            {
                TokenTypeDropDownList.DataSource = new[] { TokenUtil.TokenType.OneOff, TokenUtil.TokenType.ThirtyDays };
                //Enum.GetNames(typeof (TokenUtil.TokenType));
                TokenTypeDropDownList.DataBind();
                //TODO get services names from database
                firstStart = false;
            }

            // test encryption
            var encryptedText = AesCryptUtil.Encrypt("some text");
            var decryptedText = AesCryptUtil.Decrypt(encryptedText);
        }

        protected void OnGetTokenButtonClick(object sender, EventArgs e)
        {
            //TODO push to database
            throw new NotImplementedException();
        }
    }
}