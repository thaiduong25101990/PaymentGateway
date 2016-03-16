<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmListReport.aspx.cs" Inherits="BIDVWEB.BIDV_UC.Common.WfrmListReport" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 375px; width:100%" align="center">
    <br/>
        <table cellpadding="0" cellspacing="0" border="0" width="600px" class="DialogContent">
            <tr align="center" class="DialogCaption">
                <td style="height:22px">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" 
                        Text = "LIST REPORTS" >
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>            
            <tr>
                <td>
                    <fieldset title="Info">                        
                        <table>
                            <tr align="left">
                                <td style="width:20%">
                                    
                                    <asp:Label ID="Label11" runat="server" Text="Chọn chi nhánh"></asp:Label>
                                </td>
                                <td style="width:50%" colspan="2">
                                    
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="150px" 
                                        AutoPostBack="true" onselectedindexchanged="ddlBranch_SelectedIndexChanged1">
                                    </asp:DropDownList>
                                </td>                                
                                <td style="width:30%">
                                    
                                    &nbsp;</td>
                            </tr>
                            <tr align="left">
                                <td style="width:20%">
                                    
                                    <asp:Label ID="Label2" runat="server" Text="Chức vụ"></asp:Label>
                                    
                                </td>
                                <td style="width:30%">
                                    
                                    <asp:TextBox ID="txtFunction1" runat="server"></asp:TextBox>
                                    
                                </td>
                                <td style="width:20%">
                                    
                                    <asp:Label ID="Label5" runat="server" Text="FullName1"></asp:Label>
                                    
                                </td>
                                <td style="width:30%">
                                    
                                    <asp:TextBox ID="txtName1" runat="server"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    
                                    <asp:Label ID="Label3" runat="server" Text="Chức vụ"></asp:Label>
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFunction2" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    
                                    <asp:Label ID="Label6" runat="server" Text="FullName2"></asp:Label>
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    
                                    <asp:Label ID="Label4" runat="server" Text="Chức vụ"></asp:Label>
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFunction3" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    
                                    <asp:Label ID="Label7" runat="server" Text="FullName3"></asp:Label>
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    
                                    <asp:Label ID="Label10" runat="server" Text="Chức vụ"></asp:Label>
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFunction4" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    
                                    <asp:Label ID="Label8" runat="server" Text="FullName4"></asp:Label>
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName4" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Report Title"></asp:Label>
                                </td>
                                <td colspan="3">                                   
                                    <asp:TextBox ID="txtTitle" runat="server" Width="420px"></asp:TextBox>                                    
                                </td>
                            </tr>
                        </table>                        
                    </fieldset>                    
                </td>
            </tr>       
            <tr>
                <td style="height:20px" align="left">                
                   <asp:Label ID="lblError" runat="server" Text="Error" ForeColor="Red"></asp:Label>                                    
                </td>
            </tr>  
            <tr>
                <td align="left">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        Width="565px">
                        <Columns>                            
                            <asp:BoundField DataField="ID" HeaderText="ID" >
                                <HeaderStyle HorizontalAlign="Center" />                                
                            </asp:BoundField>
                            <asp:BoundField DataField="REPORTNAME" HeaderText="REPORTNAME" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TITLE" HeaderText="TITLE" >
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FUNCTION1" HeaderText="FUNCTION1" >
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="USER1" HeaderText="USER1" >                            
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <a href='../Common/WfrmListReport.aspx?ID_REPORT=<%# Eval("ID_REPORT") %>&ID=<%# Eval("ID") %>'>
                                    &nbsp;Detail&nbsp;</a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                        </Columns>                        
                    </asp:GridView>
                </td>
            </tr>                                                                                               
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Height="22px" Text="Add" Width="80px" 
                        onclick="btnAdd_Click" Visible="False" />
                    <asp:Button ID="btnSave" runat="server" Height="22px" Text="Save" 
                        Width="80px" onclick="btnSave_Click" />
                </td>
            </tr>                                           
        </table>
    </div>
</asp:Content>

