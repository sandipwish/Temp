using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xmlDocument = new XmlDocument();

            //Read the XML File  
            xmlDocument.Load(@"C:\Users\sandi\source\repos\ConsoleApp1\ConsoleApp1\books.xml");

            XmlNode xmlNodeList = xmlDocument.FirstChild;

            StringBuilder sb = new StringBuilder();
            string delimiter = ",";

            //Display the contents of the child nodes.
            if (xmlNodeList.HasChildNodes)
            {
                List<Info> infos = new List<Info>();
                sb.Append("Author" + delimiter +
                          "Title" + delimiter + "\r\n");

                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    Info info = new Info();
                    sb.Append(Sanitize(xmlNode["Author"].InnerText) + delimiter +
                              Sanitize(xmlNode["Title"].InnerText) + delimiter + "\r\n");

                }
            }
            string somePath = ConfigurationManager.AppSettings.Get("FilePath");
            string filename = String.Format("result_{0}.csv", Guid.NewGuid()); ;
            string path = Path.Combine(somePath, filename);

            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(sb.ToString());
            sw.Close();
        }

        public static string Sanitize(string data)
        {
            if(!String.IsNullOrWhiteSpace(data))
            {
                if (data.Contains("\""))
                {
                    data = data.Replace("\"", "\"\"");
                }

                if (data.Contains(","))
                {
                    data = String.Format("\"{0}\"", data);
                }

                if (data.Contains(System.Environment.NewLine))
                {
                    data = String.Format("\"{0}\"", data);
                }
            }
            
            return data.Trim();
        }
    }



    public class Info
    {
        public string CollageName { get; set; }
        public string Students { get; set; }
    }
}
