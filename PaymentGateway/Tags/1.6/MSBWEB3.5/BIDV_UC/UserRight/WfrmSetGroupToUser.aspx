<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSetGroupToUser.aspx.cs" Inherits="BIDVWEB.BIDV_UC.UserRight.WfrmSetGroupToUser" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
    <br/>
        <table cellpadding="0" cellspacing="0" border="0" width="740px" class="DialogContent">
            <tr>
                <td class="DialogCaption" align="center" colspan="3" style="height:25px">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="GÁN NHÓM CHO NSD" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left" >                
                <td valign ="middle" colspan="3" style="width:100%">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td style="width:15%">
                                <asp:Label ID="Label2" runat="server" Text="Chọn chi nhánh"></asp:Label>
                            </td>
                            <td style="width:85%">
                                <asp:DropDownList ID="ddlBranch" runat="server" Height="22px" 
                                    Width="150px" AutoPostBack="true" 
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
                                <asp:Label ID="Label5" runat="server" Text="Chọn NSD"></asp:Label>
                            </td>
                            <td style="width:85%">
                                <asp:DropDownList ID="ddlUser" runat="server" Height="22px" 
                                    Width="150px" AutoPostBack="true" 
                                    onselectedindexchanged="ddlUser_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>        
                </td>                              
            </tr>
            <tr align="left">
                <td valign="top" style="width:45%">
                    <asp:Label ID="Label3" runat="server" Text="Danh sách nhóm" Height="16px"></asp:Label>
                </td>
                <td style="width:10%">                    
                </td>
                <td valign="top" style="width:45%">
                    <asp:Label ID="Label4" runat="server" Text="Danh sách nhóm đã chọn" 
                        Height="16px"></asp:Label>
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
                            <asp:BoundField DataField="GROUPID" HeaderText="Mã nhóm" />
                            <asp:BoundField DataField="GROUPNAME" HeaderText="Tên nhóm" />
                        </Columns>
                        <rowstyle backcolor="#EEEEEE" forecolor="Black" />
                        <pagerstyle backcolor="#999999" forecolor="Black" horizontalalign="Center" />
                        <selectedrowstyle backcolor="#008A8C" font-bold="True" forecolor="White" />                        
                        <HeaderStyle BackColor="#3399FF" ForeColor="Yellow" />
                        <alternatingrowstyle backcolor="#DCDCDC" />
                    </asp:GridView>
                </td>
                <td style="width:10%" align="center" valign="middle">                    
                    <asp:Button ID="btnLeft" runat="server" Text="&gt;&gt;" Width="100px" 
                        onclick="btnLeft_Click" Height="25px" TabIndex="3" />
                    <br />
                    <asp:Button ID="btnRight" runat="server" Text="&lt;&lt;" Width="100px" 
                        onclick="btnRight_Click" Height="25px" TabIndex="4" />                                  
                    <br />
                </td>
                <td valign="top" style="width:45%" align="center">
                    <asp:GridView ID="grvData1" runat="server" AutoGenerateColumns="False" 
                        Width="100%" AllowPaging="True" AllowSorting="false" 
                        OnPageIndexChanging="grvData1_PageIndexChanging"   
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
                            <asp:BoundField DataField="GROUPID" HeaderText="Mã nhóm" />
                            <asp:BoundField DataField="GROUPNAME" HeaderText="Tên nhóm" />
                        </Columns>
                        <rowstyle backcolor="#EEEEEE" forecolor="Black" />
                        <pagerstyle backcolor="#999999" forecolor="Black" horizontalalign="Center" />
                        <selectedrowstyle backcolor="#008A8C" font-bold="True" forecolor="White" />                        
                        <HeaderStyle BackColor="#3399FF" ForeColor="Yellow" />
                        <alternatingrowstyle backcolor="#DCDCDC" />
                    </asp:GridView>
                </td>                
            </tr>
            </table>         
    </div>
</asp:Content>
