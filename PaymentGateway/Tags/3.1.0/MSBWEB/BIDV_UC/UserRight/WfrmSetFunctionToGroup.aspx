<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSetFunctionToGroup.aspx.cs" Inherits="BIDVWEB.BIDV_UC.UserRight.WfrmSetFunctionToGroup" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div width:100%" align="center">
    <br/>
        <table cellpadding="0" cellspacing="0" border="0" width="740px" class="DialogContent">
            <tr>
                <td class="DialogCaption" align="center" style="height:25px">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="PHÂN QUYỀN CHO NHÓM" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left" >                
                <td valign ="middle" style="width:100%">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td style="width:15%">
                                <asp:Label ID="Label4" runat="server" Text="Chọn chi nhánh"></asp:Label>
                            </td>
                            <td style="width:35%">
                                <asp:DropDownList ID="ddlBranch" runat="server" Height="16px" 
                                    Width="140px" AutoPostBack="true" 
                                    onselectedindexchanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td style="width:15%">
                                <asp:Label ID="Label5" runat="server" Text="Chọn kênh TT"></asp:Label>
                            </td>
                            <td style="width:35%">
                                <asp:DropDownList ID="ddlGWType" runat="server" Height="16px" 
                                    Width="140px" AutoPostBack="true" 
                                    onselectedindexchanged="ddlGWType_SelectedIndexChanged" TabIndex="2">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>        
                </td> 
            </tr>            
            <tr align="left" >                
                <td valign ="middle" style="width:100%">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td style="width:15%">
                                <asp:Label ID="Label2" runat="server" Text="Chọn nhóm"></asp:Label>
                            </td>
                            <td style="width:85%">
                                <asp:DropDownList ID="ddlGroup" runat="server" Height="16px" 
                                    Width="140px" AutoPostBack="true" 
                                    onselectedindexchanged="ddlGroup_SelectedIndexChanged" TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>        
                </td> 
            </tr>
            <tr align="left">
                <td valign="bottom">
                    <asp:Label ID="Label3" runat="server" Text="Danh sách menu" Height="16px"></asp:Label>
                </td>                           
            </tr>
            <tr align="center">
                <td valign="top">
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
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1"
                                    runat="server"
                                    OnCheckedChanged="CheckBox1_CheckedChanged1" />                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MENUID" HeaderText="Mã" >
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CAPTION" HeaderText="Tên chức năng" />
                            <asp:TemplateField HeaderText="Xem">
                                <HeaderTemplate>
                                    <asp:Label ID="ChonXem" Text="Xem" runat="server">
                                    </asp:Label>
                                    <asp:CheckBox ID="chkALLView"
                                    runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkALLView_CheckedChanged1" /> 
                                </HeaderTemplate> 
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk1" runat="server" />
                                </ItemTemplate>                                
                                <ItemStyle HorizontalAlign="Center" Width="50px" />                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Xóa">
                                <HeaderTemplate>
                                    <asp:Label ID="ChonXoa" Text="Xóa" runat="server">
                                    </asp:Label>
                                    <asp:CheckBox ID="chkALLDel"
                                    runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkALLDel_CheckedChanged1" /> 
                                </HeaderTemplate> 
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk2" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thêm mới">
                                <HeaderTemplate>
                                    <asp:Label ID="ChonThemmoi" Text="Thêm mới" runat="server">
                                    </asp:Label>
                                    <asp:CheckBox ID="chkALLAdd"
                                    runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkALLAdd_CheckedChanged1" /> 
                                </HeaderTemplate> 
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk3" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sửa/duyệt">
                                <HeaderTemplate>
                                    <asp:Label ID="ChonSua" Text="Sửa/duyệt" runat="server">
                                    </asp:Label>
                                    <asp:CheckBox ID="chkALLEdit"
                                    runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkALLEdit_CheckedChanged1" /> 
                                </HeaderTemplate> 
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk4" runat="server" />
                                </ItemTemplate>
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
            <tr align="right">
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Ghi" Width="100px" 
                        onclick="btnSave_Click" Height="25px" TabIndex="3" />                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>    


