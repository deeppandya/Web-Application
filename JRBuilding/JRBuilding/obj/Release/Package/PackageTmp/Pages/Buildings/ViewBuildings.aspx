<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBuildings.aspx.cs" Inherits="JRBuilding.Pages.Buildings.ViewBuildings" %>

<!DOCTYPE html>
<html class="skin-black">
    <head>
        <meta charset="UTF-8">
        <title>JRBuildings | Buildings</title>
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
        
        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
        <script type="text/javascript">
            function viewbuilding(id)
            {
                alert(id);
                
                
            }
        </script>
    </head>
<body class="skin-black">
    <form id="form1" runat="server">
        <p>
            <%--<button class="btn bg-navy margin" >Add New Building </button>--%>
            <asp:Button ID="btnaddbuilding" runat="server" class="btn bg-navy margin" Text="Add New Building" OnClick="btnaddbuilding_Click" />
        </p>
       <div class="row" id="divcontent">
                        
       </div><!-- /.row -->
    </form>
    <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="../../js/bootstrap.min.js" type="text/javascript"></script>
        <!-- AdminLTE App -->
        <script src="../../js/AdminLTE/app.js" type="text/javascript"></script>
</body>
</html>
