<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WfrmViewCrystalReport.aspx.cs" Inherits="BIDVWEB.BIDV_UC.ViewReport.WfrmViewCrystalReport" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MSBGW_WEB</title>
    <script type="text/javascript">
        function set_interval()
        {
            //the interval 'timer' is set as soon as the page loads
            var timer=setInterval("auto_logout()",600000);
            // the figure '10000' above indicates how many milliseconds the timer be set to.
            //Eg: to set it to 5 mins, calculate 5min= 5x60=300 sec = 300,000 millisec. So set it to 3000000
        }

        function reset_interval()
        {
            //resets the timer. The timer is reset on each of the below events:
            // 1. mousemove 2. mouseclick 3. key press 4. scroliing
            //first step: clear the existing timer
            var timer;
            clearInterval(timer);
            //second step: implement the timer again
            var timer=setInterval("auto_logout()",600000);
        }

        function auto_logout()
        {
            //this function will redirect the user to the logout script
            //alert("You have been logged out and forwarded to Login Page");
            window.close();            
            //alert("You are IDLE For more than 1 minutes,So,You have been logged out and forwarded to Login Page");
            //window.forward("/login.jsp");
        }
    </script> 
</head>
<body onmousemove="reset_interval()" onclick="reset_interval()" onkeypress="reset_interval()">
    <form id="form1" runat="server">
    <p>
        
        <asp:DropDownList ID="ddlFileType" runat="server" Width="140px" Visible="false">
        </asp:DropDownList>
        
        <asp:Button ID="btnExport" runat="server" onclick="btnExport_Click" Visible="false"  
            Text="Export" Width="100px" />
        
    </p>
    <div>
    
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            oninit="CrystalReportViewer1_Init" Height="50px"             
            Width="350px" AutoDataBind="False" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" DisplayGroupTree="False" 
            HasCrystalLogo="False" 
            HasSearchButton="False" HasToggleGroupTreeButton="False" 
            ShowAllPageIds="True" SeparatePages="True" PrintMode="ActiveX" />    
    </div>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
