<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSearchVCB.aspx.cs" Inherits="BIDVWEB.BIDV_UC.SearchMessage.WfrmSearchVCB" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
    <br/>
        <table id="Table1" runat="server" width="740px" class="DialogContent">
            <tr>
                <td align="center" colspan="4" class="DialogCaption" style="height:25px">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="ĐIỆN VCB" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="width:25%">
                    <asp:Label ID="Label3" runat="server" Text="Từ ngày"></asp:Label>
                </td>
                <td style="width:30%">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="140px" MaxLength="10" 
                        Height="22px" TabIndex="1"></asp:TextBox>
                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="2" />
                </td>    
                <td style="width:15%">
                    <asp:Label ID="Label2" runat="server" Text="Số tham chiếu"></asp:Label>
                </td>
                <td style="width:30%">
                    <asp:TextBox ID="txtRef" runat="server" MaxLength="30" Width="140px" 
                        Height="22px" TabIndex="10"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Width="140px" MaxLength="10" 
                        Height="22px" TabIndex="3"></asp:TextBox>
                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="4" />
                </td>                   
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Loại điện"></asp:Label>
                </td>
                <td>
                    
                    <asp:TextBox ID="txtMsgType" runat="server" MaxLength="20" Width="140px" 
                        Height="22px" TabIndex="11"></asp:TextBox>
                    
                </td>             
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Ngân hàng gửi"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSender" runat="server" Width="140px" MaxLength="12" 
                        Height="22px" TabIndex="5"></asp:TextBox>                
                </td> 
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Nguồn điện"></asp:Label>                           
                </td>
                <td>
                    <asp:DropDownList ID="ddlMsgSource" runat="server" Height="16px" Width="140px" 
                         TabIndex="12">
                    </asp:DropDownList>   
                </td>
            </tr>
            <tr align="left">
                <td class="style7">
                    <asp:Label ID="Label5" runat="server" Text="Ngân hàng nhận"></asp:Label>
                </td>
                <td class="style7">
                    <asp:TextBox ID="txtReceiver" runat="server" MaxLength="12" Width="140px" 
                        Height="22px" TabIndex="6"></asp:TextBox>
                </td>
                <td class="style7">
                    <asp:Label ID="Label9" runat="server" Text="Phân hệ"></asp:Label>                           
                </td>
                <td class="style7">
                    <asp:DropDownList ID="ddlDepartment" runat="server" Height="16px" Width="140px" 
                         TabIndex="13">
                    </asp:DropDownList>   
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Số tiền"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" Width="140px" MaxLength="30" 
                        Height="22px" TabIndex="7" ontextchanged="txtAmount_TextChanged" 
                         AutoPostBack="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Chiều điện"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMsgDirection" runat="server" Width="140px" 
                        Height="16px" TabIndex="14" >
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Loại tiền"></asp:Label> 
                </td>
                <td>
                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="140px" 
                        Height="16px" TabIndex="8">
                    </asp:DropDownList> 
                </td>
                <td>
                    
                    <asp:Label ID="Label12" runat="server" Text="Trạng thái"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="140px" 
                        Height="16px" TabIndex="15">
                    </asp:DropDownList> 
                    
                </td>
            </tr>            
            <tr align="left">
                <td>
                    <asp:Label ID="Label16" runat="server" Text="Số RM/Số tham chiếu gốc"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRM" runat="server" Height="22px" Width="140px" 
                        TabIndex="9"></asp:TextBox>
                </td>
                <td>
                    
                    <asp:Label ID="Label17" runat="server" Text="Trạng thái in điện"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlPrintSTS" runat="server" Width="140px" 
                        Height="16px" TabIndex="15">
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
                        Height="25px" TabIndex="16" onclick="btnSearch_Click" />
                    <asp:Button ID="btnPrint" runat="server" Text="In điện" Width="100px" 
                        Height="25px" TabIndex="17" onclick="btnPrint_Click" Visible="true" />
                </td>                
            </tr>            
            <tr>
                <td align="center" colspan="4">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="grvData_PageIndexChanging"   
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Vertical" TabIndex="18" PageSize="30" >
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
                            <asp:BoundField DataField="QUERY_ID" HeaderText="Query_id" Visible="False" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RM_NUMBER" HeaderText="Số RM"> 
                                <HeaderStyle HorizontalAlign="Center"/>
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FIELD20" HeaderText="Số bút toán">                            
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BRANCH_A" HeaderText="NH gửi" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BRANCH_B" HeaderText="NH nhận" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AMOUNT" HeaderText="Số tiền" 
                            DataFormatString="{0:###,##0.00}">
                                <HeaderStyle HorizontalAlign="Center" />    
                                <ItemStyle HorizontalAlign="Right" Width="130px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CCYCD" HeaderText="Loại tiền" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MSG_TYPE" HeaderText="Loại điện" Visible="False">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemTemplate>
                                    <a href='../SearchMessage/WfrmViewMsgVCB.aspx?mn=2401&ID=<%# Eval("MSG_ID") %>' target="_blank">
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
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style3
        {
            width: 15%;
            height: 17px;
        }
        .style4
        {
            width: 30%;
            height: 17px;
        }
        .style5
        {
            width: 20%;
            height: 17px;
        }
        .style6
        {
            width: 35%;
            height: 17px;
        }
        .style7
        {
            height: 5px;
        }
    </style>

</asp:Content>
