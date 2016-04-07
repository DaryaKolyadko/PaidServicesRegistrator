using PaidServicesRegistrator.Utils;
using PaidServicesRegistrator.Utils.ClientRegister;
using PaidServicesRegistrator.Utils.ServiceRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

namespace PaidServicesRegistrator.PaidService
{
    /// <summary>
    /// Summary description for PaidService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PaidService : System.Web.Services.WebService
    {
        private const string uid = "root";
        private const string pass = "318114275";

        [WebMethod]
        public string RegisterService(XDocument wsdl)
        {
            ServiceDAO register = new ServiceDAO(uid, pass);
            string token = register.RegisterService(wsdl);

            return token;
        }

        [WebMethod]
        public string RegisterClient(int serviceId, TokenUtil.TokenType tokenType)
        {
            ClientDAO register = new ClientDAO(uid, pass);
            string token = register.RegisterClient(serviceId, tokenType);

            return token;
        } 
    }
}
