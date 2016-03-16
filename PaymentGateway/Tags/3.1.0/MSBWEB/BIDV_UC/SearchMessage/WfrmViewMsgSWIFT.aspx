<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WfrmViewMsgSWIFT.aspx.cs" Inherits="BIDVWEB.BIDV_UC.SearchMessage.WfrmViewMsgSWIFT" Title="MSBGW_WEB" %>
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
		    padding-right: 5px;
		    border-top: #f5f5f5 1px solid;
		    padding-left: 5px;
		    padding-bottom: 5px;
		    border-left: #f5f5f5 1px solid;
		    padding-top: 5px;
		    border-bottom: buttonshadow 1px solid;
		    background-color: #ffffff;
	    }        
    </style>
    <script type="text/javascript">
        function set_interval()
        {
            //the interval 'timer' is set as soon as the page loads
            var timer=setInterval("auto_logout()",600000);
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
            var timer=setInterval("auto_logout()",600000);
        }

        function auto_logout()
        {
            //this function will redirect the user to the logout script
            //alert("You have been logged out and forwarded to Login Page");
            window.close();       
            
            //alert("You are IDLE For more than 1 minutes,So,You have been logged out and forwarded to Login Page");
            //window.forward("/login.jsp");
        }
    </script>     
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">    
    <table id="Table4" runat="server" width="750px" class="DialogContent">
        <tr>
            <td align="center" colspan="2" class="DialogCaption" style="height:25px">
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="ĐIỆN SWIFT" 
            ForeColor="White"></asp:Label>
            </td>
        </tr>    
        <tr>
            <td style="width:100%">
               <table id="Table1" runat="server" width="100%" >
                    <tr align="left">
                        <td style="width:15%">
                            <asp:Label ID="Label2" runat="server" Text="Số RM"></asp:Label>
                        </td>
                        <td style="width:35%">                    
                            <asp:TextBox ID="txtRM" runat="server" Width="150px" 
                                Height="20px" TabIndex="1" ReadOnly="True"></asp:TextBox>
                        </td>        
                        <td style="width:15%">
                            <asp:Label ID="Label11" runat="server" Text="Số Ref"></asp:Label>
                        </td>
                        <td style="width:35%">
                            <asp:TextBox ID="txtRefNo" runat="server" Width="150px" 
                                Height="20px" TabIndex="11" ReadOnly="True">
                            </asp:TextBox>
                            <asp:TextBox ID="txtMsgDirection" runat="server" Width="20px" 
                                Height="20px" TabIndex="11" ReadOnly="True" Visible="false">
                            </asp:TextBox>
                        </td>                  
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Loại điện"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMsgType" runat="server" Width="150px" 
                                Height="20px" TabIndex="4" ReadOnly="True"></asp:TextBox>
                        </td>                                                            
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Phân hệ"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDepartment" runat="server" Width="150px" ReadOnly="True" 
                                Height="20px"></asp:TextBox>
                        </td>                   
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Ngân hàng gửi"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSender" runat="server" Width="150px" 
                                Height="20px" TabIndex="7" ReadOnly="True"></asp:TextBox>                            
                        </td>                         
                        <td colspan="2">
                            <asp:Label ID="lblSender" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Ngân hàng nhận"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReceiver" runat="server" Width="150px" 
                                Height="20px" TabIndex="9" ReadOnly="True"></asp:TextBox>                            
                        </td>                        
                        <td colspan="2">
                            <asp:Label ID="lblReceiver" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Số tiền"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" Width="150px" 
                                Height="20px" TabIndex="13" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="lblCCY" runat="server"></asp:Label>
                        </td>               
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Trạng thái"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStatus" runat="server" Width="150px" ReadOnly="True" 
                                Height="20px"></asp:TextBox>
                        </td>
                    </tr>                         
                    <tr align="left">
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="TT in điện"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrintSTS" runat="server" Width="150px" ReadOnly="True" 
                                Height="20px"></asp:TextBox>
                        </td>          
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="TT xử lý điện"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProcessSTS" runat="server" Width="150px" ReadOnly="True" 
                                Height="20px"></asp:TextBox>
                        </td>
                    </tr>            
                </table>                 
            </td>
        </tr>
        <tr>
            <td style="width:100%" valign="top">
                <asp:TextBox Width="100%" ID="txtContent" runat="server" Height="250px" 
                TextMode="MultiLine" ReadOnly="True" ></asp:TextBox>                        
            </td>
        </tr>
        <tr>
            <td style="width:100%" valign="top" align="center">
                 
                <asp:DropDownList ID="ddlLang" runat="server" Width="100px" Visible="false">
                </asp:DropDownList>
&nbsp;
                 
                <asp:Button ID="btnPrint" runat="server" Height="25px" onclick="btnPrint_Click" 
                    Text="In điện" Width="100px" />
                 
            </td>
        </tr>
    </table>        
    </div>
    </form>
</body>
</html>

