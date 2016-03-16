<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmIBPS_TS.aspx.cs" Inherits="BIDVWEB.BIDV_UC.SearchMessage.WfrmIBPS_TS" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
    <br/>
        <table id="Table1" runat="server" width="740px" class="DialogContentSmall">
            <tr>
                <td align="center" colspan="4" class="DialogCaption" style="height:25px">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="ĐIỆN TRA SOÁT NỘI BỘ IBPS" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="width:15%">
                    <asp:Label ID="Label3" runat="server" Text="Từ ngày"></asp:Label>
                </td>
                <td style="width:35%">                    
                    <asp:TextBox ID="txtFromDate" runat="server" Width="140px" MaxLength="10" 
                         TabIndex="1"></asp:TextBox>
                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="2" />
                </td>    
                <td style="width:17%">
                    &nbsp;</td>
                <td style="width:33%">
                    &nbsp;</td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label16" runat="server" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Width="140px" MaxLength="10" 
                         TabIndex="1"></asp:TextBox>
                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="2" />
                </td>                   
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Chiều điện" Visible ="false" 
                    >
                    </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMsgDirection" runat="server" Width="140px" 
                        Height="16px" TabIndex="12" Visible ="false">
                    </asp:DropDownList> 
                </td>             
            </tr>
            <tr align="left">
                <td>
                    Số tham chiếu</td>
                <td>
                    <asp:TextBox ID="txtRefNo" runat="server" Width="140px" MaxLength="20" 
                        Height="22px" TabIndex="4"></asp:TextBox>
                </td>                   
                <td>
                    <asp:Label ID="Label8" runat="server" Text="CN gửi tra soát"></asp:Label> 
                </td>
                <td>
                    <asp:DropDownList ID="ddlBrSend" runat="server" Width="200px" 
                        Height="16px" TabIndex="6" AutoPostBack = "true"
                        onselectedindexchanged="ddlBrSend_SelectedIndexChanged">
                    </asp:DropDownList> 
                </td>             
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Trạng thái"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="140px" 
                        Height="16px" TabIndex="16">
                    </asp:DropDownList> 
                </td> 
                <td>
                    <asp:Label ID="Label13" runat="server" Text="CN nhận tra soát"></asp:Label>                           
                </td>
                <td>
                    <asp:DropDownList ID="ddlBrReceive" runat="server" Width="200px" 
                         TabIndex="8" Style="max-height:50px; min-height:50px; 
                        orphans:10; widows:10">
                    </asp:DropDownList>   
                </td>
            </tr>
            <tr align="left">
                <td colspan="2">                    
                    <asp:Label ID="Label15" runat="server" Text="Tổng số điện: " Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label>                    
                </td>
                <td colspan="2">                    
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" Width="100px" 
                        onclick="btnSearch_Click" Height="25px" TabIndex="20" />
                    <asp:Button ID="btnAdd" runat="server" Text="Tạo mới" Width="100px" 
                        onclick="btnAdd_Click" Height="25px" TabIndex="20" />
                    <asp:Button ID="btnApprove" runat="server" Text="Duyệt" Width="100px" 
                        onclick="btnApprove_Click" Height="25px" TabIndex="20" Visible="false" />
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="100px" 
                        onclick="btnPrint_Click" Height="25px" TabIndex="20" Visible="false" />
                </td>                
            </tr>            
            <tr>
                <td align="center" colspan="4">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="grvData_PageIndexChanging"   
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Vertical" TabIndex="22" >
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
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="SBT_ID" HeaderText="Số lệnh TS">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="100px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="REFNO" HeaderText="Số tham chiếu">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="100px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="BR_SEND" HeaderText="CN gửi TS" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="70px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="BR_RECEIVE" HeaderText="CN nhận TS" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STATUS" HeaderText="Trạng thái">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="70px"  HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemTemplate>
                                    <a href='../SearchMessage/WfrmViewIBPS_TS.aspx?mn=2102&ID=<%# Eval("ID") %>&vi=0' target="_blank">
                                    Chi tiết</a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tra soát">
                                <ItemTemplate>
                                    <a href='../SearchMessage/WfrmViewIBPS_TS.aspx?mn=2102&ID=<%# Eval("ID") %>&vi=1' target="_blank">
                                    <%# Eval("TTS") %></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" >                                
                                <ItemTemplate> 
                                    <asp:Label ID="lblQueryid" runat="server" Text='<%# Bind("ID") %>'>
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