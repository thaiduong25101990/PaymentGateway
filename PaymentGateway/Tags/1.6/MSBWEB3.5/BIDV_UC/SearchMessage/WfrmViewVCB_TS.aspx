<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WfrmViewVCB_TS.aspx.cs" Inherits="BIDVWEB.BIDV_UC.SearchMessage.WfrmViewVCB_TS" Title="MSBGW_WEB" %>
<!--
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type ="text/javascript" src="../Scripts/popcalendar.js"></script>
-->
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>MSBGW_WEB</title>        
    <style type="text/css">
        .DialogCaption
	    {
		    BORDER-RIGHT: buttonshadow 1px solid; 
		    PADDING-RIGHT: 5px; 
		    BORDER-TOP: #f5f5f5 1px solid; 
		    PADDING-LEFT: 2px; 
		    PADDING-BOTTOM: 0px; 
		    BORDER-LEFT: #f5f5f5 1px solid; 
		    PADDING-TOP: 0px; 
		    BORDER-BOTTOM: buttonshadow 1px solid; 
		    BACKGROUND-COLOR: #7aa0e6;
		    height: 10px; 
	    }   
        .DialogContent
	    {
		    border-right: buttonshadow 1px solid;
		    padding-right: 0px;
		    border-top: #f5f5f5 1px solid;
		    padding-left: 0px;
		    padding-bottom: 0px;
		    border-left: #f5f5f5 1px solid;
		    padding-top: 0px;
		    border-bottom: buttonshadow 1px solid;
		    background-color: #ffffff;
	    }                
    </style>
    <script type="text/javascript">
        function set_interval()
        {
            //the interval 'timer' is set as soon as the page loads
            var timer=setInterval("auto_logout()",120000);
            // the figure '10000' above indicates how many milliseconds the timer be set to.
            //Eg: to set it to 5 mins, calculate 5min= 5x60=300 sec = 300,000 millisec. So set it to 3000000
        }

        function reset_interval()
        {
            //resets the timer. The timer is reset on each of the below events:
            // 1. mousemove 2. mouseclick 3. key press 4. scroliing
            //first step: clear the existing timer
            var timer;
            clearInterval(timer);
            //second step: implement the timer again
            var timer=setInterval("auto_logout()",120000);
        }

        function auto_logout()
        {
            //this function will redirect the user to the logout script
            //alert("You have been logged out and forwarded to Login Page");
            //window.close();
            self.close();
            //alert("You are IDLE For more than 1 minutes,So,You have been logged out and forwarded to Login Page");
            //window.forward("/login.jsp");
        }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-weight: 700">

        <table id="Table1" runat="server" width="95%" class="DialogContent" 
        cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td align="center" colspan="4" class="DialogCaption" style="height:25px">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="ĐIỆN TRA SOÁT NỘI BỘ VCB" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="width:15%">
                    <asp:Label ID="Label3" runat="server" Text="Ngày tạo"></asp:Label>
                </td>
                <td style="width:35%">                    
                    <asp:TextBox ID="txtCreateDate" runat="server" Width="150px" 
                        Height="18px" TabIndex="1" ReadOnly="false"></asp:TextBox>
                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="2" />
                </td>    
                <td style="width:15%">
                    <asp:Label ID="Label1" runat="server" Text="Số tiền"></asp:Label>
                </td>
                <td style="width:35%">
                    <asp:TextBox ID="txtAmount" runat="server" Width="150px" 
                        Height="18px" TabIndex="7" ReadOnly="True"></asp:TextBox>                
                    <asp:TextBox ID="txtID" runat="server" Height="18px" 
                        TabIndex="7" ReadOnly="True" Visible="false" Width="16px"></asp:TextBox>
                    <asp:TextBox ID="txtQUERY_ID" runat="server" Height="18px" 
                        TabIndex="7" ReadOnly="True" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Số tham chiếu"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRefNo" runat="server" Width="150px" 
                        Height="18px" TabIndex="3" ReadOnly="false"></asp:TextBox>
                    <asp:Button ID="btnFind" runat="server" Height="25px" Text="..." 
                        OnClick = "btnFind_Click" />
                </td>                   
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Loại tiền"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCCYCD" runat="server" Width="150px" 
                        Height="18px" TabIndex="20" ReadOnly="True"></asp:TextBox>
                </td>             
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label17" runat="server" Text="Field20"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtField20" runat="server" Width="150px" 
                        Height="18px" TabIndex="3" ReadOnly="false"></asp:TextBox>
                </td> 
                <td>
                    <asp:Label ID="Label13" runat="server" Text="CN gửi tra soát"></asp:Label>                           
                </td>
                <td>
                    <asp:DropDownList ID="ddlBrSend" runat="server" Height="16px" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Tên ĐV yêu cầu"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDVYC" runat="server" Width="250px" Height="18px" TabIndex="13" ReadOnly="True">
                    </asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="CN nhận tra soát"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBrReceive" runat="server" Height="16px" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Tên ĐV hưởng"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDVH" runat="server" Width="250px" 
                        Height="18px" TabIndex="11" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Trạng thái"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Height="16px" Width="143px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label11" runat="server" Text="TK đơn vị hưởng"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTKDVH" runat="server" Width="250px" 
                        Height="18px" TabIndex="11" ReadOnly="True"></asp:TextBox>
                </td>
                <td>                    
                    <asp:Label ID="Label9" runat="server" Text="CN tạo điện"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCNTD" runat="server" Width="250px" 
                        Height="18px" TabIndex="11" ReadOnly="True"></asp:TextBox>                     
                </td>
            </tr>            
            <tr align="left">
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Ngân hàng hưởng"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNHH" runat="server" Width="250px" 
                        Height="18px" TabIndex="11" ReadOnly="True"></asp:TextBox>
                </td>
                <td>                    
                    <asp:Label ID="Label10" runat="server" Text="CN nhận điện"></asp:Label>
                </td>
                <td>                
                    <asp:TextBox ID="txtCNND" runat="server" Width="250px" 
                        Height="18px" TabIndex="11" ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="txtID_PARENT" runat="server" Visible ="false"
                        Height="18px" TabIndex="11" ReadOnly="True" Width="16px">
                    </asp:TextBox>
                    <asp:TextBox ID="txtIORDER" runat="server" Visible ="false"
                        Height="18px" TabIndex="11" ReadOnly="True" Width="16px">
                    </asp:TextBox> 
                    
                </td>
            </tr>            
            <tr align="left">
                <td>
                    <asp:Label ID="Label18" runat="server" Text="Số lệnh TS">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSBT_ID" runat="server" 
                        Height="18px" TabIndex="11" ReadOnly="True" Width="150px">
                    </asp:TextBox>
                </td>
                <td>                    
                    <asp:Label ID="Label16" runat="server" Text="Chiều điện" Visible="false">
                    </asp:Label>                    
                </td>
                <td>             
                    <asp:DropDownList ID="ddlDirection" runat="server" Height="16px" Width="143px"
                     Visible="false">
                    </asp:DropDownList>                      
                </td>
            </tr>            
            <tr>
                <td style="width:100%" colspan="4">
                    <asp:Label ID="Label29" runat="server" Text="Nội dung tra soát"></asp:Label>
                </td>
           </tr>
           <tr>
                <td style="width:100%" align="left" valign="top" colspan="4">
                    <asp:TextBox ID="txtContent_TS" runat="server" Height="50px" TextMode="MultiLine" 
                        Width="875px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:100%" colspan="4">
                    <asp:Label ID="Label8" runat="server" Text="Nội dung tra soát"></asp:Label>
                </td>
           </tr>
           <tr>
                <td style="width:100%" align="left" valign="top" colspan="4">
                    <asp:TextBox ID="txtContent" runat="server" Height="80px" TextMode="MultiLine" 
                        Width="875px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>            
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnSave" runat="server" Text="Ghi" Height="25px" 
                        Width="100px" onclick="btnSave_Click" />
                    <asp:Button ID="btnApprove" runat="server" Text="Duyệt" Height="25px" 
                        Width="100px" onclick="btnApprove_Click" />
                    <asp:Button ID="btnClose" runat="server" Text="Đóng" Height="25px" 
                        Width="100px" onclick="btnClose_Click" Visible="false" />
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="100px" 
                        onclick="btnPrint_Click" Height="25px" TabIndex="20" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

