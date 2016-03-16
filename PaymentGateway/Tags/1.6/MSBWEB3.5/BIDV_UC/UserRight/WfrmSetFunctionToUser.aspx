<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSetFunctionToUser.aspx.cs" Inherits="BIDVWEB.BIDV_UC.UserRight.WfrmSetFunctionToUser" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 375px; width:100%" align="center">
        <br>
        <table cellpadding="0" cellspacing="0" border="0" width="600px" class="DialogContent">
            <tr>
                <td class="DialogCaption" align="center" colspan="2">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="SET  PERMISSION USER" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left" >
                <td valign ="middle" colspan="2">
                    <asp:Label ID="Label2" runat="server" Text="Select User"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlUser" runat="server" Height="16px" 
                        Width="163px" AutoPostBack="true" 
                        onselectedindexchanged="ddlUser_SelectedIndexChanged">
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr align="left">
                <td valign="top" style="width:50%" colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Danh sách menu"></asp:Label>
                </td>                           
            </tr>
            <tr align="left">
                <td valign="top" style="width:50%" colspan="2">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        Width="567px" PageSize="2">
                        <Columns>                            
                            <asp:TemplateField HeaderText="Select">                                
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1"
                                    AutoPostBack="true" runat="server"
                                    OnCheckedChanged="CheckBox1_CheckedChanged1" />
                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MENUID" HeaderText="MENUID" >
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CAPTION" HeaderText="CAPTION" />
                            <asp:BoundField DataField="PERMISSION" HeaderText="PERMISSION" >                             
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Input Permission">
                                
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPermission" runat="server" Height="16px" Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                
                                <ItemStyle Width="80px" />
                                
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>                               
            </tr>
            <tr align="left">
                <td colspan="2">
                    <asp:Label ID="lblError" runat="server" Text="Error" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="80px" 
                        onclick="btnSave_Click" />                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

