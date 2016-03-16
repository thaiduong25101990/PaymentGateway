using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ENCRYPT
{
    public class SecurityEncrypt
    {
        private const string key = "Security2008"; //Encrypt Key 

        public string test()
        {
            return "Hello!. This is TestEncrypt Class Library";
        }

        // Encrypt a string using dual encryption method. Return a encrypted cipher Text 
        public static string Encrypt(string toEncrypt)
        {
            string result = "";
            try
            {
                bool useHashing = true;
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                // Get the key from config file
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

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                result = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                result = null;
            }
            return result;
        }
    }
         public class SecurityDecrypt
    {
        private const string key = "Security2008"; //Encrypt Key 
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
