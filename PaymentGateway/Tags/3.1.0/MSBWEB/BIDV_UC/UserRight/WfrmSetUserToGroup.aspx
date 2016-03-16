<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSetUserToGroup.aspx.cs" Inherits="BIDVWEB.BIDV_UC.UserRight.WfrmSetUserToGroup" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
        <br/>
        <table cellpadding="0" cellspacing="0" border="0" width="740px" class="DialogContent">
            <tr>
                <td class="DialogCaption" align="center" colspan="3">                    
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="THÊM NSD CHO NHÓM" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left" >
                <td valign ="middle" colspan="3" style="width:100%">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td style="width:15%">
                                <asp:Label ID="Label5" runat="server" Text="Chọn chi nhánh"></asp:Label>
                            </td>
                            <td style="width:85%">
                                <asp:DropDownList ID="ddlBranch" runat="server" Height="22px" 
                                    Width="140px" AutoPostBack="true" 
                                    onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>        
                </td>
            </tr>                
            <tr align="left" >
                <td valign ="middle" colspan="3" style="width:100%">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td style="width:15%">
                                <asp:Label ID="Label2" runat="server" Text="Chọn nhóm"></asp:Label>
                            </td>
                            <td style="width:85%">
                                <asp:DropDownList ID="ddlGroup" runat="server" Height="22px" 
                                    Width="140px" AutoPostBack="true" 
                                    onselectedindexchanged="ddlGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>        
                </td>
            </tr>        
            <tr align="left">
                <td valign="top" style="width:45%">
                    <asp:Label ID="Label3" runat="server" Text="Danh sách NSD"></asp:Label>
                </td>
                <td style="width:10%">
                </td>
                <td valign="top" style="width:45%">
                    <asp:Label ID="Label4" runat="server" Text="Danh sách NSD đã chọn"></asp:Label>
                </td>                
            </tr>
            <tr align="center">
                <td valign="top" style="width:45%" align="center">
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
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="USERID" HeaderText="Mã NSD" />
                            <asp:BoundField DataField="USERNAME" HeaderText="Tên NSD" />
                            <asp:BoundField DataField="BRANCH" HeaderText="Branch" Visible="False" />                             
                        </Columns>
                        <rowstyle backcolor="#EEEEEE" forecolor="Black" />
                        <pagerstyle backcolor="#999999" forecolor="Black" horizontalalign="Center" />
                        <selectedrowstyle backcolor="#008A8C" font-bold="True" forecolor="White" />                        
                        <HeaderStyle BackColor="#3399FF" ForeColor="Yellow" />
                        <alternatingrowstyle backcolor="#DCDCDC" />
                    </asp:GridView>
                </td>
                <td style="width:10%" valign="middle">
                    <asp:Button ID="btnLeft" runat="server" Text="&gt;&gt;" Width="100px" 
                        onclick="btnLeft_Click" Height="25px" TabIndex="2" />
                    <br />
                    <asp:Button ID="btnRight" runat="server" Text="&lt;&lt;" Width="100px" 
                        onclick="btnRight_Click" Height="25px" TabIndex="3" />
                </td>
                <td valign="top" style="width:50%" align="center">
                    <asp:GridView ID="grvUserGroup" runat="server" AutoGenerateColumns="False" 
                        Width="100%" AllowPaging="True" AllowSorting="false" 
                        OnPageIndexChanging="grvUserGroup_PageIndexChanging"   
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Vertical" PageSize="10" >
                        <Columns>                            
                            <asp:TemplateField HeaderText="Chọn">   
                                <HeaderTemplate>
                                    <asp:Label ID="Chon1" Text="Chọn" runat="server">
                                    </asp:Label>
                                    <asp:CheckBox ID="chkALL1" 
                                    runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkALL1_CheckedChanged1" /> 
                                </HeaderTemplate>                             
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1"
                                     runat="server"
                                    OnCheckedChanged="CheckBox1_CheckedChanged1" />                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="USERID" HeaderText="Mã NSD" />
                            <asp:BoundField DataField="USERNAME" HeaderText="Tên NSD" />
                            <asp:BoundField DataField="BRANCH" HeaderText="Branch" Visible="False" />                             
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
                <td colspan="3">
                    <asp:Label ID="lblError" runat="server" Text="Error" ForeColor="Red"></asp:Label>
                </td>
            </tr>            
        </table>        
        </div>
</asp:Content>
    
