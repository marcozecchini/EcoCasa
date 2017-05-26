using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Reflection;
using EcoCasa.Models;
using Xamarin.Forms;


namespace EcoCasa.Util
{
    public class XMLUtil
    {
        public static bool SaveUserCode(String UserCode)
        {
            try
            {
                //var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "MyCompany.MyProduct.MyFile.txt";

                XDocument doc = XDocument.Load("UserXML.xml");
                XElement User = new XElement("User");
                XElement codeElement = new XElement("Code");
                XAttribute code = new XAttribute("Code",UserCode);
                
                codeElement.Add(code);
                User.Add(codeElement);
                doc.Add(User);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}