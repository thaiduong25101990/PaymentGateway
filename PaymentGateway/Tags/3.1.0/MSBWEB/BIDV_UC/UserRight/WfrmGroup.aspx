<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmGroup.aspx.cs" Inherits="BIDVWEB.BIDV_UC.UserRight.WfrmGroup" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <div align="center">    
        <br>
        <table id="Table1" runat="server" width="740px" class="DialogContent">
            <tr>
                <td align="center" colspan="4" class="DialogCaption" style="height:25px">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="THÔNG TIN NHÓM" 
                        ForeColor="White">
                    </asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Tên nhóm"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGroupName" runat="server" Width="140px" Height="22px" 
                        MaxLength="20" TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Kênh TT"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGwtype" runat="server" Height="25px" Width="140px" 
                        TabIndex="2" AutoPostBack="true"
                        onselectedindexchanged="ddlGwtype_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Chi nhánh"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBranch" runat="server" Height="22px" Width="140px" 
                     AutoPostBack="true" onselectedindexchanged="ddlBranch_SelectedIndexChanged" 
                        TabIndex="3">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblAdmin" runat="server" Text="Nhóm quản trị"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkAdmin" runat="server" TabIndex="4" />
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Mô tả"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDes" runat="server" Width="540px" Height="22px" 
                        TabIndex="5" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">                
                <td colspan="4">
                
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Error"></asp:Label>
                </td>
            </tr>
            <tr align="center">                
                <td colspan="4">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        Width="100%" AllowPaging="True" AllowSorting="false" 
                        OnPageIndexChanging="grvData_PageIndexChanging"   
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Vertical" PageSize="10" >
                        <Columns>
                            <asp:TemplateField HeaderText="Chọn">                                
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1"
                                     runat="server" AutoPostBack ="true" 
                                    OnCheckedChanged="CheckBox1_CheckedChanged1" />                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="GROUPID" HeaderText="Mã nhóm" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GROUPNAME" HeaderText="Tên nhóm" >
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GWTYPE" HeaderText="Kênh TT" >
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>                                                        
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemTemplate>
                                    <a href='../UserRight/WfrmGroup.aspx?mn=1202&GROUPID=<%# Eval("GROUPID") %>'>
                                    Chi tiết</a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
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
            <tr>
                <td align="right" colspan="4">
                    <asp:Button ID="btnAdd" runat="server" Text="Thêm mới" Width="100px" 
                        onclick="btnAdd_Click" Height="25px" TabIndex="6" />
                    <asp:Button ID="btnSave" runat="server" Text="Ghi" Width="100px" 
                        onclick="btnSave_Click" Height="25px" TabIndex="7" />
                    <asp:Button ID="btnDel" runat="server" Text="Xóa" Width="100px" 
                        onclick="btnDel_Click" Height="25px" TabIndex="8" />
                </td>
            </tr>
        </table>
    
    </div>
</asp:Content>