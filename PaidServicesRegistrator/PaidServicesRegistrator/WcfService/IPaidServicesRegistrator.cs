using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace PaidServicesRegistrator.WcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IPaidServicesRegistrator" в коде и файле конфигурации.
    [ServiceContract]
    public interface IPaidServicesRegistrator
    {
        [OperationContract]
        string RegisterService(XDocument wsdl);

        [OperationContract]
        bool CheckUserRegistration(String serviceToken, String userToken);
    }
}