<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmChangePassword.aspx.cs" Inherits="BIDVWEB.BIDV_UC.UserRight.WfrmChangePassword" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
    <br>
        <table style="width: 740px" cellpadding="0" cellspacing="0" 
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
                        Width="140px" TabIndex="1" MaxLength="100"></asp:TextBox>
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
                        TabIndex="2" MaxLength="100"></asp:TextBox>
    &nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtPassword" 
                        ControlToValidate="txtRetype" ErrorMessage="CompareValidator">*</asp:CompareValidator>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="Black" 
                        Text="Xác nhận mật khẩu:"></asp:Label>                
                </td>
                <td>
                    <asp:TextBox ID="txtRetype" runat="server" TextMode="Password" Width="140px" 
                        TabIndex="3" MaxLength="100"></asp:TextBox>
    &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" 
                        ControlToValidate="txtRetype" ErrorMessage="CompareValidator">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnChange" runat="server" onclick="btnChange_Click" 
                        Text="Ghi" Height="25px" Width="100px" TabIndex="4" 
                        CausesValidation="False" />
                </td>
            </tr>
        </table>            
    </div>
</asp:Content>            
