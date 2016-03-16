<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSetPerReport.aspx.cs" Inherits="BIDVWEB.BIDV_UC.UserRight.WfrmSetPerReport" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
    <br/>
        <table cellpadding="0" cellspacing="0" border="0" width="740px" class="DialogContent">
            <tr>
                <td class="DialogCaption" align="center" colspan="2" style="height:25px" >
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="PHÂN QUYỀN BÁO CÁO" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left" >
                <td valign ="middle" style="width:25%" align="left">
                    <asp:Label ID="Label5" runat="server" Text="Chọn chi nhánh"></asp:Label>
                </td>
                <td valign ="middle" style="width:75%" align="left">
                    <asp:DropDownList ID="ddlBranch" runat="server" Height="22px" 
                        Width="140px" AutoPostBack="true" 
                        onselectedindexchanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr align="left" >
                <td valign ="middle" style="width:25%" align="left">
                    <asp:Label ID="Label2" runat="server" Text="Chọn nhóm"></asp:Label>
                </td>
                <td valign ="middle" style="width:75%" align="left">
                    <asp:DropDownList ID="ddlGroup" runat="server" Height="22px" 
                        Width="140px" AutoPostBack="true" 
                        onselectedindexchanged="ddlGroup_SelectedIndexChanged" TabIndex="1">
                    </asp:DropDownList>                
                </td>
            </tr>
             <tr align="left" >
                <td valign ="middle">
                    <asp:Label ID="Label4" runat="server" Text="Kênh thanh toán"></asp:Label>
                </td>
                <td valign ="middle">
                    <asp:DropDownList ID="ddlType" runat="server" Height="22px" 
                        Width="140px" AutoPostBack="true" 
                        onselectedindexchanged="ddlType_SelectedIndexChanged" TabIndex="1">
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr align="left">
                <td valign="top" style="width:50%" colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Danh sách báo cáo"></asp:Label>
                </td>                           
            </tr>
            <tr align="center">
                <td valign="top" style="width:50%" colspan="2">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        Width="100%" AllowPaging="True" AllowSorting="false" 
                        OnPageIndexChanging="grvData_PageIndexChanging"   
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Vertical" PageSize="10" >
                        <Columns>                            
                            <asp:TemplateField HeaderText="Chọn">
                                <HeaderTemplate>
                                    <asp:Label ID="Chon" Text="Chọn" runat="server">
                                    </asp:Label>
                                    <asp:CheckBox ID="chkALL" 
                                    runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkALL_CheckedChanged1" /> 
                                </HeaderTemplate>                                                                
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1"
                                    runat="server"
                                    OnCheckedChanged="CheckBox1_CheckedChanged1" />                                    
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID_REPORT" HeaderText="Mã">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TITLE" HeaderText="Tên">                                
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Tạo báo cáo">
                                <HeaderTemplate>
                                    <asp:Label ID="ChonXem" Text="Tạo báo cáo" runat="server">
                                    </asp:Label>
                                    <asp:CheckBox ID="chkALLView"
                                    runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkALLView_CheckedChanged1" /> 
                                </HeaderTemplate>                                
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk1" runat="server" />
                                </ItemTemplate>                                
                                <ItemStyle HorizontalAlign="Center" Width="100px" />                                
                            </asp:TemplateField>
                        </Columns>
                        <rowstyle backcolor="#EEEEEE" forecolor="Black" />
                        <pagerstyle backcolor="#999999" forecolor="Black" horizontalalign="Center" />
                        <selectedrowstyle backcolor="#008A8C" font-bold="True" forecolor="White" />                        
                        <HeaderStyle BackColor="#3399FF" ForeColor="Yellow" />
                        <alternatingrowstyle backcolor="#DCDCDC" />
                    </asp:GridView>
                </td>                               
            </tr>
            <tr align="left">
                <td colspan="2">
                    <asp:Label ID="lblError" runat="server" Text="Error" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr align="right">
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Ghi" Width="100px" 
                        onclick="btnSave_Click" Height="25px" TabIndex="3" />                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>    


