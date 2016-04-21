using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using PaidServicesRegistrator.Utils;
using PaidServicesRegistrator.Utils.ClientRegister;
using PaidServicesRegistrator.Utils.ServiceRegister;

namespace PaidServicesRegistrator.WcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "PaidServicesRegistrator" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы PaidServicesRegistrator.svc или PaidServicesRegistrator.svc.cs в обозревателе решений и начните отладку.
    public class PaidServicesRegistrator : IPaidServicesRegistrator
    {
        public string RegisterService(XDocument wsdl)
        {
            ServiceDAO register = new ServiceDAO();
            string token = register.RegisterService(wsdl);

            return token;
        }

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
    }
}