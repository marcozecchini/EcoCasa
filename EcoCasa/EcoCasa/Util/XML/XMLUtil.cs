using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xamarin.Forms;


namespace EcoCasa.Util.XML
{
    public class XMLUtil
    {
        public static bool SaveUserCode(String UserCode)
        {
            try
            {
                var assembly = typeof(XMLUtil).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("EcoCasa.UserXML.xml");
                

                XElement User = new XElement("User");
                XElement codeElement = new XElement("Code");
                XAttribute code = new XAttribute("Code",UserCode);
                
                codeElement.Add(code);
                User.Add(codeElement);
                /*using (var writer = new System.IO.StreamWriter(stream))
                {
                    var serializer = new XmlSerializer(typeof(XElement));
                    serializer.Serialize(writer,User);
                }*/

                var fileService = DependencyService.Get<ISaveAndLoad>();
                fileService.SaveTextAsync("UserXML.xml", User.ToString());
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public static string getCode()
        {
            var assembly = typeof(XMLUtil).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("EcoCasa.UserXML.xml");
            XElement element;
            using (var reader = new System.IO.StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(XElement));
                element = (XElement)serializer.Deserialize(reader);
                if (!element.HasElements) return "";
                element = element.Element("Code");

            }

            return element.Value;
        }
    }
}