using PaidServicesRegistrator.Utils;
using PaidServicesRegistrator.Utils.ClientRegister;
using PaidServicesRegistrator.Utils.ServiceRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

namespace PaidServicesRegistrator.Service
{
    /// <summary>
    /// Summary description for PaidService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PaidService : WebService
    {
        [WebMethod]
        public string RegisterService(XDocument wsdl)
        {
            ServiceDAO register = new ServiceDAO();
            string token = register.RegisterService(wsdl);

            return token;
        }

        [WebMethod]
        public bool CheckUserRegistration(String serviceToken, String userToken)
        {
            ServiceDAO serviceDao = new ServiceDAO();
            int serviceId;

            if ((serviceId = serviceDao.GetIdByToken(serviceToken)) != -1)
            {
                ClientDAO clientDao = new ClientDAO();
                String finishTimeStr = clientDao.FindClientToken(serviceId, userToken);

                if (finishTimeStr != null)
                {
                    TokenUtil.TokenLifeTimeInfo tokenInfo = TokenUtil.ExtractTokenInfo(finishTimeStr);

                    if (TokenUtil.IsTokenValid(tokenInfo))
                    {
                        if (tokenInfo.tokenType.Equals(TokenUtil.TokenType.OneOff))
                        {
                            clientDao.DeleteClient(userToken);
                        }

                        return true;
                    }
                    else
                        clientDao.DeleteClient(userToken);
                }

                return false;
            }

            return false;
        }


        // are we need this method?
        //[WebMethod]
        //public string RegisterClient(int serviceId, TokenUtil.TokenType tokenType)
        //{
        //    ClientDAO register = new ClientDAO();
        //    string token = register.RegisterClient(serviceId, tokenType);

        //    return token;
        //} 
    }
}
