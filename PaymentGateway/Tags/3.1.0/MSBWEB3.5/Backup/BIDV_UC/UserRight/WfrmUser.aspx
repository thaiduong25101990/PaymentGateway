<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmUser.aspx.cs" Inherits="BIDVWEB.BIDV_UC.UserRight.WfrmUser" Title="MSBGW_WEB" %>
<script runat="server">

  //void grvData_DataBound(Object sender, EventArgs e)
  //  {
  //  if (!IsPostBack)
  //  {
  //    // Call a helper method to display the current page number 
  //    // when the page is first loaded.
  //    DisplayCurrentPage();
  //  }
  //}

  //void grvData_PageIndexChanged(Object sender, EventArgs e)
  //{
  //  // Call a helper method to display the current page number 
  //  // when the user navigates to a different page.
  //  DisplayCurrentPage();
  //}

  //void DisplayCurrentPage()
  //{
  //  // Calculate the current page number.
  //    int currentPage = grvData.PageIndex + 1;

  //  // Display the current page number. 
  //  //Message.Text = "Page " + currentPage.ToString() + " of " +
  //    //grvData.PageCount.ToString() + ".";
  //}
</script>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
    <br/>
        <table id="Table1" runat="server" width="740px" class="DialogContent">
            <tr>
                <td align="center" colspan="5" class="DialogCaption" style="height:25px">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="THÔNG TIN NGƯỜI SỬ DỤNG" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="width:15%">
                    <asp:Label ID="Label7" runat="server" Text="Chi nhánh"></asp:Label>
                </td>
                <td style="width:25%">
                    <asp:DropDownList ID="ddlBranch" runat="server" Height="16px" Width="140px" 
                        AutoPostBack="true" 
                        onselectedindexchanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="*" ControlToValidate="ddlBranch"></asp:RequiredFieldValidator>
                </td>                
                <td style="width:60%" colspan ="3">
                    <asp:Label ID="lblBranch" runat="server" Text="Label"></asp:Label>
                </td>                
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Mã NSD"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPrefix" runat="server" Width="45px" Height="22px" 
                        TabIndex="2" MaxLength="3"></asp:TextBox>
                    <asp:TextBox ID="txtUserID" runat="server" Width="90px" MaxLength="8" 
                        Height="22px" TabIndex="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtUserID" ErrorMessage="*" Enabled="False"></asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;</td>
                <td style="width:25%">
                    <asp:Label ID="Label2" runat="server" Text="Số di động"></asp:Label>
                </td>
                <td style="width:30%">
                    <asp:TextBox ID="txtMobile" runat="server" Width="140px" Height="22px" 
                        TabIndex="4" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Tên người sử dụng."></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" Width="140px" Height="22px" 
                        TabIndex="5" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtUserName" ErrorMessage="*" Enabled="False"></asp:RequiredFieldValidator>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Email"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Height="22px" Width="140px" 
                        TabIndex="6" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td style="width:20%">
                    <asp:Label ID="Label3" runat="server" Text="Mật khẩu"></asp:Label>
                </td>
                <td style="width:25%">
                    <asp:TextBox ID="txtPassword" runat="server" Height="22px" TextMode="Password" 
                        Width="140px" TabIndex="7" MaxLength="100"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtConfirm" ControlToValidate="txtPassword" ErrorMessage="*"></asp:CompareValidator>
                </td>
                <td style="width:8%">
                    &nbsp;</td>                    
                <td style="width:15%">
                    <asp:Label ID="Label11" runat="server" Text="Ngày đăng nhập cuối"></asp:Label>
                </td>
                <td style="width:32%">
                    <asp:TextBox ID="txtLTDate" runat="server" Width="140px" Height="22px" 
                        TabIndex="8"></asp:TextBox>
                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="9" />
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Xác nhận mật khẩu"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtConfirm" runat="server" Height="22px" TextMode="Password" 
                        Width="140px" TabIndex="10" MaxLength="100"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToCompare="txtPassword" ControlToValidate="txtConfirm" ErrorMessage="*"></asp:CompareValidator>
                </td>
                <td>
                </td>                    
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Ngày đổi mật khẩu cuối"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLCPDate" runat="server" Width="140px" Height="22px" 
                        TabIndex="11"></asp:TextBox>
                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="12" />
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Trạng thái"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="140px" TabIndex="13" 
                        AutoPostBack ="true"                         
                        ontextchanged="ddlStatus_TextChanged">
                    </asp:DropDownList>
                </td>
                <td>
                </td>           
                <td>
                    <asp:Button ID="btnFind" runat="server" Height="25px" onclick="btnFind_Click" 
                        Text="Tìm kiếm" Width="100px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Mô tả"></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtDes" runat="server" Width="551px" Height="22px" 
                        TabIndex="14" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5">
                    <asp:Label ID="lblError" runat="server" Text="Error" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="style3" align="center">
                    <asp:GridView ID="grvData" runat="server" AllowPaging="true" 
                        AutoGenerateColumns="False" Width="100%"  OnPageIndexChanging ="grvData_PageIndexChanging"  
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Vertical" PageSize="30" >                        
                        <Columns>  
                            <asp:TemplateField HeaderText="Chọn">                                
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1"
                                     runat="server" AutoPostBack ="true" 
                                    OnCheckedChanged="CheckBox1_CheckedChanged1" />                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>                          
                            <asp:BoundField DataField="USERID" HeaderText="Mã NSD" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="70px" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="USERNAME" HeaderText="Tên NSD" >
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DESCRIPTION" HeaderText="Mô tả" >
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemTemplate>
                                    <a href='../UserRight/WfrmUser.aspx?mn=1201&USERID=<%# Eval("USERID") %>'>
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
                <td align="right" colspan="5">
                    <asp:Button ID="btnAdd" runat="server" Text="Thêm mới" Width="100px" 
                        onclick="btnAdd_Click" Height="25px" TabIndex="16" 
                        CausesValidation="False" />
                    <asp:Button ID="btnSave" runat="server" Text="Ghi" Width="100px" 
                        onclick="btnSave_Click" Height="25px" TabIndex="17" CausesValidation="False" 
                         />
                    <asp:Button ID="btnDel" runat="server" onclick="btnDel_Click" Text="Xóa" 
                        Width="100px" Height="25px" TabIndex="18" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style3
        {
            height: 95px;
        }
    </style>

</asp:Content>


