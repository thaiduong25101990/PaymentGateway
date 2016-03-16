using System;
//using System.Numerics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;
using System.Web.UI.WebControls.WebParts;
using System.Web.RegularExpressions;

using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Xml.Linq;
using Microsoft.Win32;


namespace BIDVWEB.Comm
{
    public class clsCommon
    {
        public string strError="";                              //Chuoi thong bao loi
        public const string gc_sDisplayFormat = "dd/mm/yyyy";   //
        public int iMenutop=1;                                  //Menutop


        //Ham kiem tra chuoi nhap vao la ngay//////////////////////////
        //Mo ta:        Ham kiem tra chuoi nhap vao la ngay
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      ds: dataset
        //Dau ra:       Tra ra true: Neu la ngay, nguoc lai: False
        ///////////////////////////////////////////////////////////////
        public bool g_IsDate(string sDate)
        {
            DateTime dt;
            bool isDate = true;
            int iCount = 0;
            string str = "";
            string strDay = "";
            string strMonth = "";
            string strYear = "";
            string[] arrayDate = new string[2];
            char[] splitter = { '/' };
                    
            try
            {
                ////DOI DINH DANG TU DD/MM/YYYY => MM/DD/YYYY
                //arrayDate = sDate.Split(splitter);
                //strDay = arrayDate[0];
                //strMonth = arrayDate[1];
                //strYear = arrayDate[2];
                ////str = strMonth.PadLeft(2, '0') + "/" + strDay.PadLeft(2, '0') + "/" + strYear;
                //str = strDay.PadLeft(2, '0') + "/" + strMonth.PadLeft(2, '0') + "/" + strYear;            

                ////Kiem tra du dinh dang ngay/thang/nam theo "DD/MM/YYYY"
                //for (int i = 0; i < sDate.Length; i++)
                //{
                //    if (sDate[i].ToString() == "/" || sDate[i].ToString() == "-")
                //    {
                //        iCount = iCount + 1;
                //    }
                //}
                //if (iCount < 2)
                //{
                //    isDate = false;
                //}
                //dt = DateTime.Parse(str);
                //Kiem tra dinh dang ShortDate
                //open the key
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\International");
                //
                string strKey = "";
                string sFormatDate = "0";
                if (key.GetValue("sShortDate") != null)
                {
                    strKey = key.GetValue("sShortDate").ToString().ToUpper();
                }
                if (strKey == "MM/DD/YYYY" || strKey == "M/D/YY" || strKey == "M/D/YYYY"
                    || strKey == "MM/DD/YY")
                {
                    sFormatDate = "0";
                }
                else
                {
                    sFormatDate = "1";
                }

                arrayDate = sDate.Split(splitter);
                strDay = arrayDate[0];
                strMonth = arrayDate[1];
                strYear = arrayDate[2];
                if (sFormatDate == "0")
                {
                    //DOI DINH DANG TU DD/MM/YYYY => MM/DD/YYYY
                    //str = sDate;
                    str = strMonth.PadLeft(2, '0') + "/" + strDay.PadLeft(2, '0') + "/" + strYear;
                }
                else
                {
                    str = strDay.PadLeft(2, '0') + "/" + strMonth.PadLeft(2, '0') + "/" + strYear;
                    //str = sDate;
                }

                //Kiem tra du dinh dang ngay/thang/nam theo "DD/MM/YYYY"
                for (int i = 0; i < sDate.Length; i++)
                {
                    if (sDate[i].ToString() == "/" || sDate[i].ToString() == "-")
                    {
                        iCount = iCount + 1;
                    }
                }
                if (iCount < 2)
                {
                    isDate = false;
                }
                dt = DateTime.Parse(str);    
            }
            catch
            {
                isDate = false;
            }
            return isDate;
        }

        //Ham kiem tra chuoi nhap vao la tien te///////////////////////
        //Mo ta:        Ham kiem tra chuoi nhap vao la tien te
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strCheck: chuoi
        //Dau ra:       Tra ra true: Neu la tien te, nguoc lai: False
        ///////////////////////////////////////////////////////////////
        public bool g_IsCurrency(string strCheck)
        {
            try
            {
                string si = "1234567890.,";
                int iCount1 = 0;                

                //Neu "." dung truoc "," => chuoi ko la currency
                if (strCheck.IndexOf(".") >= 0 && strCheck.IndexOf(",") >= 0)
                {
                    if (strCheck.IndexOf(",") >= strCheck.IndexOf("."))
                    {
                        return false;
                    }
                }
                for (int i = 0; i < strCheck.Length; i++)
                {
                    //Dem so "." trong chuoi
                    if (strCheck[i].Equals('.'))
                    {
                        iCount1 = iCount1 + 1;
                    }                    
                    //Kiem tra ky tu "," nhap canh nhau
                    if (i < strCheck.Length - 1)
                    {
                        if (strCheck[i].Equals(',') && strCheck[i].Equals(strCheck[i + 1]))
                        {
                            return false;
                        }
                    }
                    int a = 0;
                    for (int j = 0; j < si.Length; j++)
                    {
                        if (!strCheck[i].Equals(si[j]))
                        {
                            a++;
                        }
                    }
                    //Neu a = 10 ==> strCheck[i] khong phai la so
                    // ==> function = false
                    if (a == 12)
                    {                        
                        return false;
                    }
                }
                if (iCount1>1)
                {                    
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        //Ham kiem tra chuoi nhap vao la so////////////////////////////
        //Mo ta:        Ham kiem tra chuoi nhap vao la so
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strCheck: chuoi
        //Dau ra:       Tra ra true: Neu la so, nguoc lai: False
        ///////////////////////////////////////////////////////////////
        public bool g_IsNumeric(string strCheck)
        {
            try
            {                
                string si = "1234567890";
                for (int i = 0; i < strCheck.Length; i++)
                {
                    int a = 0;
                    for (int j = 0; j < si.Length; j++)
                    {
                        if (!strCheck[i].Equals(si[j]))
                        {
                            a++;
                        }
                    }
                    //Neu a = 10 ==> strCheck[i] khong phai la so
                    // ==> function = false
                    if (a == 10)
                    {
                        //strError = "Nhập số!";
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


//        public bool IsValidEmail(string email)
//        {

//            string pattern = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.
//            (com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$";

//            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
//            bool valid = false;
//            if (string.IsNullOrEmpty(email))
//            {
//                valid = false;
//            }
//            else
//            {
//                valid = check.IsMatch(email);
//            }
//            return valid;
////        }


        //Ham tim kiem mot record trong dataset////////////////////////
        //Mo ta:        Ham tim kiem mot record trong dataset
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      ds: dataset
        //Dau ra:       Tra ra mot record
        ///////////////////////////////////////////////////////////////
        public DataRow g_GetDatarow(DataSet ds, string sID1, string sField1)
        {
            try
            {                
                DataRow dr;

                dr = null;
                if (ds == null) { return dr; }
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                { 
                    dr = ds.Tables[0].Rows[i];
                    if (dr[sField1].ToString().Trim() == sID1.Trim()) 
                    {                        
                        return dr;
                    }
                }
                return dr;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        ///////////////////////////////////////////////////////////////
        //Mo ta:        Xu li chuoi ky tu, chuyen ky tu "'" thanh "''"
        //              (hoac thanh "")
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      strConvert: Xau can kiem tra
        //Dau ra:       Tra ra xau da kiem tra
        ///////////////////////////////////////////////////////////////        
        public string g_sConvert2Valid(string strConvert, bool IsBool)
        {
            try
            {                
                string str="";    
                            
                if (strConvert.Trim() == "")
                {
                    return "";
                }                
                if (IsBool == false)
                {                    
                    //Chuyen thanh "''"
                    str = strConvert.Replace("'", "''");
                }                    
                else
                {
                    //Chuyen thanh ""
                    str = strConvert.Replace("'", "");                    
                }
                return str;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return "";     
            }            
        }
       

        ///////////////////////////////////////////////////////////////
        //Mo ta:        Ham kiem tra xau rong
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      obj: Xau can kiem tra
        //Dau ra:       Tra ra xau da kiem tra
        ///////////////////////////////////////////////////////////////
        public string g_sCheckString(string strCheck)
        {
            if (strCheck.Trim()== "")
            {
                return "";
            }
            else
            {
                return strCheck.Trim();                               
            }
        }


        /////////////////////////////////////////////////////////////////
        //Mo ta:        Ham lay gia tri ngay hien tai he thong
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      
        //Dau ra:       Tra ra chuoi co dinh dang "DD/MM/YYYY"
        /////////////////////////////////////////////////////////////////
        public string GetSysDate()
        {
            string str = "";
            str = DateTime.Now.Day.ToString().PadLeft(2,'0') + "/" +
                DateTime.Now.Month.ToString().PadLeft(2,'0') + "/" +
                DateTime.Now.Year.ToString();
            return str;
        }


        /////////////////////////////////////////////////////////////////
        //Mo ta:        Ham dinh dang kieu du lieu date THEO YYYYMMDD
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      sDate: Gia tri date 'DD/MM/YYYY'        
        //Dau ra:       Date da dinh dang
        /////////////////////////////////////////////////////////////////
        public string g_FormatdateYYYYMMDD(string sDate)
        {
            string str = "";
            string strDay = "";
            string strMonth = "";
            string strYear = "";
            string[] arrayDate = new string[2];
            char[] splitter = { '/' };

            //Kiem tra dinh dang ShortDate
            //open the key
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\International");
            //
            string strKey = "";
            string sFormatDate = "0";
            if (key.GetValue("sShortDate") != null)
            {
                strKey = key.GetValue("sShortDate").ToString().ToUpper();
            }
            if (strKey == "MM/DD/YYYY" || strKey == "M/D/YY" || strKey == "M/D/YYYY"
                || strKey == "MM/DD/YY")
            {
                sFormatDate = "0";
            }
            else
            {
                sFormatDate = "1";
            }

            if (sDate == "")
            {
                return "";
            }
            arrayDate = sDate.Split(splitter);
            strDay = arrayDate[0];
            strMonth = arrayDate[1];
            strYear = arrayDate[2];
            if (sFormatDate == "0")
            {
                str = strYear + strMonth.PadLeft(2, '0') + strDay.PadLeft(2, '0');
            }
            else
            {
                str = strYear + strDay.PadLeft(2, '0') + strMonth.PadLeft(2, '0');
            }
            return str;
        }


        /////////////////////////////////////////////////////////////////
        //Mo ta:        Ham dinh dang kieu du lieu date
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      sDate: Gia tri date
        //              bDDmmyy: True: "DD/MM/YYYY" ->"MM/DD/YYYY"
        //                       False:"MM/DD/YYYY" ->"DD/MM/YYYY"
        //Dau ra:       Date da dinh dang
        /////////////////////////////////////////////////////////////////
        public string g_Formatdate(string sDate, bool bDDmmyy)
        {
            string str = "";
            string strDay = "";
            string strMonth = "";
            string strYear = "";
            string[] arrayDate = new string[2];
            char[] splitter = { '/' };
                        
            if (sDate == "")
            {
                return "";
            }

            //Kiem tra dinh dang ShortDate
            //open the key
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\International");
            //
            string strKey = "";
            string sFormatDate = "0";
            if (key.GetValue("sShortDate") != null)
            {
                strKey = key.GetValue("sShortDate").ToString().ToUpper();
            }
            if (strKey == "MM/DD/YYYY" || strKey == "M/D/YY" || strKey == "M/D/YYYY"
                || strKey == "MM/DD/YY")
            {
                sFormatDate = "0";
            }
            else
            {
                sFormatDate = "1";
            }

            arrayDate = sDate.Split(splitter);
            strDay = arrayDate[0];
            strMonth = arrayDate[1];
            strYear = arrayDate[2];
            if (sFormatDate == "1")
            {
                //DOI DINH DANG TU DD/MM/YYYY => MM/DD/YYYY
                //str = sDate;
                str = strMonth.PadLeft(2, '0') + "/" + strDay.PadLeft(2, '0') + "/" + strYear;
            }
            else
            {
                if (bDDmmyy == true)
                    str = strMonth.PadLeft(2, '0') + "/" + strDay.PadLeft(2, '0') + "/" + strYear;
                else
                    str = strDay.PadLeft(2, '0') + "/" + strMonth.PadLeft(2, '0') + "/" + strYear;

            }

            //arrayDate = sDate.Split(splitter);
            //if (bDDmmyy == true)
            //{
            //    strDay = arrayDate[0];
            //    strMonth = arrayDate[1];
            //    strYear = arrayDate[2];
            //    str = strMonth.PadLeft(2,'0') + "/" + strDay.PadLeft(2,'0') + "/" + strYear;
            //}
            //else
            //{
            //    strMonth = arrayDate[0];
            //    strDay = arrayDate[1];
            //    strYear = arrayDate[2];
            //    str = strDay.PadLeft(2, '0') + "/" + strMonth.PadLeft(2, '0') + "/" + strYear;
            //}            
            return str;            
        }
        
        /////////////////////////////////////////////////////////////////
        //Mo ta:        Ham convert kieu du lieu date
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      obj: Xau can kiem tra
        //Dau ra:       Tra ra xau da kiem tra co kieu "DD-MMM-YYYY"
        /////////////////////////////////////////////////////////////////
        public string g_ConvertDateToString(string strDate, bool bDDmmyy)
        {
            string str = "";
            string strDay = "";
            string strMonth = "";
            string strYear = "";
            string[] arrayDate = new string[2];
            char[] splitter = { '/' };

            arrayDate = strDate.Split(splitter);
                                    
            if (strDate == "")
            {
                return "";
            }
            else
            {
                if (bDDmmyy == true)
                {   
                    strDay = arrayDate[0];
                    strMonth = arrayDate[1];
                    strYear = arrayDate[2];

                }
                else
                {                                         
                    strMonth = arrayDate[0];
                    strDay = arrayDate[1];
                    strYear = arrayDate[2];
                }
                switch (strMonth)
                {
                    case "01":
                        strMonth = "Jan";
                        break;
                    case "1":
                        strMonth = "Jan";
                        break;
                    case "02":
                        strMonth = "Feb";
                        break;
                    case "2":
                        strMonth = "Feb";
                        break;
                    case "03":
                        strMonth = "Mar";
                        break;
                    case "3":
                        strMonth = "Mar";
                        break;
                    case "04":
                        strMonth = "Apr";
                        break;
                    case "4":
                        strMonth = "Apr";
                        break;
                    case "05":
                        strMonth = "May";
                        break;
                    case "5":
                        strMonth = "May";
                        break;
                    case "06":
                        strMonth = "Jun";
                        break;
                    case "6":
                        strMonth = "Jun";
                        break;
                    case "07":
                        strMonth = "Jul";
                        break;
                    case "7":
                        strMonth = "Jul";
                        break;
                    case "08":
                        strMonth = "Aug";
                        break;
                    case "8":
                        strMonth = "Aug";
                        break;
                    case "09":
                        strMonth = "Sep";
                        break;
                    case "9":
                        strMonth = "Sep";
                        break;
                    case "10":
                        strMonth = "Oct";
                        break;
                    case "11":
                        strMonth = "Nov";
                        break;
                    case "12":
                        strMonth = "Dec";
                        break;
                }
                str = strDay + "-" + strMonth + "-" + strYear;
                return str;
            }
        }

        /////////////////////////////////////////////////////////////////
        //Mo ta:        Ham gan image calendaer popup voi textbox
        //Ngay tao:     05/06/2008
        //Nguoi tao:    Huypq7
        //Dau vao:      pv_CtrlDate: Xau can kiem tra
        //              pv_CtrlPopup: image
        //Dau ra:       Thanh cong
        /////////////////////////////////////////////////////////////////
        public void gs_SetDate(TextBox pv_CtrlDate, Image pv_CtrlPopup)
        {
            string sv_sDisFormat="";

            try
            {
                sv_sDisFormat = Convert.ToString(gc_sDisplayFormat).ToLower();
                if (sv_sDisFormat == "")
                {
                    sv_sDisFormat = "dd/mm/yyyy";
                }
                pv_CtrlPopup.Attributes.Add("Style", "CURSOR: hand");
                pv_CtrlPopup.Attributes.Add("OnClick", "showCalendar(" + pv_CtrlDate.ClientID +
                    "," + pv_CtrlDate.ClientID + ", '" + sv_sDisFormat + "',null,1,-1,-1,true);");
            }
            catch (Exception ex)
            {
                strError =  ex.Message;
            }
        }

    }
}
