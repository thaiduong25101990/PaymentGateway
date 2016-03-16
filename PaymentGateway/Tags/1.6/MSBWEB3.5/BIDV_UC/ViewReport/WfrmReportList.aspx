<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmReportList.aspx.cs" Inherits="BIDVWEB.BIDV_UC.ViewReport.WfrmReportList" Title="MSBGW_WEB" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
    <br/>
        <table style="width:740px" class="DialogContent">
            <tr>
                <td align="left" valign="bottom" class="DialogCaption" style="height:25px">
                    <asp:Image ID="Image1" runat="server" Width="16px" 
                        ImageUrl="~/Images/txtmenu.gif" />
                    &nbsp;<asp:Label ID="lblCaption" runat="server" Text="Danh sách báo cáo" Font-Bold="True" 
                        ForeColor="White"></asp:Label>                    
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblError" runat="server" Text="Error" Font-Bold="False" 
                        ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" ShowHeader="False" Width="100%">
                        <Columns>                                                        
                            <asp:ImageField DataImageUrlField="URLICON">
                                <ItemStyle Height="25px" Width="25px" />
                            </asp:ImageField>
                            <asp:HyperLinkField DataTextField="TITLE" 
                                NavigateUrl="URL" DataNavigateUrlFields="URL" ShowHeader="False" />                            
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/wordclean.gif" />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="left" class="style3">
                   
                </td>
            </tr>
        </table>    
    </div>
</asp:Content>    


<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style3
        {
            height: 22px;
        }
    </style>

</asp:Content>
    


