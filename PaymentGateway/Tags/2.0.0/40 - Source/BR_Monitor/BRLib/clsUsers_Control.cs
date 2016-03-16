using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace BR.BRLib
{
    public class clsUsers_Control
    {
        public static int _Exist;
        //kiem tra va thuc hien an hien cac control theo muc dich cua mot form
        public static void Users_Control(Form _Form ,DataGridView _datGrid,bool _Edit, bool _Delete, bool _insert, bool _Cancel,String []arr)
        {
            try
            {
                int i = 0;
                while (i < _Form.Controls.Count)
                {
                    if (_Form.Controls[i] is TextBox)//neu control la textbox
                    {
                        _Form.Controls[i].Enabled = false;
                    }
                    else if (_Form.Controls[i] is Button)//neu control la button
                    {
                        if (_datGrid.Rows.Count > 0)//neu luoi khong co du lieu
                        {
                            //kiem tra tiep dieu kien
                            if (_Edit == false && _Delete == false && _insert == false && _Cancel == false)//khong xay ra insert ,delete,update
                            {
                                Check_Exist(_datGrid, _Form.Controls[i], arr);
                            }
                            else
                            {
                                //else if (_Edit == true)
                                //{ }
                                //else if (_Delete == true)
                                //{ }
                                //else if (_insert == true)
                                //{ }

                            }
                        }
                        else if (_datGrid.Rows.Count > 0)
                        {
                        }
                       
                    }
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
        //kiem tra control xem co trung voi gia tri mac dinh con trol khong
        private static void Check_Exist(DataGridView _datGrid, Control _bt, String[] _arr)
        {
            try
            {
                for (int j = 0; j < _arr.Length; j++)//kiem tra mang
                {
                    if (_bt.Name == _arr[j].ToString())
                    {
                        _Exist = 1;
                        break;
                    }
                }
                if (_Exist == 0)//neu control nay khong trung voi gia tri trong mang
                {
                    if (_datGrid.Rows.Count == 0)//neu luoi khong co du lieu
                    {
                        _bt.Enabled = false;
                    }
                    else
                    {
                        _bt.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex.Message, 2, MessageBoxButtons.OK);
            }
        }
    }
}
