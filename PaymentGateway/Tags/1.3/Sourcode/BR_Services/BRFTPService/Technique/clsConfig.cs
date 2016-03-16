using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BRFTPService.Technique
{
    public class Config
    {
        public static string ConfigFilePath = System.Environment.CurrentDirectory + @"\BRService.conf";
        /**************************************
         * Ham doc file Config cua GWServices
         **************************************/
        
        
        // Lay ten file cau hinh 
        public static string getKeyConfig(string key)
        {
            string result = "";
            // load config document 
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(ConfigFilePath);
            }
            catch (System.IO.FileNotFoundException ex)
            {

                throw new Exception("Khong tim thay File cau hinh cua he thong Service: " + ex.Message);
            }
            // retrieve ServicesConfig node
            XmlNode node = doc.SelectSingleNode("//DatabaseConfig");
            if (node == null)
            {
                throw new InvalidOperationException("Khong tim thay tag DatabaseConfig trong file cau hinh.");
            }
            try
            {
                // select the 'add' element that contains the key
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
                if (elem == null)
                {
                    // add value for key
                    throw new Exception("Khong tim thay key " + key + " trong file cau hinh he thong service.");
                }
                else
                {
                    // key was not found so create the 'add' element
                    // and set it's key/value attributes
                    result = elem.Attributes.GetNamedItem("value").Value;
                }
            }
            catch
            {
                Log.WriteLogFile(0, "Khong tim thay key " + key + " trong file cau hinh he thong service.");
                throw new Exception("Khong tim thay key " + key + " trong file cau hinh he thong service.");

            }
            return result;
        }
    }
}
