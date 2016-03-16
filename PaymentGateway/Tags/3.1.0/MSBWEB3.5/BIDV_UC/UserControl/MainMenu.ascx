<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.ascx.cs" Inherits="BIDVWEB.BIDV_UC.UserControl.MainMenu" %>
<asp:Menu ID="Menu1" runat="server">
    <Items>
        <asp:MenuItem Text="Trang chủ" Value="Trang chủ"></asp:MenuItem>
        <asp:MenuItem Text="Quản trị hệ thống" Value="Quản trị hệ thống">
            <asp:MenuItem Text="Quản lý người dùng" Value="Quản lý người dùng">
            </asp:MenuItem>
            <asp:MenuItem Text="Quản lý tham số hệ thống" Value="Quản lý tham số hệ thống">
            </asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/Common/ChangePassword.aspx" 
            Text="Thay đổi mật khẩu" Value="Thay đổi mật khẩu"></asp:MenuItem>
        <asp:MenuItem Text="Báo cáo" Value="Báo cáo">
            <asp:MenuItem Text="IBPS" Value="IBPS"></asp:MenuItem>
            <asp:MenuItem Text="TTSP" Value="TTSP"></asp:MenuItem>
            <asp:MenuItem Text="VCB" Value="VCB"></asp:MenuItem>
            <asp:MenuItem Text="Swift" Value="Swift" 
                NavigateUrl="~/ManagementReport/SW05Main.aspx"></asp:MenuItem>
        </asp:MenuItem>
    </Items>
</asp:Menu>
