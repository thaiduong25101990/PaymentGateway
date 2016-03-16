<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSearchSwift.aspx.cs" Inherits="BIDVWEB.BIDV_UC.SearchMessage.WfrmSearchSwift" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
    <br/>
        <table id="Table1" runat="server" width="740px" class="DialogContent">
            <tr>
                <td align="center" colspan="4" class="DialogCaption" style="height:25px">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="ĐIỆN SWIFT" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="width:15%">
                    <asp:Label ID="Label3" runat="server" Text="Từ ngày"></asp:Label>
                </td>
                <td style="width:30%">                    
                    <asp:TextBox ID="txtFromDate" runat="server" Width="140px" MaxLength="10" 
                        Height="22px" TabIndex="1"></asp:TextBox>
                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="2" />
                </td>    
                <td style="width:15%">
                    
                    <asp:Label ID="Label10" runat="server" Text="Chiều điện"></asp:Label>
                    
                </td>
                <td style="width:40%">
                    
                    <asp:DropDownList ID="ddlMsgDirection" runat="server" Width="140px" 
                        Height="16px" TabIndex="3"  AutoPostBack="true"
                        onselectedindexchanged="ddlMsgDirection_SelectedIndexChanged" >
                    </asp:DropDownList>
                &nbsp;
                    <asp:Label ID="lblMsgDirection" runat="server" Font-Bold="False" 
                        ForeColor="Black"></asp:Label>
                    
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Width="140px" MaxLength="10" 
                        Height="22px" TabIndex="4"></asp:TextBox>
                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="5" />
                </td>                   
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Loại điện"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMsgType" runat="server" Height="22px" Width="140px" 
                        TabIndex="6" MaxLength="20"></asp:TextBox>
                </td>             
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Số tiền"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" Width="140px" MaxLength="25" 
                        Height="22px" TabIndex="7" ontextchanged="txtAmount_TextChanged"
                         AutoPostBack="true"></asp:TextBox>
                </td> 
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Số tham chiếu"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRef" runat="server" MaxLength="25" Width="140px" 
                        Height="22px" TabIndex="8"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Loại tiền"></asp:Label> 
                </td>
                <td>
                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="140px" 
                        Height="16px" TabIndex="9">
                    </asp:DropDownList> 
                </td>
                <td>
                    <asp:Label ID="lblBank" runat="server" Text="Ngân hàng gửi"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBank" runat="server" Width="140px" MaxLength="20" 
                        Height="22px" TabIndex="10"></asp:TextBox>                
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="lblBranch" runat="server" Text="Chi nhánh nhận"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBranchReceiver" runat="server" Height="22px" 
                        TabIndex="11" Width="140px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblBranch0" runat="server" Text="Trạng thái in điện"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPrintSTS" runat="server" Height="22px" 
                        TabIndex="11" Width="140px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="left">
                <td colspan="2">                    
                    <asp:Label ID="Label16" runat="server" Text="Tổng số điện: " Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Phân hệ"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepartement" runat="server" Height="22px" 
                        TabIndex="11" Width="140px">
                    </asp:DropDownList>                    
                </td>                
            </tr>            
            <tr >
                <td>
                </td>
                <td>        
                    <asp:DropDownList ID="ddlLang" runat="server" Width="140px" Visible ="false">
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" Width="100px" 
                        Height="25px" TabIndex="12" onclick="btnSearch_Click" />
                </td>
                <td colspan="2" align="left">
                    
                    <asp:Button ID="btnPrint" runat="server" Text="In điện" Width="100px" 
                        Height="25px" TabIndex="13" onclick="btnPrint_Click" />
                </td>                
            </tr>            
            <tr>
                <td align="center" colspan="4">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="grvData_PageIndexChanging"   
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Vertical" TabIndex="14" PageSize="30" >
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
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="QUERY_ID" HeaderText="QUERY_ID" Visible="False" 
                                InsertVisible="False" ShowHeader="False" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="0px" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MSG_TYPE" HeaderText="Loại điện" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="80px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="BANK" HeaderText="NH gửi/nhận" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Left"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="FIELD20" HeaderText="Số tham chiếu">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="120px" HorizontalAlign="Left"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="CCYCD" HeaderText="Loại tiền" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="AMOUNT" HeaderText="Số tiền" 
                            DataFormatString="{0:###,##0.00}">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="PRINT_STS" HeaderText="TT in điện">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="60px"  HorizontalAlign="Center" />
                            </asp:BoundField>                                             
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemTemplate>
                                    <a href='../SearchMessage/WfrmViewMsgSWIFT.aspx?mn=2301&ID=<%# Eval("MSG_ID") %>'  target="_blank">
                                    Chi tiết</a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>

                            <asp:TemplateField Visible="false" >                                
                                <ItemTemplate> 
                                    <asp:Label ID="lblMsgid" runat="server" Text='<%# Bind("MSG_ID") %>'>
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