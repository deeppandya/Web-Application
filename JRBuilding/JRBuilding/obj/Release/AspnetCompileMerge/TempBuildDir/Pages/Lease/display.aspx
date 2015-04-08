<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="display.aspx.cs" Inherits="JRBuilding.Pages.Lease.display" %>

<!DOCTYPE html>
<html class="bg-black">
    <head>
        <meta charset="UTF-8">
        <title>JRBuildings | View Documents</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <!-- bootstrap 3.0.2 -->
        <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- font Awesome -->
        <link href="../../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
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
                            <div class="col-md-12">
                                            <p>
                                                <asp:Button ID="btnback" class="btn btn-default margin" runat="server" Text="Back" OnClick="btnback_Click"/>
                                            </p>
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <iframe runat="server" id="iframepdf" height="600" width="100%" visible="false"> </iframe>
                            </div>
                        </div>
        </section>

        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="../../js/bootstrap.min.js" type="text/javascript"></script>        

    </form>
</body>
</html>
