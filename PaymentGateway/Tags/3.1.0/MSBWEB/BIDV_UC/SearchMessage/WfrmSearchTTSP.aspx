<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSearchTTSP.aspx.cs" Inherits="BIDVWEB.BIDV_UC.SearchMessage.WfrmMessageManagement" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
        <br/>
        <table id="Table1" runat="server" width="740px" class="DialogContentSmall">
            <tr>
                <td align="center" colspan="4" class="DialogCaption" style="height:25px">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="IN ĐIỆN TTSP" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td class="style3">
                    <asp:Label ID="Label3" runat="server" Text="Từ ngày"></asp:Label>
                </td>
                <td class="style4">                    
                    <asp:TextBox ID="txtFromDate" runat="server" Width="200px" MaxLength="10" 
                         TabIndex="1"></asp:TextBox>
                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="2" />
                </td>    
                <td style="width:17%">
                    Số thạm chiếu</td>
                <td style="width:33%">
                    <asp:TextBox ID="txtRefno" runat="server" Width="200px" MaxLength="20" 
                        Height="22px" TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td class="style3">
                    <asp:Label ID="Label16" runat="server" Text="Đến ngày"></asp:Label>
                </td>
                <td class="style4">
                    <asp:TextBox ID="txtToDate" runat="server" Width="200px" MaxLength="10" 
                         TabIndex="1"></asp:TextBox>
                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="2" />
                </td>                   
                <td>
                    Số RM</td>
                <td>
                    <asp:TextBox ID="txtRMNo" runat="server" Width="200px" MaxLength="20" 
                        Height="22px" TabIndex="4"></asp:TextBox>
                </td>             
            </tr>
            <tr align="left">
                <td style="border-width: thin" class="style3">
                    Ngân hàng gửi</td>
                <td style="border-width: thin" class="style4">
                    <asp:TextBox ID="txtSender" runat="server" Width="200px" MaxLength="20" 
                        Height="22px" TabIndex="4"></asp:TextBox>
                </td>                   
                <td style="border-width: thin">
                    Chiều điện</td>
                <td style="border-width: thin">
                    <asp:DropDownList ID="ddlMSGDirection" runat="server" Width="200px" 
                        Height="20px" TabIndex="6" AutoPostBack = "true"
                        onselectedindexchanged="ddlMSGDirection_SelectedIndexChanged">
                    </asp:DropDownList> 
                </td>             
            </tr>
            <tr align="left">
                <td class="style3">
                    Ngân hàng nhận</td>
                <td class="style4">
                    <asp:TextBox ID="txtReceiver" runat="server" Width="200px" MaxLength="20" 
                        Height="22px" TabIndex="4"></asp:TextBox>
                </td> 
                <td>
                    Trạng thái</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="200px" 
                        Height="20px" TabIndex="6" AutoPostBack = "true"
                        onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr align="left">
                <td class="style3">
                    Loại tiền</td>
                <td class="style4">
                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="200px" 
                        Height="20px" TabIndex="6" AutoPostBack = "True"
                        onselectedindexchanged="ddlCurrency_SelectedIndexChanged">
                    </asp:DropDownList> 
                </td> 
                <td>
                    Phân hệ</td>
                <td>
                    <asp:DropDownList ID="ddlModule" runat="server" Width="200px" 
                        Height="20px" TabIndex="6" AutoPostBack = "true"
                        onselectedindexchanged="ddlModule_SelectedIndexChanged">
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr align="left">
                <td class="style3">
                    Số tiền</td>
                <td class="style4">
                    <asp:TextBox ID="txtAmount" runat="server" Width="200px" MaxLength="20" 
                        Height="22px" TabIndex="4"></asp:TextBox>
                </td> 
                <td>
                    Loại điện</td>
                <td>
                    <asp:TextBox ID="txtMSGType" runat="server" Width="200px" MaxLength="20" 
                        Height="22px" TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td colspan="2">                    
                    <asp:Label ID="Label15" runat="server" Text="Tổng số điện: " Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label>                    
                </td>
                <td colspan="2">                    
                    <asp:Button ID="btnAdd" runat="server" Text="Tạo mới" Width="100px" 
                        onclick="btnAdd_Click" Height="25px" TabIndex="20" Visible="False" />
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" Width="100px" 
                        onclick="btnSearch_Click" Height="25px" TabIndex="20" />
                    <asp:Button ID="btnApprove" runat="server" Text="Duyệt" Width="100px" 
                        onclick="btnApprove_Click" Height="25px" TabIndex="20" Visible="false" />
                    <asp:Button ID="btnPrint" runat="server" Text="In điện" Width="100px" 
                        onclick="btnPrint_Click" Height="25px" TabIndex="20" />
                </td>                
            </tr>            
            <tr>
                <td align="center" colspan="4">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="grvData_PageIndexChanging" 
                        CellPadding="4" GridLines="None" TabIndex="22" ForeColor="#333333" 
                        BorderStyle="Solid">
                        <Columns>
                            <asp:TemplateField HeaderText="Chọn" Visible="true" >
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
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>        
                            <asp:BoundField DataField="Sender" HeaderText="NH gửi">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="100px"/>
                            </asp:BoundField>                    
                            <asp:BoundField DataField="Receiver" HeaderText="NH nhận">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="100px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="TRANS_DATE" HeaderText="Ngày chuyển" 
                                DataFormatString="{0:dd-MM-yyyy}" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="AMOUNT" HeaderText="Số tiền" 
                                DataFormatString="{0:###,###}" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="80px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FIELD20" HeaderText="Số tham chiếu">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="70px"  HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemTemplate>
                                    <a href='../SearchMessage/WfrmViewTTSP.aspx?mn=2502&ID=<%# Eval("MSG_ID") %>&vi=0' target="_blank">
                                    Chi tiết</a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" >                                
                                <ItemTemplate> 
                                    <asp:Label ID="lblQueryid" runat="server" Text='<%# Bind("MSG_ID") %>'>
                                    </asp:Label>
                                </ItemTemplate>                                
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
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style3
        {
            width: 31%;
        }
        .style4
        {
            width: 38%;
        }
    </style>
</asp:Content>
