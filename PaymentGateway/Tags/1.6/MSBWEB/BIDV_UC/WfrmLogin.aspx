<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WfrmLogin.aspx.cs" Inherits="BIDVWEB.BIDV_UC.WfrmLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
        .style2
        {
            height: 133px;
        }
        .style3
        {
            width: 249px;
            height: 133px;
        }
        .style6
        {
            height: 15px;
        }
        .style8
        {
            width: 30%;
            height: 21px;
        }
        .style9
        {
            width: 58%;
            height: 21px;
        }
        .style11
        {
            height: 19px;
            width: 58%;
        }
        .style12
        {
            height: 11px;
        }
        .style13
        {
            width: 30%;
            height: 19px;
        }
        .style14
        {
            width: 58%;
        }
    </style>
    <script type="text/javascript">
        function DisableBackButton(isBool) {
            if(isBool) {
	            //do actual submit;
            } else{	            
	            location.href="../WfrmLogin.aspx";
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        window.history.forward();        
        function DisableBack() 
        { 
            window.history.forward(1);
        }
        function noBack()
        { 
            window.history.forward(); 
        }
    </script> 
    <script type="text/javascript">        
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 100%; height: 376px;">
            <tr>
                <td class="style2" style="width:33%">
                </td>
                <td class="style3" style="width:34%">
                    &nbsp;</td>
                <td class="style2" style="width:33%">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align = "center" style="width:300px" valign ="top">
                    <table style="width:100%; height: 140px;" cellpadding="0" 
                        cellspacing="0" class="DialogContent" border="0">
                        <tr class="DialogCaption">
                            <td style="width:30%" align="left" >                                                            
                                <!--
                                <img src="../Images/usmenu.gif" 
                                style="width: 20px; height: 20px; margin-left: 0px;" alt="Image" />
                                -->
                            </td>
                            <td align="left" class="style14">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="ĐĂNG NHẬP" 
                                    ForeColor="White" Height="22px" Width="115px"></asp:Label>                            
                            </td>
                        </tr>                        
                        <tr>
                            <td align ="left" valign="bottom" class="style8">
                                <asp:Label ID="Label2" runat="server" Text="Mã NSD"></asp:Label>
                            </td>
                            <td align ="left" valign="bottom" class="style9">
                                <asp:TextBox ID="txtUserID" runat="server" Height="20px" Width="140px" 
                                    MaxLength="12" TabIndex="1" ReadOnly="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtUserID" Display="Dynamic" ErrorMessage="Nhập mã NSD">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align ="left" valign="middle" class="style13">
                                <asp:Label ID="Label3" runat="server" Text="Mật khẩu"></asp:Label>
                            </td>
                            <td align ="left" valign="middle" class="style11">
                                <asp:TextBox ID="txtPass" runat="server" Height="20px" TextMode="Password" 
                                    Width="140px" MaxLength="100" TabIndex="2" ReadOnly="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtPass" Display="Dynamic" ErrorMessage="Nhập mật khẩu">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align ="right" valign="bottom" class="style12">                                
                                <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" Height="25px" 
                                    Width="100px" onclick="btnLogin_Click" TabIndex="3" />                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align ="left" valign="top" class="style6">                                
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Error"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
