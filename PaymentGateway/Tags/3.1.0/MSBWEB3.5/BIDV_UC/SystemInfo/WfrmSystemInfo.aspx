<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSystemInfo.aspx.cs" Inherits="BIDVWEB.BIDV_UC.SystemInfo.WfrmSystemInfo" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
    <br/>
        <table id="Table1" runat="server" width="740px" class="DialogContent">
            <tr>
                <td align="center" colspan="4" class="DialogCaption" style="height:25px">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="THAM SỐ BÁO CÁO" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Chi nhánh"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlBranch" runat="server" Height="16px" Width="140px" 
                        AutoPostBack="true" 
                        onselectedindexchanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblBranch" runat="server" Text="Label"></asp:Label>
                </td>    
            </tr>
            <tr align="left">
                <td style="width:20%">
                    <asp:Label ID="Label7" runat="server" Text="Báo cáo"></asp:Label>
                </td>
                <td style="width:80%" colspan="3">
                    <asp:DropDownList ID="ddlReport" runat="server" Width="140px" 
                        AutoPostBack="true" Height="16px" TabIndex="2" 
                        onselectedindexchanged="ddlReport_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblReport" runat="server" Text="Label"></asp:Label>
                </td>                
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Thời gian lùi lấy dữ liệu"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtTime" runat="server" Width="140px" MaxLength="3" 
                        Height="22px" TabIndex="3"></asp:TextBox>
                &nbsp;<asp:Label ID="Label8" runat="server" Text="(ngày)"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Mô tả"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDes" runat="server" MaxLength="200" Width="523px" 
                        Height="22px" TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">                
                <td colspan="4">                
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Error"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="grvData_PageIndexChanging"   
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Vertical" TabIndex="4" >
                        <Columns>
                            <asp:TemplateField HeaderText="Chọn">                                
                                <ItemStyle Width="8%" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1"
                                     runat="server" AutoPostBack ="true" 
                                    OnCheckedChanged="CheckBox1_CheckedChanged1" />                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="ID" HeaderText="Mã" >
                                <ItemStyle Width="8%" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BRAN_NAME" HeaderText="Chi nhánh" >
                                <ItemStyle Width="46%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REPORTNAME" HeaderText="Báo cáo">
                                <ItemStyle Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TIME" HeaderText="Thời gian" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DESCRIPTION" HeaderText="Mô tả" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemStyle Width="8%" />
                                <ItemTemplate>
                                    <a href='../SystemInfo/WfrmSystemInfo.aspx?mn=1101&ID=<%# Eval("ID") %>'>
                                    Chi tiết</a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
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
                    <asp:Button ID="btnSave" runat="server" Text="Ghi " Width="100px" 
                        onclick="btnSave_Click" Height="25px" TabIndex="7" />
                    <asp:Button ID="btnDel" runat="server" Text="Xóa" Width="100px" 
                        onclick="btnDel_Click" Height="25px" TabIndex="8" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style3
        {
            width: 223px;
        }
    </style>

</asp:Content>

