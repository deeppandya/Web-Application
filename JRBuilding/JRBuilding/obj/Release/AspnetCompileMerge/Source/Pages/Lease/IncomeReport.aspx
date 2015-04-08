<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncomeReport.aspx.cs" Inherits="JRBuilding.Pages.Lease.IncomeReport" EnableEventValidation="false" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="UTF-8">
    <title>JRBuildings | Profile</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- bootstrap 3.0.2 -->
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- font Awesome -->
    <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Ionicons -->
    <link href="../../css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="../../css/AdminLTE.css" rel="stylesheet" type="text/css" />

    <link rel="shortcut icon" href="../../img/home.png" >

    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
</head>
<body class="skin-black">
    <form id="form1" runat="server">
                <div class="row">
                    <div class="col-md-12">
                                    <p>
                                        <asp:GridView ID="grid" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" Visible="false">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <EmptyDataTemplate>
                                                <label style="color:Red;font-weight:bold">You don&#39;t have any Tenants.</label>
                                            </EmptyDataTemplate>
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                        </asp:GridView>
                                    </p>
                     </div>
                </div>
        <div class="box-footer">
                                        <%--<button type="submit" class="btn btn-primary">Submit</button>--%>
               <asp:Button ID="btnexport" runat="server" Text="Export Excel" class="btn btn-primary" OnClick="btnexport_Click" Visible="false"/>
                                    </div>
    </form>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js" type="text/javascript"></script>
</body>
</html>
