using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace DECRYPT
{
    public class Security
    {

        private const string key = "Security2008"; //Encrypt Key 

        public string test()
        {
            return "Hello!. This is Test Decrypt - Class Library";
        }
        public static string Decrypt(string cipherString)
        {
            string result = "";
            try
            {
                bool useHashing = true;
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                //string key = (string)settingsReader.GetValue("Security", typeof(String));
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                result = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                result = null;
            }
            return result;
        }
    }
}
