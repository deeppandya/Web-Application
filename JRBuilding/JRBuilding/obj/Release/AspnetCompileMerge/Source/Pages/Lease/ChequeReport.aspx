<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChequeReport.aspx.cs" Inherits="JRBuilding.Pages.Lease.ChequeReport" %>

<!DOCTYPE html>
<html class="bg-black">
    <head>
        <meta charset="UTF-8">
        <title>JRBuildings | Locals</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <!-- bootstrap 3.0.2 -->
        <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- font Awesome -->
        <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Ionicons -->
        <link href="../../css/ionicons.min.css" rel="stylesheet" type="text/css" />
        <!-- Theme style -->
        <link href="../../css/AdminLTE.css" rel="stylesheet" type="text/css" />

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
    </head>
<body class="bg-black">
    <form id="form1" runat="server">
    <section class="content bg-black">
                <div class="row">
                        <!-- left column -->
                        <div class="col-md-12">
                            <!-- general form elements -->
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 id="headertxt" class="box-title"></h3>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-3">
                                        <asp:Label ID="lbltype" runat="server" Text="Type Of search"></asp:Label><br/>
                                        <asp:DropDownList ID="drptype" runat="server" OnSelectedIndexChanged="drptype_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="--Choose One--"  Value="0"></asp:ListItem>
                                            <asp:ListItem Text="By Time" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="By Tenants" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                     </div>
                                    <div class="form-group col-md-3">
                                        <asp:Label ID="lblselected" runat="server" Text=""></asp:Label><br/>
                                        <asp:DropDownList ID="drpselected" runat="server" OnSelectedIndexChanged="drpselected_SelectedIndexChanged" AutoPostBack="true">
                                         
                                        </asp:DropDownList>
                                     </div>
                                </div>
                                <div class="box-footer">
                                     <asp:Button ID="btnsubmit" runat="server" Text="Go" class="btn btn-primary" OnClick="btnsubmit_Click"/>
                                </div>
                            </div><!-- /.box -->
                        </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grid" runat="server" Width="100%" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None" >
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <EmptyDataTemplate>
                                <label style="color:Red;font-weight:bold">You don&#39;t have any cheque information.</label>
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />

                        </asp:GridView>        
                     </div>
                </div>
        <br />
                <div class="box-footer">
                    <asp:Button ID="btnexport" runat="server" Text="Export Excel" class="btn btn-primary" OnClick="btnexport_Click" Visible="false"/>
                </div>
        </section>
    </form>
</body>
</html>