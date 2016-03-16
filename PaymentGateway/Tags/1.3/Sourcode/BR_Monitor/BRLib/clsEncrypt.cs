using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace BR.BRLib
{


    public class UserEncrypt
    {
        public string sKeySetup = "FPTSecurity2008_BIDVSecurity2008";   //Key setup
        public string sKeyUser = "UserSecurity2008";                     //Key user/pass         
        public string sKeyDB = "FPTSecurity2008";                       //Key server database


        /////////////////////////////////////////////////////////-/
        // Mo ta:       Encrypt a string using dual encryption method.
        //              Return a encrypted cipher Text
        // Tham so:     toEncrypt:  Xau can ma hoa
        //              sKey:       Key ma hoa
        // Tra ve:      
        // Ngay tao:    
        // Nguoi tao:   
        ///////////////////////////////////////////////////////////        
        public string Encrypt(string toEncrypt, string sKey)
        {
            string result = "";
            if (sKey == "")
                sKey = sKeyUser;
            try
            {
                bool useHashing = true;
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(sKey));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(sKey);
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
                MessageBox.Show("Can not encode from input",Common.sCaption);
            }
            return result;
        }



        /////////////////////////////////////////////////////////-/
        // Mo ta:       DeCrypt a string using dual encryption method.
        //              Return a DeCrypted clear string
        // Tham so:     cipherString:   Xau can giai ma
        //              sKey:           Key giai ma
        // Tra ve:      
        // Ngay tao:    
        // Nguoi tao:   
        ///////////////////////////////////////////////////////////
        public string Decrypt(string cipherString, string sKey)
        {
            string result = "";
            if (sKey == "")
                sKey = sKeyUser;
            try
            {
                bool useHashing = true;
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(sKey));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(sKey);

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
                //MessageBox.Show("Can't Decode from Input");                
            }
            return result;
        }
    }
}
