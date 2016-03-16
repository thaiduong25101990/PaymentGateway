<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSystemPara.aspx.cs" Inherits="BIDVWEB.BIDV_UC.SystemInfo.WfrmSystemPara" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
        <br>                      
        <table style="width: 740px" cellpadding="0" cellspacing="0" class="DialogContent">
            <tr>
                <td class="DialogCaption" colspan="2" align="center" style="height:25px">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" 
                        Text="THAM SỐ HỆ THỐNG"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Error"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="width:20%">                
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" ForeColor="Black" 
                        Text="Tên"></asp:Label>                      
                </td>
                <td style="width:80%">
                    <asp:TextBox ID="txtName" runat="server" 
                        Width="140px" TabIndex="1" MaxLength="30"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtName" ErrorMessage="Chưa nhập tên" 
                        Display="Dynamic">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr align="left">
                <td>                
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" ForeColor="Black" 
                        Text="Giá trị"></asp:Label>                                
                </td>
                <td >
                    <asp:TextBox ID="txtValue" runat="server" Width="140px" 
                        TabIndex="2" MaxLength="30"></asp:TextBox>
    &nbsp;<asp:Image ID="img1" runat="server" ImageUrl="~/Images/insertdate.gif" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtValue" Display="Dynamic" ErrorMessage="Nhập giá trị">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr align="left">
                <td>                
                    <asp:Label ID="lblHeader" runat="server" Font-Bold="False" 
                        Text="Kiểu dữ liệu"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" TabIndex="3" Width="140px" 
                        onselectedindexchanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="ddlType" Display="Dynamic" ErrorMessage="Chọn kiểu dữ liệu">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="Black" 
                        Text="Ghi chú"></asp:Label>                
                </td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" Width="484px" 
                        TabIndex="4" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr align="center">
                <td colspan="2">
                    <asp:GridView ID="grvData" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" Width="100%"  OnPageIndexChanging ="grvData_PageIndexChanging"  
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Vertical" >                        
                        <Columns>  
                            <asp:TemplateField HeaderText="Chọn">                                
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1"
                                     runat="server" AutoPostBack ="true" 
                                    OnCheckedChanged="CheckBox1_CheckedChanged1" />                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>                          
                            <asp:BoundField DataField="ID" HeaderText="Mã">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VARNAME" HeaderText="Tên" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VALUE" HeaderText="Giá trị" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TYPE" HeaderText="Kiểu" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NOTE" HeaderText="Ghi chú" />
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemTemplate>
                                    <a href='../SystemInfo/WfrmSystemPara.aspx?mn=1102&ID=<%# Eval("ID") %>'>
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
                <td align="right" colspan="2">
                    <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" 
                        Text="Thêm mới" Height="25px" Width="100px" TabIndex="6" 
                        CausesValidation="False" />
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" 
                        Text="Ghi" Height="25px" Width="100px" TabIndex="7" 
                        CausesValidation="False" />
                    <asp:Button ID="btnDel" runat="server" onclick="btnDel_Click" 
                        Text="Xóa" Height="25px" Width="100px" TabIndex="8" 
                        CausesValidation="False" />
                </td>
            </tr>
        </table>            
    </div>
</asp:Content>            
            
