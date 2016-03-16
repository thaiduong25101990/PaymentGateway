<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WfrmViewMsgIBPS.aspx.cs" Inherits="BIDVWEB.BIDV_UC.SearchMessage.WfrmViewMsgIBPS" Title="MSBGW_WEB" %>
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
        <table id="Table1" runat="server" width="100%" class="DialogContent" 
        cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td align="center" colspan="4" class="DialogCaption" style="height:25px">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="ĐIỆN IBPS" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="width:15%">
                    <asp:Label ID="Label3" runat="server" Text="Mã loại giao dịch"></asp:Label>
                </td>
                <td style="width:45%">                    
                    <asp:TextBox ID="txtTransCode" runat="server" Width="150px" 
                        Height="18px" TabIndex="1" ReadOnly="True"></asp:TextBox>
                    <asp:Label ID="lblDes" runat="server" Font-Size="X-Small"></asp:Label>
                </td>    
                <td style="width:15%">
                    <asp:Label ID="Label13" runat="server" Text="Ngày giao dịch"></asp:Label>                           
                </td>
                <td style="width:25%">
                    <asp:TextBox ID="txtTransDate" runat="server" ReadOnly="True" Height="18px" 
                        Width="150px" TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Số bút toán"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRefNo" runat="server" Width="150px" 
                        Height="18px" TabIndex="3" ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="txtMsgDirection" runat="server" Width="20px" 
                        Height="18px" TabIndex="3" ReadOnly="True" Visible="false">
                    </asp:TextBox>
                </td>                   
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Phân hệ"></asp:Label>                           
                </td>
                <td>
                    <asp:TextBox ID="txtDepartment" runat="server" ReadOnly="True" Height="18px" 
                        Width="150px" TabIndex="4"></asp:TextBox>
                </td>             
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Ngân hàng gửi"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSender" runat="server" Width="150px" 
                        Height="18px" TabIndex="7" ReadOnly="True"></asp:TextBox>                
                    <asp:Label ID="lblSender" runat="server" Font-Size="X-Small"></asp:Label>
                </td> 
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Trạng thái"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStatus" runat="server" ReadOnly="True" Height="18px" 
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Ngân hàng nhận"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReceiver" runat="server" Width="150px" 
                        Height="18px" TabIndex="20" ReadOnly="True"></asp:TextBox>
                    <asp:Label ID="lblReceiver" runat="server" Font-Size="X-Small"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Thời gian gửi"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSendingTime" runat="server" ReadOnly="True" Height="18px" 
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Số tiền"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" Width="150px" 
                        Height="18px" TabIndex="13" ReadOnly="True" 
                        ></asp:TextBox>
                    <asp:Label ID="lblCCY" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Thời gian nhận"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReceivingTime" runat="server" ReadOnly="True" Height="18px" 
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Số RM"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRM" runat="server" Width="150px" 
                        Height="18px" TabIndex="11" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    
                    <asp:Label ID="Label30" runat="server" Text="Trạng thái in điện"></asp:Label>
                </td>
                <td>
                    
                    <asp:TextBox ID="txtPrintSTS" runat="server" ReadOnly="True" Height="18px" 
                        Width="150px" TabIndex="11"></asp:TextBox>
                </td>
            </tr>            
        </table>    
        <table id="Table2" runat="server" width="100%" class="DialogContent" 
        cellpadding="0" cellspacing="0" border="0">
            <tr align="left">
                <td style="width:15%">
                    <asp:Label ID="Label17" runat="server" Text="Mã loại giao dịch"></asp:Label>
                &nbsp;cũ</td>
                <td style="width:45%">                    
                    <asp:TextBox ID="txtOldTransCode" runat="server" Width="150px" 
                        Height="18px" TabIndex="1" ReadOnly="True"></asp:TextBox>
                </td>    
                <td style="width:15%">
                    <asp:Label ID="Label18" runat="server" Text="Trạng thái forward"></asp:Label>                           
                </td>
                <td style="width:25%">
                    <asp:TextBox ID="txtForwardStatus" runat="server" ReadOnly="True" Height="18px" 
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label19" runat="server" Text="Số bút toán cũ"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOldGwTranNum" runat="server" Width="150px" 
                        Height="18px" TabIndex="4" ReadOnly="True"></asp:TextBox>
                </td>                   
                <td>
                    <asp:Label ID="Label20" runat="server" Text="Thời gian Forward"></asp:Label>                           
                </td>
                <td>
                    <asp:TextBox ID="txtForwardTime" runat="server" ReadOnly="True" Height="18px" 
                        Width="150px"></asp:TextBox>
                </td>             
            </tr>
            <tr align="left">
                <td>
                    
                    <asp:Label ID="Label11" runat="server" Text="Giao dịch viên"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtTeller" runat="server" Width="150px" 
                        Height="18px" TabIndex="14" ReadOnly="True"></asp:TextBox>
                    
                </td> 
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>   
        <table id="Table3" runat="server" width="100%" class="DialogContent" 
        cellpadding="0" cellspacing="0" border="0">
            <tr align="left">                                                    
                <td style="width:15%">                    
                    <asp:Label ID="Label21" runat="server" Text="Tên người gửi"></asp:Label>                    
                </td>
                <td style="width:35%">
                    <asp:TextBox ID="txtSenderName" runat="server" Width="240px" ReadOnly="True" 
                        Height="18px"></asp:TextBox>
                </td>
                <td style="width:15%">
                    <asp:Label ID="Label25" runat="server" Text="Tên người nhận"></asp:Label>                        
                </td>
                <td style="width:35%">
                    <asp:TextBox ID="txtReceiverName" runat="server" ReadOnly="True" Height="18px" 
                        Width="240px"></asp:TextBox>
                </td>
            </tr>               
            <tr align="left">                                                    
                <td>                    
                    <asp:Label ID="Label22" runat="server" Text="Địa chỉ người gửi"></asp:Label>                    
                </td>
                <td>
                    <asp:TextBox ID="txtSenderAddress" runat="server" Width="240px" ReadOnly="True" 
                        Height="18px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label26" runat="server" Text="Địa chỉ người nhận"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReceiverAddress" runat="server" ReadOnly="True" 
                        Height="18px" Width="240px"></asp:TextBox>
                </td>
            </tr>               
            <tr align="left">                                                    
                <td>
                    <asp:Label ID="Label23" runat="server" Text="TK người gửi"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSenderAccount" runat="server" Width="240px" ReadOnly="True" 
                        Height="18px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label27" runat="server" Text="TK người nhận "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReceiverAcount" runat="server" ReadOnly="True" 
                        Height="18px" Width="240px"></asp:TextBox>
                </td>
            </tr>               
            <tr align="left">                                                    
                <td>
                    <asp:Label ID="Label24" runat="server" Text="Ngân hàng"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSenderBank" runat="server" Width="240px" ReadOnly="True" 
                        Height="18px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label28" runat="server" Text="Ngân hàng"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReceiverBank" runat="server" ReadOnly="True" Height="18px" 
                        Width="240px"></asp:TextBox>
                </td>
            </tr>               
        </table>                
        <table id="Table5" runat="server" width="100%" class="DialogContent" 
        cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="width:15%">
                    <asp:Label ID="Label29" runat="server" Text="Nội dung"></asp:Label>
                </td>
           </tr>
           <tr>
                <td style="width:85%" align="left" valign="top">                    
                    <asp:TextBox ID="txtContent" runat="server" Height="70px" TextMode="MultiLine" 
                        Width="875px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnPrint" runat="server" Text="In điện" Height="25px" 
                        Width="100px" onclick="btnPrint_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

