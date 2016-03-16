<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmList.aspx.cs" Inherits="BIDVWEB.BIDV_UC.Common.WfrmList" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 375px; width:100%" align="center">
    <br/>
        <table id="Table1" runat="server" style="width:95%">
            <tr>
                <td align="center" colspan="4" class="DialogCaption" style="height:22px">
                    <asp:Label ID="lblCaption" runat="server" Font-Bold="True" Text="LISTS" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:80px" align ="left" >
                    &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Select Field"></asp:Label>
                </td>
                <td style="width:150px" valign="bottom"  align ="left">
                    <asp:DropDownList ID="ddlField" runat="server">
                    </asp:DropDownList>                                        
                </td>                    
                <td style="width:70px" valign="bottom" align ="left">                                
                    <asp:DropDownList ID="ddlOperator" runat="server">
                    </asp:DropDownList>                    
                </td>
                <td style="width:53%" valign="top"  align ="left">
                    <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Height="25px" Text="Search" 
                        Width="80px" />
                </td>
            </tr>
            <tr>
                <td style="width:100%" colspan="4" align ="left">
                    &nbsp;&nbsp;<asp:Label ID="lblRecordCount" runat="server" Text="lblRecordCount" 
                        ForeColor="#3333FF"></asp:Label>
                </td>                
            </tr>
            <tr>
                <td colspan="4" align ="left">
                    <asp:GridView ID="grvList" runat="server" Width="100%" 
                        AutoGenerateColumns="False">
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" 
                        ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" 
                        
                        SelectCommand="SELECT USERID,USERNAME FROM &quot;USERS&quot; ORDER BY &quot;ID&quot;">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4">
                    <asp:Label ID="lblError" runat="server" Text="Error" ForeColor="Red"></asp:Label>                    
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnAdd" runat="server" Height="25px" Text="Add" Width="80px" />
                    <asp:Button ID="btnDel" runat="server" Height="25px" Text="Delete" 
                        Width="80px" />                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

