<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmCondition10.aspx.cs" Inherits="BIDVWEB.BIDV_UC.ViewReport.WfrmCondition10" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
    <br/>
    <table style="width:740px" runat ="server" class="DialogContent" >
    <tr>
    <td style="width:100%" valign="top" colspan="2">    
        <table id ="tblMain" style="width:100%" runat ="server">
            <tr id="trCaption" runat="server">
                <td style="height:25px" align="center" colspan ="4" class="DialogCaption">
                    <asp:Label ID="lblCaption" runat="server" Text="Tên báo cáo" Font-Bold="True" 
                        ForeColor="White"></asp:Label>
                </td>                
            </tr>
            <tr>
                <td style="width:15%" align="left">
                    &nbsp;</td>
                <td style="width:35%" align="left" valign="bottom">
                    &nbsp;</td>
                <td style="width:15%" align="left">
                    &nbsp;</td>
                <td style="width:35%" align="left">
                    &nbsp;</td>
            </tr>
            </table>           
    </td>
    </tr>
    <tr>
        <td style="width:15%" align="left">
            &nbsp;</td>
        <td style="width:85%" align ="left">
            
                    <asp:Label ID="lblError" runat="server" Text="Error" Font-Bold="False" 
                        ForeColor="Red"></asp:Label>
            
        </td>
    </tr>
    <tr>
        <td style="width:15%" align="left">
        </td>
        <td style="width:85%" align ="left">
            
                    <asp:Button ID="btnPreview" runat="server" Text="Tạo báo cáo" Width="100px" 
                        onclick="btnPreview_Click" Height="25px" />
                    <asp:Button ID="btnPrint" runat="server" Height="25px" onclick="btnPrint_Click" 
                        Text="In báo cáo" Width="100px" Visible="False" />
                    <asp:Button ID="btnExport" runat="server" Height="25px" 
                        onclick="btnExport_Click" Text="Xuất excel" Width="100px" 
                        Visible="False" />
            
        </td>
    </tr>
    </table>  
    </div>
</asp:Content>    

