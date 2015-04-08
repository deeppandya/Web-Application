<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewscreen.aspx.cs" Inherits="JRBuilding.Pages.Buildings.viewscreen" %>

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
                <%--<div class="row">
                    <div class="col-md-12">
                         <p>
                            <asp:Button ID="btnback" class="btn btn-default margin" runat="server" Text="Back" OnClick="btnback_Click"/>
                         </p>
                     </div>
                </div>--%>
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
                                        <asp:Label ID="lblbuilding" runat="server" Text="Building Name"></asp:Label><br/>
                                        <asp:DropDownList ID="drpbuilding" runat="server" OnSelectedIndexChanged="drpbuilding_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                     </div>
                                    <div class="form-group col-md-3">
                                        <asp:Label ID="lblcivic" runat="server" Text="Civic Address"></asp:Label><br/>
                                        <asp:DropDownList ID="drpcivic" runat="server" OnSelectedIndexChanged="drpcivic_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                     </div>
                                    <div class="form-group col-md-3">
                                        <asp:Label ID="lblsuites" runat="server" Text="Suite Number"></asp:Label><br/>
                                        <asp:DropDownList ID="drpsuites" runat="server" OnSelectedIndexChanged="drpsuites_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                     </div>
                                    <div class="form-group col-md-3">
                                        <asp:Label ID="lbltenant" runat="server" Text="Tenant Name"></asp:Label><br/>
                                        <asp:DropDownList ID="drptenant" runat="server" OnSelectedIndexChanged="drptenant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
                         <asp:Label ID="lblerror" runat="server"></asp:Label>
                         <iframe id="iframecontent" style="min-height: 956px; width: 100%" scrolling="yes" frameborder="0" runat="server"></iframe>        
                     </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gridsuites" runat="server" Width="100%" Visible="false">

                        </asp:GridView>        
                     </div>
                </div>
        </section>
    </form>
</body>
</html>