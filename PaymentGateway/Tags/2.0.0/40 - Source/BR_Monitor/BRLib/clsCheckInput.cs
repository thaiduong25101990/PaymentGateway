using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;


namespace BR.BRLib
{
   public class clsCheckInput
    {
        Button cmdAdd = new Button();
        Button cmdEdit = new Button();
        Button cmdDelete = new Button();
        Button cmdSave = new Button();
        Button cmdClose = new Button();
        private static ErrorProvider errorProvider = new ErrorProvider();
        public bool IsNumeric(string s)
        {
            try
            {
                Int32.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }
        

        public void LockCommand(Boolean a)
        {
            cmdAdd.Enabled = a;
            cmdDelete.Enabled = a;
            cmdEdit.Enabled = a;
            cmdSave.Enabled = !a;
        }

        


       //Muc dich: Chuyen doi tu Tieng Viet co dau (chuan Unicode) sang Tieng Viet khong co dau
       //Input: Chuoi ky tu Tieng Viet theo chuan Unicode
       //Output: Chuoi ky tu Tieng Viet khong co dau
       //Nguoi tao: HaNTT10@fpt.com.vn 
       //Ngay tao: 04/08/2008
        #region string ConvertVietnamese(string strInput): Chuyen tieng Viet co dau TCVN3 sang Khong dau
        public string ConvertVietnamese(string strInput)
        {
            StringBuilder strOut = new StringBuilder();
            if (string.IsNullOrEmpty(strInput))
                return string.Empty; // Neu input la rong thi return string.Empty

            try
            {
                for (int i = 0; i < strInput.Length; i++)
                {
                    if (strInput[i] > 30 && strInput[i] < 127)
                    {
                        strOut.Append(strInput[i]);
                    }
                    //***********************************************************************
                    else if (strInput[i] == 'Í' || strInput[i] == 'Ì' || strInput[i] == 'Ị' //Unicode
                          || strInput[i] == 'Ĩ' || strInput[i] == 'Ỉ')                      //Unicode
                    {
                        strOut.Append('I');
                    }
                    else if (strInput[i] == 'í' || strInput[i] == 'ì' || strInput[i] == 'ị' //Unicode
                          || strInput[i] == 'ĩ' || strInput[i] == 'ỉ' //Unicode
                          || strInput[i] == '×' || strInput[i] == 'Ø' || strInput[i] == 'Þ'  //TCVN
                          || strInput[i] == 'Ü')//TCVN
                    {
                        strOut.Append('i');
                    }
                    else if (strInput[i] == 'Ô' || strInput[i] == 'Ố' || strInput[i] == 'Ồ' //Unicode
                          || strInput[i] == 'Ộ' || strInput[i] == 'Ỗ' || strInput[i] == 'Ổ')//Unicode
                    {
                        strOut.Append('O');
                    }
                    else if (strInput[i] == 'ô' || strInput[i] == 'ố' || strInput[i] == 'ồ' //Unicode
                          || strInput[i] == 'ộ' || strInput[i] == 'ỗ' || strInput[i] == 'ổ')//Unicode
                    {
                        strOut.Append('o');
                    }
                    else if (strInput[i] == 'Ơ' || strInput[i] == 'Ớ' || strInput[i] == 'Ờ' //Unicode
                          || strInput[i] == 'Ợ' || strInput[i] == 'Ỡ' || strInput[i] == 'Ở'//Unicode
                          || strInput[i] == '¤' || strInput[i] == '¥')
                    {
                        strOut.Append('O');
                    }
                    else if (strInput[i] == 'ơ' || strInput[i] == 'ớ' || strInput[i] == 'ờ' //Unicode
                          || strInput[i] == 'ợ' || strInput[i] == 'ỡ' || strInput[i] == 'ở')//Unicode
                    {
                        strOut.Append('o');
                    }
                    else if (strInput[i] == 'Â' || strInput[i] == 'Ầ' || strInput[i] == 'Ấ' //Unicode
                          || strInput[i] == 'Ậ' || strInput[i] == 'Ẫ' || strInput[i] == 'Ẩ'//Unicode
                          || strInput[i] == '¡' || strInput[i] == '¢')//TCVN
                    {
                        strOut.Append('A');
                    }
                    else if (strInput[i] == 'â' || strInput[i] == 'ầ' || strInput[i] == 'ấ' //Unicode
                          || strInput[i] == 'ậ' || strInput[i] == 'ẫ' || strInput[i] == 'ẩ' //Unicode
                          || strInput[i] == 'µ' || strInput[i] == '¶'//TCVN
                          || strInput[i] == '¹' || strInput[i] == '¾'//TCVN
                          || strInput[i] == 'Ç' || strInput[i] == 'Ë' || strInput[i] == '©' //TCVN
                          || strInput[i] == '»' || strInput[i] == '¼' || strInput[i] == 'Æ' //TCVN
                          || strInput[i] == '½' || strInput[i] == '¨')
                    {
                        strOut.Append('a');
                    }
                    else if (strInput[i] == 'Ă' || strInput[i] == 'Ằ' || strInput[i] == 'Ắ' //Unicode
                          || strInput[i] == 'Ặ' || strInput[i] == 'Ẵ' || strInput[i] == 'Ẳ')//Unicode
                    {
                        strOut.Append('A');
                    }
                    else if (strInput[i] == 'ă' || strInput[i] == 'ằ' || strInput[i] == 'ắ' //Unicode
                          || strInput[i] == 'ặ' || strInput[i] == 'ẵ' || strInput[i] == 'ẳ')//Unicode
                    {
                        strOut.Append('a');
                    }
                    else if (strInput[i] == 'Á' || strInput[i] == 'À' || strInput[i] == 'Ạ' //Unicode
                          || strInput[i] == 'Ã' || strInput[i] == 'Ả')//Unicode
                    {
                        strOut.Append('A');
                    }
                    else if (strInput[i] == 'á' || strInput[i] == 'à' || strInput[i] == 'ạ' //Unicode
                          || strInput[i] == 'ã' || strInput[i] == 'ả')//Unicode
                    {
                        strOut.Append('a');
                    }
                    else if (strInput[i] == 'Ú' || strInput[i] == 'Ù' || strInput[i] == 'Ụ' //Unicode
                          || strInput[i] == 'Ũ' || strInput[i] == 'Ủ'//TCVN
                          || strInput[i] == '¦')//TCVN
                    {
                        strOut.Append('U');
                    }
                    else if (strInput[i] == 'ú' || strInput[i] == 'ù' || strInput[i] == 'ụ' //Unicode
                          || strInput[i] == 'ũ' || strInput[i] == 'ủ'//Unicode
                          || strInput[i] == 'ï' || strInput[i] == 'ñ' || strInput[i] == 'ø' //TCVN
                          || strInput[i] == 'ö' || strInput[i] == '÷' || strInput[i] == '­')//TCVN
                    {
                        strOut.Append('u');
                    }
                    else if (strInput[i] == 'Ư' || strInput[i] == 'Ừ' || strInput[i] == 'Ứ'//Unicode
                          || strInput[i] == 'Ự' || strInput[i] == 'Ữ' || strInput[i] == 'Ử')//Unicode
                    {
                        strOut.Append('U');
                    }
                    else if (strInput[i] == 'ư' || strInput[i] == 'ừ' || strInput[i] == 'ứ'//Unicode
                          || strInput[i] == 'ự' || strInput[i] == 'ữ' || strInput[i] == 'ử')//Unicode
                    {
                        strOut.Append('u');
                    }
                    else if (strInput[i] == 'Ó' || strInput[i] == 'Ò' || strInput[i] == 'Ọ' //Unicode
                          || strInput[i] == 'Õ' || strInput[i] == 'Ỏ')//Unicode
                    {
                        strOut.Append('O');
                    }
                    else if (strInput[i] == 'ó' || strInput[i] == 'ò' || strInput[i] == 'ọ' //Unicode
                          || strInput[i] == 'õ' || strInput[i] == 'ỏ' || strInput[i] == '¬'//Unicode
                          || strInput[i] == 'ß' || strInput[i] == 'ä' || strInput[i] == 'æ' //TCVN
                          || strInput[i] == 'ç' || strInput[i] == 'ë'//TCVN
                          || strInput[i] == 'å' || strInput[i] == '«' || strInput[i] == 'î') //TCVN
                    {
                        strOut.Append('o');
                    }
                    else if (strInput[i] == 'É' || strInput[i] == 'È' || strInput[i] == 'Ẹ' //Unicode
                          || strInput[i] == 'Ẽ' || strInput[i] == 'Ẻ'//Unicode
                          || strInput[i] == '£')
                    {
                        strOut.Append('E');
                    }
                    else if (strInput[i] == 'é' || strInput[i] == 'è' || strInput[i] == 'ẹ' //Unicode
                          || strInput[i] == 'ẽ' || strInput[i] == 'ẻ'//Unicode
                          || strInput[i] == 'Î' || strInput[i] == 'Ñ' || strInput[i] == 'Ï' 
                          || strInput[i] == 'Ö'|| strInput[i] == 'ª')
                    {
                        strOut.Append('e');
                    }
                    else if (strInput[i] == 'Ê' || strInput[i] == 'Ề' || strInput[i] == 'Ế' //Unicode
                          || strInput[i] == 'Ễ' || strInput[i] == 'Ể' || strInput[i] == 'Ệ')//Unicode
                    {
                        strOut.Append('E');
                    }
                    else if (strInput[i] == 'ê' || strInput[i] == 'ề' || strInput[i] == 'ế' //Unicode
                          || strInput[i] == 'ễ' || strInput[i] == 'ể' || strInput[i] == 'ệ')//Unicode
                    {
                        strOut.Append('e');
                    }
                    else if (strInput[i] == 'Y' || strInput[i] == 'Ỳ' || strInput[i] == 'Ý' //Unicode
                          || strInput[i] == 'Ỹ' || strInput[i] == 'Ỷ' || strInput[i] == 'Ỵ')//Unicode
                    {
                        strOut.Append('Y');
                    }
                    else if (strInput[i] == 'y' || strInput[i] == 'ỳ' || strInput[i] == 'ý' //Unicode
                          || strInput[i] == 'ỹ' || strInput[i] == 'ỷ' || strInput[i] == 'ỵ'//Unicode
                          || strInput[i] == 'û' || strInput[i] == 'þ' || strInput[i] == 'ü')
                    {
                        strOut.Append('y');
                    }
                    else if (strInput[i] == 'Đ' //Unicode
                          || strInput[i] == '§')
                    {
                        strOut.Append('D');
                    }
                    else if (strInput[i] == 'đ')//Unicode
                    {
                        strOut.Append('d');
                    }
                    else if (strInput[i] == '\r' || strInput[i] == '\n' || strInput[i] == '\0' 
                          || strInput[i] == '\b' || strInput[i] == '\t')
                    {
                        strOut.Append(strInput[i]);
                    }
                    //***********************************************************************
                    else
                    {
                        strOut.Append(strInput[i]);
                    }
                }
            }
            catch
            {
                return String.Empty;
            }
            return strOut.ToString();
        }
        #endregion

        //http://webketoan.com/forum/archive/index.php/t-7487.html
        // Tach dau ra khoi chuoi dung cho font VNI
        public string ConvertUnicodeWithoutMark(string Chuoi)
        {
            string Chuoimoi;
            int Dodai;
            int i;
            int Ma;
            Chuoi = Chuoi.Trim();
            Dodai = Chuoi.Length;
            Chuoimoi = "";
            for (i = 1; i <= Dodai; i++)
            {
                Ma =Convert.ToInt32(Chuoi.Substring(i,1)); //Asc(Mid(Chuoi, i, 1));
                if (Ma > 31 & Ma < 127)
                {
                    Chuoimoi = Chuoimoi + Chuoi.Substring(i, 1); //Chuoimoi + Mid(Chuoi, i, 1);
                }
                else
                {
                    switch (Chuoi.Substring(i, 1))
                    {
                        case "Ñ":
                            Chuoimoi = Chuoimoi + "D";
                            break;
                        case "ñ":
                            Chuoimoi = Chuoimoi + "d";
                            break;
                        case "Í":
                        case "Ì":
                        case "Æ":
                        case "Ó":
                        case "Ò":
                            Chuoimoi = Chuoimoi + "I";
                            break;
                        case "í":
                        case "ì":
                        case "æ":
                        case "ó":
                        case "ò":
                            Chuoimoi = Chuoimoi + "i";
                            break;
                        case "Ô":
                            Chuoimoi = Chuoimoi + "O";
                            break;
                        case "ô":
                            Chuoimoi = Chuoimoi + "o";
                            break;
                        case "Ö":
                            Chuoimoi = Chuoimoi + "U";
                            break;
                        case "ö":
                            Chuoimoi = Chuoimoi + "u";
                            break;
                    }
                }
            }
            return Chuoimoi;
        }
    }
}
