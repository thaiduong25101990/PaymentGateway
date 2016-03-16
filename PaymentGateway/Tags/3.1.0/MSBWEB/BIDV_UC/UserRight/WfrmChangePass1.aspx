<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WfrmChangePass1.aspx.cs" Inherits="BIDVWEB.BIDV_UC.UserRight.WfrmChangePass1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 500px" cellpadding="0" cellspacing="0" 
                class="DialogContent">
            <tr>
                <td class="DialogCaption" colspan="2" align="center"  style="height:25px">                
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="White" 
                        Text="THAY ĐỔI MẬT KHẨU"></asp:Label>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Error"></asp:Label>
                </td>
            </tr>
            <tr>                        
                <td style="width: 100%" colspan="2" align="left">
                    <asp:Label ID="lblHeader" runat="server" Font-Bold="False" 
                        Text="Hãy nhập mật khẩu cũ và mật khẩu mới."></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>                
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" ForeColor="Black" 
                        Text="Mật khẩu cũ:"></asp:Label>                      
                </td>
                <td>
                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" 
                        Width="140px" TabIndex="1"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtOldPassword" ErrorMessage="Chưa nhập mật khẩu." 
                        Display="Dynamic">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr align="left">
                <td>                
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" ForeColor="Black" 
                        Text="Mật khẩu mới:"></asp:Label>                                
                </td>
                <td style="width: 312px">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="140px" 
                        TabIndex="2"></asp:TextBox>
    &nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtPassword" 
                        ControlToValidate="txtRetype" 
                        ErrorMessage="Mật khẩu và xác nhận mật khẩu không trùng nhau">*</asp:CompareValidator>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="Black" 
                        Text="Xác nhận mật khẩu:"></asp:Label>                
                </td>
                <td>
                    <asp:TextBox ID="txtRetype" runat="server" TextMode="Password" Width="140px" 
                        TabIndex="3"></asp:TextBox>
    &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" 
                        ControlToValidate="txtRetype" 
                        ErrorMessage="Mật khẩu và xác nhận mật khẩu không trùng nhau">*</asp:CompareValidator>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnChange" runat="server" onclick="btnChange_Click" 
                        Text="Ghi" Height="25px" Width="100px" TabIndex="4" 
                        CausesValidation="False" />
                    <asp:Button ID="btnCancel" runat="server" 
                        Text="Thoát" onclick="btnCancel_Click1" onclientclick="true" Height="25px" 
                        Width="100px" TabIndex="5" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
