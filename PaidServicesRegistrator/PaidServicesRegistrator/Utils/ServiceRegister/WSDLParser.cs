using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PaidServicesRegistrator.Utils.ServiceRegister
{
    public class WSDLParser
    {
        public static String getNameFromWSDL(XDocument document)
        {
            XNamespace wsdlSpace = "http://schemas.xmlsoap.org/wsdl/";
            try
            {
                foreach (XElement element in document.Root.Elements(wsdlSpace + "service"))
                {
                    return element.Attribute("name").Value;
                }
            }
            catch (System.Xml.XmlException exp)
            {
                return "File is corrupted";
            }
            return "No name";
        }
    }
}