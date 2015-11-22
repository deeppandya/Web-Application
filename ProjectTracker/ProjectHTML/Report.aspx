<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Report.aspx.cs" Inherits="ProjectHTML.Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="rad" %>
<%@ register assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
  namespace="CrystalDecisions.Web" tagprefix="CR" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
    <link href="Css/Report.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="reportPanel" runat="server">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
            EnableDatabaseLogonPrompt="false" EnableParameterPrompt="false" HasCrystalLogo="False" HasDrilldownTabs="False"
            HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" />
       </asp:Panel>
    </div>
    </form>
</body>
</html>
